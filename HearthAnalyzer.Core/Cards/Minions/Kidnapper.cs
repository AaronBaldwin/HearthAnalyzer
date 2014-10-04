using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Kidnapper
    /// 
    /// <b>Combo:</b> Return a minion to its owner's hand.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Kidnapper : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 3;

        public Kidnapper(int id = -1)
        {
            this.Id = id;
            this.Name = "Kidnapper";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
