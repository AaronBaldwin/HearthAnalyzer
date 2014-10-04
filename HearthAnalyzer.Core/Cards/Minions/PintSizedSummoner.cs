using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Pint-Sized Summoner
    /// 
    /// The first minion you play each turn costs (1) less.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class PintSizedSummoner : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 2;

        public PintSizedSummoner(int id = -1)
        {
            this.Id = id;
            this.Name = "Pint-Sized Summoner";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
