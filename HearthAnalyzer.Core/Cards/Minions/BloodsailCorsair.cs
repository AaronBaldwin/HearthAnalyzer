using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Bloodsail Corsair
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class BloodsailCorsair : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 2;

        public BloodsailCorsair(int id = -1)
        {
            this.Id = id;
            this.Name = "Bloodsail Corsair";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
