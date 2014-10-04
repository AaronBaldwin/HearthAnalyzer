using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Dread Infernal
    /// 
    /// <b>Battlecry:</b> Deal 1 damage to ALL other characters.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class DreadInfernal : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 6;
        private const int HEALTH = 6;

        public DreadInfernal(int id = -1)
        {
            this.Id = id;
            this.Name = "Dread Infernal";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
