using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Emboldener 3000
    /// 
    /// At the end of your turn, give a random minion +1/+1.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Emboldener3000 : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 4;

        public Emboldener3000(int id = -1)
        {
            this.Id = id;
            this.Name = "Emboldener 3000";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
