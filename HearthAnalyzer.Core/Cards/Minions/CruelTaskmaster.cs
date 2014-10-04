using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Cruel Taskmaster
    /// 
    /// <b>Battlecry:</b> Deal 1 damage to a minion and give it +2 Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class CruelTaskmaster : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 2;

        public CruelTaskmaster(int id = -1)
        {
            this.Id = id;
            this.Name = "Cruel Taskmaster";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
