using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Gelbin Mekkatorque
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class GelbinMekkatorque : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 6;
        private const int HEALTH = 6;

        public GelbinMekkatorque(int id = -1)
        {
            this.Id = id;
            this.Name = "Gelbin Mekkatorque";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
