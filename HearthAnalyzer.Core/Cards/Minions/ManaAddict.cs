using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mana Addict
    /// 
    /// Whenever you cast a spell, gain +2 Attack this turn.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ManaAddict : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 3;

        public ManaAddict(int id = -1)
        {
            this.Id = id;
            this.Name = "Mana Addict";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
