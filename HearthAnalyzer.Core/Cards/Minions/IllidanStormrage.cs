using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Illidan Stormrage
    /// 
    /// Whenever you play a card, summon a 2/1 Flame of Azzinoth.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class IllidanStormrage : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 7;
        private const int HEALTH = 5;

        public IllidanStormrage(int id = -1)
        {
            this.Id = id;
            this.Name = "Illidan Stormrage";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
