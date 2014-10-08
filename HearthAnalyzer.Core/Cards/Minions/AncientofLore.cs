using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Interfaces;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ancient of Lore
    /// 
    /// <b>Choose One -</b> Draw 2 cards; or Restore 5 Health.
    /// </summary>
    public class AncientOfLore : BaseMinion, IMultiCardEffectMinion
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;
        private const int DRAW_COUNT = 2;
        private const int HEAL_AMOUNT = 5;

        public AncientOfLore(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancient of Lore";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        /// <summary>
        /// First Effect: Draw 2 cards
        /// Second Effect: Restore 5 Health
        /// </summary>
        /// <param name="cardEffect">The card effect to use</param>
        /// <param name="target">The target of the heal</param>
        public void UseCardEffect(CardEffect cardEffect, IDamageableEntity target = null)
        {
            if (cardEffect == CardEffect.FIRST)
            {
                // Draw cards
                this.Owner.DrawCards(DRAW_COUNT);
            }
            else if (cardEffect == CardEffect.SECOND)
            {
                // Heal
                if (target == null)
                {
                    throw new InvalidOperationException("Needs to have a target!");
                }

                target.TakeHealing(HEAL_AMOUNT);
            }
            else
            {
                throw new InvalidOperationException("You must choose a card effect to play it!");
            }
        }
    }
}
