using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Stormpike Commando
    /// 
    /// <b>Battlecry:</b> Deal 2 damage.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class StormpikeCommando : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;

        public StormpikeCommando(int id = -1)
        {
            this.Id = id;
            this.Name = "Stormpike Commando";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
