using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Aldor Peacekeeper
    /// 
    /// <b>Battlecry:</b> Change an enemy minion's Attack to 1.
    /// </summary>
    public class AldorPeacekeeper : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public AldorPeacekeeper(int id = -1)
        {
            this.Id = id;
            this.Name = "Aldor Peacekeeper";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            var targetMinion = subTarget as BaseMinion;
            if (targetMinion == null)
            {
                throw new InvalidOperationException("Target needs to be a minion!");
            }

            if (!GameEngine.GameState.WaitingPlayerPlayZone.Contains(targetMinion))
            {
                throw new InvalidOperationException("Target must be an enemy");
            }

            var attackToSubtract = targetMinion.CurrentAttackPower - 1;
            targetMinion.TakeBuff(-attackToSubtract, 0);
        }
    }
}
