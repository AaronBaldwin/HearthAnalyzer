using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Secretkeeper
    /// 
    /// Whenever a <b>Secret</b> is played, gain +1/+1.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Secretkeeper : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 2;

        public Secretkeeper(int id = -1)
        {
            this.Id = id;
            this.Name = "Secretkeeper";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
