using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mukla's Big Brother
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MuklasBigBrother : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 10;
        private const int HEALTH = 10;

        public MuklasBigBrother(int id = -1)
        {
            this.Id = id;
            this.Name = "Mukla's Big Brother";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
