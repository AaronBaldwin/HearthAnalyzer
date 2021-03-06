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
    public class StormpikeCommando : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;
        private const int BATTLECRY_DAMAGE = 2;

        public StormpikeCommando(int id = -1)
        {
            this.Id = id;
            this.Name = "Stormpike Commando";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            subTarget.TakeDamage(BATTLECRY_DAMAGE);
        }
    }
}
