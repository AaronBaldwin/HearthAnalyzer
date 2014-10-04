using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ragnaros the Firelord
    /// 
    /// Can't Attack.  At the end of your turn, deal 8 damage to a random enemy.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class RagnarostheFirelord : BaseMinion
    {
        private const int MANA_COST = 8;
        private const int ATTACK_POWER = 8;
        private const int HEALTH = 8;

        public RagnarostheFirelord(int id = -1)
        {
            this.Id = id;
            this.Name = "Ragnaros the Firelord";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
