using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents a player in Hearthstone
    /// </summary>
    public abstract class BasePlayer : IDamageableEntity, IAttacker
    {
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
        public List<BaseCard> Deck;

        /// <summary>
        /// The player's graveyard
        /// </summary>
        public List<BaseCard> Graveyard;

        /// <summary>
        /// The player's health
        /// </summary>
        public int Health = 30;

        /// <summary>
        /// The player's armor value
        /// </summary>
        public int Armor;

        /// <summary>
        /// The player's remaining mana
        /// </summary>
        public int Mana = 1;

        /// <summary>
        /// The player's maximum mana
        /// </summary>
        public int MaxMana = 1;

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
        /// <remarks>Only really needed for simulation. We can read the logs to determine where a card got placed.</remarks>
        public void PlayCard(BaseCard card, IDamageableEntity subTarget, int gameboardPos = 0)
        {
            // Is it even our turn to play?
            var gameState = GameEngine.GameState;
            if (gameState.CurrentPlayer != this)
            {
                throw new InvalidOperationException(string.Format("You can't play out of turn! It is currently {0}'s turn", gameState.CurrentPlayer));
            }

            // Check if it exists in the player's hand
            var cardInHand = this.Hand.FirstOrDefault(c => c.Equals(card));
            if (cardInHand == null)
            {
                throw new InvalidOperationException(string.Format("You can't play a card that's not in hand! {0}", card));
            }

            // Check if we have enough mana to make the play
            if (this.Mana < cardInHand.CurrentManaCost)
            {
                throw new InvalidOperationException(string.Format("Not enough mana {0} to play that card {1}!", this.Mana, card.CurrentManaCost));
            }

            // Check if there are too many minions on the board
            var playZone = gameState.CurrentPlayerPlayZone;
            var playZoneCount = playZone.Count(slot => slot != null);
            if (playZoneCount >= GameBoard.MAX_CARDS_IN_PLAY_ZONE)
            {
                throw new InvalidOperationException(string.Format("There are too many cards ({0}) in the playzone!", playZoneCount));
            }

            // If there is a card already in the target gameboard position, shift everything to the right and place it there
            if (playZone[gameboardPos] != null)
            {
                for (int i = playZone.Count - 1; i > gameboardPos; i--)
                {
                    playZone[i] = playZone[i - 1];
                }
            }

            playZone[gameboardPos] = cardInHand;

            // Remove it from the player's hand
            this.Hand.Remove(cardInHand);

            // Remove mana from the player
            this.Mana -= cardInHand.CurrentManaCost;

            // Fire card placed event

            // call the card's battlecry 
            var battlecryCard = card as IBattlecry;
            if (battlecryCard != null)
            {
                battlecryCard.Battlecry(subTarget);
            }

            // Fire card played event

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
        /// Status effects for the player
        /// </summary>
        [Flags]
        public enum PlayerStatusEffects
        {
            FROZEN = 0,
            IMMUNE_TO_DAMAGE = 1
        }

        #region IAttacker

        public void Attack(IDamageableEntity target)
        {
            if (this.Weapon != null)
            {
                this.Weapon.Attack(target);
            }
        }

        #endregion

        #region IDamageableEntity

        public void TakeDamage(int damage)
        {
            if (this.IsImmuneToDamage) return;

            this.Health -= damage;

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

        #endregion
    }
}
