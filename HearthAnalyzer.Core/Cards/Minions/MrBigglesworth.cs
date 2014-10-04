using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mr. Bigglesworth
    /// 
    /// <i>This is Kel'Thuzad's kitty.</i>
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MrBigglesworth : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 1;

        public MrBigglesworth(int id = -1)
        {
            this.Id = id;
            this.Name = "Mr. Bigglesworth";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }
    }
}
