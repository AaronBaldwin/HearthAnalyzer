using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Homing Chicken
    /// 
    /// At the start of your turn, destroy this minion and draw 3 cards.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HomingChicken : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 1;

        public HomingChicken(int id = -1)
        {
            this.Id = id;
            this.Name = "Homing Chicken";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
