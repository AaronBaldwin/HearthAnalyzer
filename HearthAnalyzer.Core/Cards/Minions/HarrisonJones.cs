using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Harrison Jones
    /// 
    /// <b>Battlecry:</b> Destroy your opponent's weapon and draw cards equal to its Durability.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HarrisonJones : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 4;

        public HarrisonJones(int id = -1)
        {
            this.Id = id;
            this.Name = "Harrison Jones";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
