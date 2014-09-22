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
        /// <param name="gameboardPos">The position on the gameboard to place the card (if applicable)</param>
        /// <remarks>Only really needed for simulation. We can read the logs to determine where a card got placed.</remarks>
        public void PlayCard(BaseCard card, int gameboardPos = 0)
        {
            // Check if it exists in the player's hand
            var cardInHand = this.Hand.FirstOrDefault(c => c.Equals(card));
            if (cardInHand == null)
            {
                throw new InvalidOperationException(string.Format("You can't play a card that's not in hand! {0}", card));
            }

            // if so, remove it
            this.Hand.Remove(cardInHand);

            // then put it on the game board

            // call the card's battlecry 

            // Buff / Damage / kill check

            // place it on the board
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

        public void Attack(IDamageableEntity target, GameState gameState)
        {
            if (this.Weapon != null)
            {
                this.Weapon.Attack(target, gameState);
            }
        }

        #endregion

        #region IDamageableEntity

        public void TakeDamage(int damage)
        {
            this.Health -= damage;

            // Fire damage dealt event

            if (this.Health <= 0)
            {
                this.Die();
            }
        }

        public void TakeHealing(int healAmount)
        {
            this.Health += healAmount;

            // Fire heal dealt event
        }

        public void TakeBuff(int attackBuff, int healthBuff)
        {
            throw new InvalidOperationException("Player's can't receive buffs.");
        }

        #endregion
    }
}
