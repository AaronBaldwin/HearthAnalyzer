using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mountain Giant
    /// 
    /// Costs (1) less for each other card in your hand.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MountainGiant : BaseMinion
    {
        private const int MANA_COST = 12;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public MountainGiant(int id = -1)
        {
            this.Id = id;
            this.Name = "Mountain Giant";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
