using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Tundra Rhino
    /// 
    /// Your Beasts have <b>Charge</b>.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class TundraRhino : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 5;

        public TundraRhino(int id = -1)
        {
            this.Id = id;
            this.Name = "Tundra Rhino";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }
    }
}
