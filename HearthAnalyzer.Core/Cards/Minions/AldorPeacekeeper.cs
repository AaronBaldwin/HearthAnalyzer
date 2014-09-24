using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Aldor Peacekeeper
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AldorPeacekeeper : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public AldorPeacekeeper(int id = -1)
        {
            this.Id = id;
            this.Name = "Aldor Peacekeeper";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
