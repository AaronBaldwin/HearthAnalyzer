using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Shadow of Nothing
    /// 
    /// Mindgames whiffed! Your opponent had no minions!
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ShadowofNothing : BaseMinion
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 1;

        public ShadowofNothing(int id = -1)
        {
            this.Id = id;
            this.Name = "Shadow of Nothing";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
