using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mind Control Tech
    /// 
    /// <b>Battlecry:</b> If your opponent has 4 or more minions, take control of one at random.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MindControlTech : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public MindControlTech(int id = -1)
        {
            this.Id = id;
            this.Name = "Mind Control Tech";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
