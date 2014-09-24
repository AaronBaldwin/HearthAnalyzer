using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Elite Tauren Chieftain
    /// Basic Minion
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class EliteTaurenChieftain : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;

        public EliteTaurenChieftain(int id = -1)
        {
            this.Id = id;
            this.Name = "Elite Tauren Chieftain";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }
    }
}