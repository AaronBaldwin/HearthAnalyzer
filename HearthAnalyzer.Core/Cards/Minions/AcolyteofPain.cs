using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Acolyte of Pain
    /// 
    /// Whenever this minion takes damage, draw a card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AcolyteofPain : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 3;

        public AcolyteofPain(int id = -1)
        {
            this.Id = id;
            this.Name = "Acolyte of Pain";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
