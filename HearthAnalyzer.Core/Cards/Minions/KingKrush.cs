using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the King Krush
    /// 
    /// <b>Charge</b>
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class KingKrush : BaseMinion
    {
        private const int MANA_COST = 9;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public KingKrush(int id = -1)
        {
            this.Id = id;
            this.Name = "King Krush";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }
    }
}
