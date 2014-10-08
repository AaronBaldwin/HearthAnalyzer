using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Interfaces;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ancient of War
    /// 
    /// <b>Choose One</b> -\n+5 Attack; or +5 Health and <b>Taunt</b>.
    /// </summary>
    public class AncientOfWar : BaseMinion, IMultiCardEffectMinion
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;
        private const int ATTACK_BUFF = 5;
        private const int HEALTH_BUFF = 5;

        public AncientOfWar(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancient of War";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void UseCardEffect(CardEffect cardEffect, IDamageableEntity target = null)
        {
            if (cardEffect == CardEffect.FIRST)
            {
                this.TakeBuff(ATTACK_BUFF, 0);
            }
            else if (cardEffect == CardEffect.SECOND)
            {
                this.TakeBuff(0, HEALTH_BUFF);
                this.ApplyStatusEffects(MinionStatusEffects.TAUNT);
            }
            else
            {
                throw new InvalidOperationException("You must choose a card effect to play!");
            }
        }
    }
}
