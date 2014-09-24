using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Stampeding Kodo
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class StampedingKodo : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 5;

        public StampedingKodo(int id = -1)
        {
            this.Id = id;
            this.Name = "Stampeding Kodo";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
