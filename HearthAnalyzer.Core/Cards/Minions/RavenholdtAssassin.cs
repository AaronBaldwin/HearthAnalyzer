using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ravenholdt Assassin
    /// 
    /// <b>Stealth</b>
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class RavenholdtAssassin : BaseMinion
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 7;
        private const int HEALTH = 5;

        public RavenholdtAssassin(int id = -1)
        {
            this.Id = id;
            this.Name = "Ravenholdt Assassin";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
