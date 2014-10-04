using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ancient Watcher
    /// 
    /// Can't Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AncientWatcher : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 5;

        public AncientWatcher(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancient Watcher";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
