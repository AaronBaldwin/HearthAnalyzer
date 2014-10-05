using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Deathrattles;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents a player in Hearthstone
    /// </summary>
    public abstract class BasePlayer : IDamageableEntity, IAttacker, IEquatable<BasePlayer>
    {
        internal int attacksThisTurn = 0;

        protected BasePlayer(int id = -1)
        {
            this.Id = id;
            this.Hand = new List<BaseCard>();
            this.Deck = new Deck();
            this.Health = 30;
            this.Armor = 0;
            this.Mana = 0;
            this.MaxMana = 0;
            this.Graveyard = new List<BaseCard>();
            this.Weapon = null;
            this.HeroPower = null;
        }

        /// <summary>
        /// The id for this player
        /// </summary>
        public int Id;

        /// <summary>
        /// The name of this player's hero
        /// </summary>
        public string Name;

        /// <summary>
        /// The game board the player is playing on
        /// </summary>
        public GameBoard Board;

        /// <summary>
        /// The players hand
        /// </summary>
        public List<BaseCard> Hand;

        /// <summary>
        /// The player's deck
        /// </summary>
        public Deck Deck;

        /// <summary>
        /// The player's graveyard
        /// </summary>
        public List<BaseCard> Graveyard;

        /// <summary>
        /// The player's health
        /// </summary>
        public int Health;

        /// <summary>
        /// The player's armor value
        /// </summary>
        public int Armor;

        /// <summary>
        /// The player's remaining mana
        /// </summary>
        public int Mana;

        /// <summary>
        /// The player's maximum mana
        /// </summary>
        public int MaxMana;

        /// <summary>
        /// The player's current overload amount
        /// </summary>
        public int Overload;

        /// <summary>
        /// The player's pending overload amount
        /// </summary>
        public int PendingOverload;

        /// <summary>
        /// The temporary attack buff added to this player
        /// </summary>
        public int TemporaryAttackBuff;

        /// <summary>
        /// The player's status effects
        /// </summary>
        public PlayerStatusEffects StatusEffects;

        /// <summary>
        /// The player's weapon
        /// </summary>
        public BaseWeapon Weapon;

        /// <summary>
        /// The player's hero power
        /// </summary>
        public BaseCard HeroPower;

        /// <summary>
        /// Plays a card
        /// </summary>
        /// <param name="card">The card to play</param>
        /// <param name="subTarget">The sub target for this card, usually for targeting batlle cry spells</param>
        /// <param name="gameboardPos">The position on the gameboard to place the card (if applicable)</param>
        /// <param name="forceSummoned">Whether or not this minion was force summoned. This means no battlecry</param>
        /// <param name="fromDeck">Whether or not it's from the player's deck, used in conjunction with forceSummoned</param>
        public void PlayCard(BaseCard card, IDamageableEntity subTarget, int gameboardPos = 0, bool forceSummoned = false, bool fromDeck = false)
        {
            // Is it even our turn to play?
            var gameState = GameEngine.GameState;
            if (gameState.CurrentPlayer != this)
            {
                throw new InvalidOperationException(string.Format("You can't play out of turn! It is currently {0}'s turn", gameState.CurrentPlayer));
            }

            // Check if it exists in the player's hand
            BaseCard cardInHand;
            if (forceSummoned && fromDeck)
            {
                // It was force summoned from the player's deck so check if it was there
                cardInHand = this.Deck.Cards.FirstOrDefault(c => c.Equals(card));
            }
            else
            {
                cardInHand = this.Hand.FirstOrDefault(c => c.Equals(card));    
            }
            
            if (cardInHand == null)
            {
                throw new InvalidOperationException(string.Format("You can't play a card that's not in hand (or deck if force summoned from there)! {0}", card));
            }

            // Check if we have enough mana to make the play
            if (!forceSummoned && this.Mana < cardInHand.CurrentManaCost)
            {
                throw new InvalidOperationException(string.Format("Not enough mana {0} to play that card {1}!", this.Mana, card.CurrentManaCost));
            }

            var minionCard = cardInHand as BaseMinion;
            if (minionCard != null)
            {
                this.PlayMinion(minionCard, subTarget, gameboardPos);
            }

            var spellCard = cardInHand as BaseSpell;
            if (spellCard != null)
            {
                this.PlaySpell(spellCard, subTarget);
            }

            var weaponCard = cardInHand as BaseWeapon;
            if (weaponCard != null)
            {
                this.PlayWeapon(weaponCard, subTarget);
            }

            // Register any triggered effects
            var triggeredEffectCard = cardInHand as ITriggeredEffectOwner;
            if (triggeredEffectCard != null)
            {
                triggeredEffectCard.RegisterEffect();
            }

            GameEngine.CheckForGameEnd();
        }

        /// <summary>
        /// Plays a minion onto the game board
        /// </summary>
        /// <param name="minion">The minion to be played on the game board</param>
        /// <param name="subTarget">The sub target for this card, usually for targetting battle cry spells</param>
        /// <param name="gameboardPos">The position on the gameboard to place the card</param>
        /// <param name="forceSummoned">Whether or not this card was force summoned. This means no battle cry</param>
        public void PlayMinion(BaseMinion minion, IDamageableEntity subTarget, int gameboardPos = 0, bool forceSummoned = false)
        {
            var gameState = GameEngine.GameState;

            // Check if there are too many minions on the board
            var playZone = gameState.CurrentPlayerPlayZone;
            var playZoneCount = playZone.Count(slot => slot != null);
            if (playZoneCount >= Constants.MAX_CARDS_ON_BOARD)
            {
                throw new InvalidOperationException(string.Format("There are too many cards ({0}) in the playzone!", playZoneCount));
            }

            // If there is a card already in the target gameboard position, shift everything to the right and place it there
            if (playZone[gameboardPos] != null)
            {
                for (int i = playZone.Count - 1; i > gameboardPos; i--)
                {
                    playZone[i] = playZone[i-1];
                }
            }

            // Set the time the card was played
            minion.TimePlayed = DateTime.Now;

            // Start the minion as exhausted unless it has charge
            if (!minion.HasCharge)
            {
                minion.ApplyStatusEffects(MinionStatusEffects.EXHAUSTED);
            }

            minion.ResetAttacksThisTurn();

            // Place the minion on the board
            playZone[gameboardPos] = minion;

            // Remove it from the player's hand
            this.Hand.Remove(minion);

            // Remove mana from the player
            this.Mana -= minion.CurrentManaCost;

            // Fire minion placed event
            if (!forceSummoned)
            {
                GameEventManager.MinionPlaced(minion);
            }

            // Register deathrattle if applicable
            var deathrattleCard = minion as IDeathrattler;
            if (deathrattleCard != null)
            {
                deathrattleCard.RegisterDeathrattle();
            }

            if (!forceSummoned)
            {
                // call the card's battlecry 
                var battlecryCard = minion as IBattlecry;
                if (battlecryCard != null)
                {
                    battlecryCard.Battlecry(subTarget);
                }
            }

            // Fire card played event
            GameEventManager.MinionPlayed(minion);
        }

        /// <summary>
        /// Plays a spell
        /// </summary>
        /// <param name="spell">The spell to play</param>
        /// <param name="subTarget">The sub target for this spell card if applicable</param>
        public void PlaySpell(BaseSpell spell, IDamageableEntity subTarget = null)
        {
            if (subTarget != null && subTarget is BaseMinion)
            {
                if (((BaseMinion) subTarget).IsImmuneToSpellTarget)
                {
                    throw new InvalidOperationException("Can't target minion that is immune to spell targeting");
                }
            }

            // Remove it from the player's hand
            this.Hand.Remove(spell);

            // Remove mana from the player
            this.Mana -= spell.CurrentManaCost;

            // Fire spell casting event
            bool shouldAbort;
            GameEventManager.SpellCasting(spell, subTarget, out shouldAbort);

            // Check if we need to abort the spell or redirect
            if (!shouldAbort)
            {
                spell.Activate(subTarget);
            }

            // Fire spell casted event (if we need to)
        }

        /// <summary>
        /// Plays a weapon
        /// </summary>
        /// <param name="weapon">The weapon to play</param>
        /// <param name="subTarget">The sub target for this weapon if applicable</param>
        public void PlayWeapon(BaseWeapon weapon, IDamageableEntity subTarget = null)
        {
            this.Hand.Remove(weapon);

            this.Mana -= weapon.CurrentManaCost;

            weapon.WeaponOwner = this;
            this.Weapon = weapon;

            // Register deathrattle if applicable
            var deathrattleCard = weapon as IDeathrattler;
            if (deathrattleCard != null)
            {
                deathrattleCard.RegisterDeathrattle();
            }

            // Call the card's battle cry
            var battlecryWeapon = weapon as IBattlecry;
            if (battlecryWeapon != null)
            {
                battlecryWeapon.Battlecry(subTarget);
            }

            // Fire card played event
            // TODO: Add weapon played event?
        }

        /// <summary>
        /// Draws a card from the player's deck and puts it into his hand
        /// </summary>
        public void DrawCard()
        {
            this.DrawCards(1);
        }

        /// <summary>
        /// Draws n cards from the player's deck and puts it into his hand
        /// </summary>
        /// <param name="n"></param>
        public void DrawCards(int n = 1)
        {
            var drawnCards = this.Deck.DrawCards(n);

            foreach (var drawnCard in drawnCards)
            {
                drawnCard.Owner = this;

                if (drawnCard is FatigueCard)
                {
                    // Oh noes!
                    this.TakeDamage(((FatigueCard)drawnCard).FatigueDamage);
                    GameEngine.CheckForGameEnd();
                }
                else
                {
                    // If the hand is full, mill the card (graveyard it)
                    if (this.Hand.Count >= Constants.MAX_CARDS_IN_HAND)
                    {
                        Logger.Instance.Info(string.Format("{0}: {1} was milled because the hand is too full!", this.LogString(), drawnCard));
                        this.Graveyard.Add(drawnCard);
                    }
                    else
                    {
                        Logger.Instance.Info(string.Format("{0}: Drew {1}", this.LogString(), drawnCard));
                        this.Hand.Add(drawnCard);
                    }
                }
            }
        }

        /// <summary>
        /// Applies the provided effects to the player
        /// </summary>
        /// <param name="effects">The effects to apply to the player</param>
        public void ApplyStatusEffects(PlayerStatusEffects effects)
        {
            this.StatusEffects |= effects;
        }

        /// <summary>
        /// Removes the provided effects from the player
        /// </summary>
        /// <param name="effects">The effects to remove</param>
        public void RemoveStatusEffects(PlayerStatusEffects effects)
        {
            this.StatusEffects = this.StatusEffects & ~effects;
        }

        /// <summary>
        /// This kills the player
        /// </summary>
        public void Die()
        {
            // Oh no, we died, inform the Game Engine.
            GameEngine.DeadPlayersThisTurn.Add(this);
        }

        /// <summary>
        /// Returns a list of playable cards given the player's current mana
        /// </summary>
        /// <returns>The list of playable cards given the player's current mana</returns>
        public List<BaseCard> GetPlayableCards()
        {
            return this.Hand.Where(c => c.OriginalManaCost <= this.Mana).ToList();
        }

        /// <summary>
        /// Returns whether or not the player is frozen
        /// </summary>
        public bool IsFrozen { get { return this.StatusEffects.HasFlag(PlayerStatusEffects.FROZEN); } }

        /// <summary>
        /// Returns whether or not the player is immune to damage
        /// </summary>
        public bool IsImmuneToDamage { get { return this.StatusEffects.HasFlag(PlayerStatusEffects.IMMUNE_TO_DAMAGE); } }

        /// <summary>
        /// Returns whether or not the player has windfury
        /// </summary>
        public bool HasWindfury { get { return this.StatusEffects.HasFlag(PlayerStatusEffects.WINDFURY); } }

        /// <summary>
        /// Returns whether or not the player can attack
        /// </summary>
        public bool CanAttack { get { return !this.StatusEffects.HasFlag(PlayerStatusEffects.EXHAUSTED); } }

        #region IAttacker

        public int GetCurrentAttackPower()
        {
            return this.TemporaryAttackBuff;
        }

        public void Attack(IDamageableEntity target)
        {
            if (this.Weapon != null)
            {
                this.Weapon.Attack(target);
            }
            else if (this.TemporaryAttackBuff > 0)
            {
                // Fire attacking event
                bool shouldAbort;
                GameEventManager.Attacking(this, target, isRetaliation: false, shouldAbort: out shouldAbort);
            }
            else
            {
                throw new InvalidOperationException("Player has no attack damage to attack with!");
            }

            this.attacksThisTurn++;

            if (this.HasWindfury && this.attacksThisTurn >= 2)
            {
                this.ApplyStatusEffects(PlayerStatusEffects.EXHAUSTED);
            }
        }
        
        /// <summary>
        /// Resets the number of attacks this minion has performed this turn.
        /// </summary>
        public void ResetAttacksThisTurn()
        {
            this.attacksThisTurn = 0;
        }

        #endregion

        #region IDamageableEntity

        public void TakeDamage(int damage)
        {
            if (this.IsImmuneToDamage) return;

            // Take damage to armor first
            int damageToTake = damage;
            int armorDamage = Math.Min(this.Armor, damageToTake);
            if (armorDamage > 0)
            {
                this.Armor -= armorDamage;
                damageToTake -= armorDamage;
            }

            if (damageToTake > 0)
            {
                this.Health -= damageToTake;
            }

            // Fire damage dealt event
            GameEventManager.DamageDealt(this, damage);

            if (this.Health <= 0)
            {
                this.Die();
            }
        }

        public void TakeHealing(int healAmount)
        {
            this.Health = Math.Min(this.Health + healAmount, 30);

            // Fire heal dealt event
        }

        public void TakeBuff(int attackBuff, int healthBuff)
        {
            throw new InvalidOperationException("Player's can't receive buffs.");
        }

        public void TakeTemporaryBuff(int attackBuff)
        {
            if (this.Weapon == null)
            {
                this.TemporaryAttackBuff += attackBuff;
            }
            else
            {
                this.Weapon.TakeTemporaryBuff(attackBuff);
            }
        }

        #endregion
        
        /// <summary>
        /// Returns a log friendly string for this player
        /// </summary>
        public string LogString()
        {
            return string.Format("{0}[{1}]", (this == GameEngine.GameState.Player) ? "Player" : "Opponent", this.Id);
        }

        #region IEquatable

        public bool Equals(BasePlayer other)
        {
            return this.Id == other.Id;
        }

        #endregion
    }

    /// <summary>
    /// Status effects for the player
    /// </summary>
    [Flags]
    public enum PlayerStatusEffects
    {
        FROZEN = 1,
        IMMUNE_TO_DAMAGE = 2,
        EXHAUSTED = 4,
        WINDFURY = 8
    }
}
