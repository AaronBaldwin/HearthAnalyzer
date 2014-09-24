using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Injured Blademaster
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class InjuredBlademaster : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 7;

        public InjuredBlademaster(int id = -1)
        {
            this.Id = id;
            this.Name = "Injured Blademaster";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}