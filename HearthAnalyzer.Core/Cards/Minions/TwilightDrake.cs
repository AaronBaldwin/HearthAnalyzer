using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Twilight Drake
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class TwilightDrake : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 1;

        public TwilightDrake(int id = -1)
        {
            this.Id = id;
            this.Name = "Twilight Drake";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
