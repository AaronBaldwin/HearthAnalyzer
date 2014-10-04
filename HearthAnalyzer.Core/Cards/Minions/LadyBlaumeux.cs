using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Lady Blaumeux
    /// 
    /// Your hero is <b>Immune</b>.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class LadyBlaumeux : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 7;

        public LadyBlaumeux(int id = -1)
        {
            this.Id = id;
            this.Name = "Lady Blaumeux";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
