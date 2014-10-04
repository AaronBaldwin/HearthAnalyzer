using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Venture Co. Mercenary
    /// 
    /// Your minions cost (3) more.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class VentureCoMercenary : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 7;
        private const int HEALTH = 6;

        public VentureCoMercenary(int id = -1)
        {
            this.Id = id;
            this.Name = "Venture Co. Mercenary";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
