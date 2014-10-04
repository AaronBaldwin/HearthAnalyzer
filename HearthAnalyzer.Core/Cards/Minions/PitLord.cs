using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Pit Lord
    /// 
    /// <b>Battlecry:</b> Deal 5 damage to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class PitLord : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 6;

        public PitLord(int id = -1)
        {
            this.Id = id;
            this.Name = "Pit Lord";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
