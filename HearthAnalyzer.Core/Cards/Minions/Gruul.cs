using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Gruul
    /// 
    /// At the end of each turn, gain +1/+1 .
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Gruul : BaseMinion
    {
        private const int MANA_COST = 8;
        private const int ATTACK_POWER = 7;
        private const int HEALTH = 7;

        public Gruul(int id = -1)
        {
            this.Id = id;
            this.Name = "Gruul";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
