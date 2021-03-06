using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Flametongue Totem
    /// 
    /// Adjacent minions have +2 Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class FlametongueTotem : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 3;

        public FlametongueTotem(int id = -1)
        {
            this.Id = id;
            this.Name = "Flametongue Totem";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.TOTEM;
        }
    }
}
