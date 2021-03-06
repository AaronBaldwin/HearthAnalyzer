using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Voidcaller
    /// 
    /// <b>Deathrattle:</b> Put a random Demon from your hand into the battlefield.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Voidcaller : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 4;

        public Voidcaller(int id = -1)
        {
            this.Id = id;
            this.Name = "Voidcaller";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
