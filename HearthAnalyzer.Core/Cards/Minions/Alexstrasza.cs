using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Alexstrasza
    /// 
    /// <b>Battlecry:</b> Set a hero's remaining Health to 15.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Alexstrasza : BaseMinion
    {
        private const int MANA_COST = 9;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public Alexstrasza(int id = -1)
        {
            this.Id = id;
            this.Name = "Alexstrasza";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DRAGON;
        }
    }
}
