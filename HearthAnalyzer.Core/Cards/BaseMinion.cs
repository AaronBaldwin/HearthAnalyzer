using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a minion at the most basic level
    /// </summary>
    public abstract class BaseMinion : BaseCard
    {
        /// <summary>
        /// The maximum health of this card
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// The bonus health of this card
        /// </summary>
        public int BonusHealth;

        /// <summary>
        /// The current health of this card
        /// </summary>
        public int CurrentHealth;

        /// <summary>
        /// Represents the current status effects applied to the card
        /// </summary>
        public MinionStatusEffects StatusEffects;

        /// <summary>
        /// Applies the provided effects to the minion
        /// </summary>
        /// <param name="effects">The effects to apply</param>
        public void ApplyStatusEffects(MinionStatusEffects effects)
        {
            this.StatusEffects |= effects;
        }

        /// <summary>
        /// Called when a minion is silenced
        /// </summary>
        public void Silence()
        {
            Logger.Instance.Debug(string.Format("Minion {0}[{1}] has been silenced!", this.Name, this.Id));

            // Unregister all event listeners, including death rattles
            GameEventManager.UnregisterForEvents(this, unregisterDeathRattle: true);

            // Remove any effects
            this.StatusEffects = MinionStatusEffects.EXHAUSTED;

            // Remove any bonus health
            this.CurrentHealth = Math.Min(this.MaxHealth - this.BonusHealth, this.CurrentHealth);

            // Reset the attack power to its original value
            this.CurrentAttackPower = this.OriginalAttackPower;
        }

        /// <summary>
        /// Called when a minion dies
        /// </summary>
        public void Die()
        {
            Logger.Instance.Debug(string.Format("Minion {0}[{1}] has died with {2} health remaining.", this.Name, this.Id, this.CurrentHealth));

            // Unregister all event listeners except for deathrattle
            GameEventManager.UnregisterForEvents(this);

            // Add to GameEngine's list of minion's dead for deathrattle handling
            GameEngine.DeadMinionsThisTurn.Add(this);

            // Let GameEngine clean up minions from the baord
        }
    }

    /// <summary>
    /// Represents the types of status effects that can be applied to a card
    /// </summary>
    [Flags]
    public enum MinionStatusEffects
    {
        DIVINE_SHIELD = 0,
        CANT_ATTACK = 1,
        TAUNT = 2,
        STEALTHED = 4,
        EXHAUSTED = 8,
        WINDFURY = 16,
        FROZEN = 32,
        SILENCED = 64,
        IMMUNE_TO_DEATH = 128,
        IMMUNE_TO_DAMAGE = 256
    }
}
