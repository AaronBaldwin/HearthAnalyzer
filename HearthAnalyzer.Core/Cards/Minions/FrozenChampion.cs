using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Frozen Champion
    /// 
    /// Permanently Frozen.  Adjacent minions are Immune to Frost Breath.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class FrozenChampion : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 10;

        public FrozenChampion(int id = -1)
        {
            this.Id = id;
            this.Name = "Frozen Champion";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
