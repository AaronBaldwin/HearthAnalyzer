using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Poultryizer
    /// 
    /// At the start of your turn, transform a random minion into a 1/1 Chicken.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Poultryizer : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 3;

        public Poultryizer(int id = -1)
        {
            this.Id = id;
            this.Name = "Poultryizer";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
