using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Mogu'shan Warden
    /// 
    /// <b>Taunt</b>
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MogushanWarden : BaseMinion
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 7;

        public MogushanWarden(int id = -1)
        {
            this.Id = id;
            this.Name = "Mogu'shan Warden";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
