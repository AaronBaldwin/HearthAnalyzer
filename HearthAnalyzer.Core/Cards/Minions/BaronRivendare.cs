using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Baron Rivendare
    /// 
    /// Your minions trigger their <b>Deathrattles</b> twice.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class BaronRivendare : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 7;

        public BaronRivendare(int id = -1)
        {
            this.Id = id;
            this.Name = "Baron Rivendare";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
