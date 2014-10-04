using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Unrelenting Rider
    /// 
    /// <b>Deathrattle:</b> Summon a Spectral Rider for your opponent.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class UnrelentingRider : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 6;

        public UnrelentingRider(int id = -1)
        {
            this.Id = id;
            this.Name = "Unrelenting Rider";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
