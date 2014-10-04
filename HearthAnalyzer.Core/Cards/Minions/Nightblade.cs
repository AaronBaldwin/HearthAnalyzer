using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Nightblade
    /// 
    /// <b>Battlecry: </b>Deal 3 damage to the enemy hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Nightblade : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 4;

        public Nightblade(int id = -1)
        {
            this.Id = id;
            this.Name = "Nightblade";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
