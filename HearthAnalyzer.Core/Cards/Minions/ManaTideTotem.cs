using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mana Tide Totem
    /// 
    /// At the end of your turn, draw a card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ManaTideTotem : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 3;

        public ManaTideTotem(int id = -1)
        {
            this.Id = id;
            this.Name = "Mana Tide Totem";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.TOTEM;
        }
    }
}
