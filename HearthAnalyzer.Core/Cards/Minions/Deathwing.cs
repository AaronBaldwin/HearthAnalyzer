using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Deathwing
    /// 
    /// <b>Battlecry:</b> Destroy all other minions and discard your hand.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Deathwing : BaseMinion
    {
        private const int MANA_COST = 10;
        private const int ATTACK_POWER = 12;
        private const int HEALTH = 12;

        public Deathwing(int id = -1)
        {
            this.Id = id;
            this.Name = "Deathwing";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DRAGON;
        }
    }
}
