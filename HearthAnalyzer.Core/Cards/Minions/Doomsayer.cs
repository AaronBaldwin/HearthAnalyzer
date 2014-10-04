using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Doomsayer
    /// 
    /// At the start of your turn, destroy ALL minions.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Doomsayer : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 7;

        public Doomsayer(int id = -1)
        {
            this.Id = id;
            this.Name = "Doomsayer";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
