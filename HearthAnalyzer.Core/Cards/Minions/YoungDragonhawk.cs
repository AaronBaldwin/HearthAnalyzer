using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Young Dragonhawk
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class YoungDragonhawk : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 1;

        public YoungDragonhawk(int id = -1)
        {
            this.Id = id;
            this.Name = "Young Dragonhawk";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
