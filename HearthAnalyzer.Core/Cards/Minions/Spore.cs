using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Spore
    /// 
    /// <b>Deathrattle:</b> Give all enemy minions +8 Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Spore : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 1;

        public Spore(int id = -1)
        {
            this.Id = id;
            this.Name = "Spore";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
