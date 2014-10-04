using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Molten Giant
    /// 
    /// Costs (1) less for each damage your hero has taken.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MoltenGiant : BaseMinion
    {
        private const int MANA_COST = 20;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public MoltenGiant(int id = -1)
        {
            this.Id = id;
            this.Name = "Molten Giant";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
