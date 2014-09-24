using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Stranglethorn Tiger
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class StranglethornTiger : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;

        public StranglethornTiger(int id = -1)
        {
            this.Id = id;
            this.Name = "Stranglethorn Tiger";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}
