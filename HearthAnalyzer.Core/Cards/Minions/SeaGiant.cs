using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Sea Giant
    /// 
    /// Costs (1) less for each other minion on the battlefield.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SeaGiant : BaseMinion
    {
        private const int MANA_COST = 10;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public SeaGiant(int id = -1)
        {
            this.Id = id;
            this.Name = "Sea Giant";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
