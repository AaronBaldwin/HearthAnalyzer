using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ancient of Lore
    /// 
    /// <b>Choose One -</b> Draw 2 cards; or Restore 5 Health.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AncientofLore : BaseMinion
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;

        public AncientofLore(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancient of Lore";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
