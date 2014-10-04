using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Zombie Chow
    /// 
    /// <b>Deathrattle:</b> Restore 5 Health to the enemy hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ZombieChow : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 3;

        public ZombieChow(int id = -1)
        {
            this.Id = id;
            this.Name = "Zombie Chow";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
