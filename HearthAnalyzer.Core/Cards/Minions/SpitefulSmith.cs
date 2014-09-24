using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Spiteful Smith
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SpitefulSmith : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 6;

        public SpitefulSmith(int id = -1)
        {
            this.Id = id;
            this.Name = "Spiteful Smith";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
