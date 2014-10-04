using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Healing Totem
    /// 
    /// At the end of your turn, restore 1 Health to all friendly minions.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HealingTotem : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 2;

        public HealingTotem(int id = -1)
        {
            this.Id = id;
            this.Name = "Healing Totem";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.TOTEM;
        }
    }
}
