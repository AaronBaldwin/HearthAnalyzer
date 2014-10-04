using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Barrel
    /// 
    /// Is something in this barrel?
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Barrel : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 2;

        public Barrel(int id = -1)
        {
            this.Id = id;
            this.Name = "Barrel";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
