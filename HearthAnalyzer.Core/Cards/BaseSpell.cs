using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Interfaces;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents the base spell card
    /// </summary>
    public abstract class BaseSpell : BaseCard
    {
        protected BaseSpell()
        {
            this.Type = CardType.SPELL;
        }

        /// <summary>
        /// The amount of bonus spell power to add
        /// </summary>
        public int BonusSpellPower
        {
            get { return this.Owner.BonusSpellPower; }
        }

        /// <summary>
        /// Activates the spell card with an optional target
        /// </summary>
        /// <param name="target">The target for the spell card if applicable</param>
        /// <param name="cardEffect">The card effect to use</param>
        public abstract void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE);

        /// <summary>
        /// Heals a target
        /// </summary>
        /// <param name="target">The target to heal</param>
        /// <param name="healAmount">The amount to heal for</param>
        protected void HealTarget(IDamageableEntity target, int healAmount)
        {
            if (target == null) return;

            // Ok, we have to do some hacky stuff here. Heals don't get affected by spell power UNLESS the heal actually does
            // damage instead. Auchenai Soulpriest's current implementation can't handle this nuance right now.
            // So instead of going through the normal flow, change the "heal amount" based on whether or not the current player
            // has a non-silenced Auchenai Soulpriest.
            int actualHealAmount = healAmount;

            var playZone = GameEngine.GameState.CurrentPlayerPlayZone;
            if (playZone.Any(card => card is AuchenaiSoulpriest && !((BaseMinion)card).IsSilenced))
            {
                actualHealAmount += this.BonusSpellPower;
            }

            bool shouldAbort;
            GameEventManager.Healing(this.Owner, target, actualHealAmount, out shouldAbort);

            if (!shouldAbort)
            {
                target.TakeHealing(actualHealAmount);
            }
        }
    }
}
