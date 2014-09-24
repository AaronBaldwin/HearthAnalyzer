using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Southsea Captain
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SouthseaCaptain : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public SouthseaCaptain(int id = -1)
        {
            this.Id = id;
            this.Name = "Southsea Captain";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}