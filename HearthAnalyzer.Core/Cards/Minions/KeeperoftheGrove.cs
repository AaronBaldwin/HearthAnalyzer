using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Keeper of the Grove
    /// 
    /// <b>Choose One</b> - Deal 2 damage; or <b>Silence</b> a minion.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class KeeperoftheGrove : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 4;

        public KeeperoftheGrove(int id = -1)
        {
            this.Id = id;
            this.Name = "Keeper of the Grove";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
