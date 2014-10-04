using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Guardian of Kings
    /// 
    /// <b>Battlecry:</b> Restore 6 Health to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class GuardianofKings : BaseMinion
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 6;

        public GuardianofKings(int id = -1)
        {
            this.Id = id;
            this.Name = "Guardian of Kings";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
