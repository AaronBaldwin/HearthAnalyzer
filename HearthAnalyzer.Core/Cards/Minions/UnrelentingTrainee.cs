using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Unrelenting Trainee
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class UnrelentingTrainee : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 2;

        public UnrelentingTrainee(int id = -1)
        {
            this.Id = id;
            this.Name = "Unrelenting Trainee";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
