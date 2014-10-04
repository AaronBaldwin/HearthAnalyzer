using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Onyxia
    /// 
    /// <b>Battlecry:</b> Summon 1/1 Whelps until your side of the battlefield is full.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Onyxia : BaseMinion
    {
        private const int MANA_COST = 9;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public Onyxia(int id = -1)
        {
            this.Id = id;
            this.Name = "Onyxia";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DRAGON;
        }
    }
}
