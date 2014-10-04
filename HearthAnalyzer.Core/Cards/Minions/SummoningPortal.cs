using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Summoning Portal
    /// 
    /// Your minions cost (2) less, but not less than (1).
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SummoningPortal : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 4;

        public SummoningPortal(int id = -1)
        {
            this.Id = id;
            this.Name = "Summoning Portal";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
