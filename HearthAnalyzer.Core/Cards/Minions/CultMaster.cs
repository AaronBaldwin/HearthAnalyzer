using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Cult Master
    /// 
    /// Whenever one of your other minions dies, draw a card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class CultMaster : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;

        public CultMaster(int id = -1)
        {
            this.Id = id;
            this.Name = "Cult Master";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
