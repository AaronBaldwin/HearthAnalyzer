using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Flame Imp
    /// 
    /// <b>Battlecry:</b> Deal 3 damage to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class FlameImp : BaseMinion
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 2;

        public FlameImp(int id = -1)
        {
            this.Id = id;
            this.Name = "Flame Imp";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
