using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Stormpike Commando
    /// Basic Minion
    /// </summary>
    public class StormpikeCommando : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;
        private const int BATTLECRY_DAMAGE = 2;

        public StormpikeCommando(int id = -1)
        {
            this.Id = id;
            this.Name = "Stormpike Commando";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }

        #region IBattlecry

        /// <summary>
        /// Battlecry: Deal 2 damage
        /// </summary>
        /// <param name="subTarget">The target to deal damage to</param>
        public void Battlecry(IDamageableEntity subTarget)
        {
            var minion = subTarget as BaseMinion;
            if (minion != null)
            {
                if (minion.IsStealthed)
                {
                    throw new InvalidOperationException(string.Format("{0} can't target stealthed minions with his battlecry!", this.Name));
                }
            }

            subTarget.TakeDamage(BATTLECRY_DAMAGE);
        }

        #endregion
    }
}
