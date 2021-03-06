using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Void Terror
    /// 
    /// <b>Battlecry:</b> Destroy the minions on either side of this minion and gain their Attack and Health.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class VoidTerror : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public VoidTerror(int id = -1)
        {
            this.Id = id;
            this.Name = "Void Terror";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
