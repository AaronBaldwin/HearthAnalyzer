using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a weapon in Hearthstone
    /// </summary>
    public class BaseWeapon : BaseCard, IAttacker, IDamageableEntity
    {
        /// <summary>
        /// Back reference to the player that owns and has this weapon equipped
        /// </summary>
        public BasePlayer Owner;

        /// <summary>
        /// The amount of durability remaining on this weapon.
        /// </summary>
        public int Durability;

        /// <summary>
        /// This kills the weapon
        /// </summary>
        public void Die()
        {
            this.Owner.Graveyard.Add(this);

            this.Owner.Weapon = null;
            this.Owner = null;
        }

        #region IAttacker

        public void Attack(IDamageableEntity target, GameState gameState)
        {
            // Fire attacking event
            bool shouldAbort;
            GameEventManager.Attacking(this, target, gameState, isRetaliation: false, shouldAbort: out shouldAbort);

            if (!shouldAbort)
            {
                // Use up a durability charge
                this.TakeDamage(1);
            }
        }

        #endregion

        public void TakeDamage(int damage)
        {
            // Nobody cares about weapons taking damage yet so no need to fire an event
            this.Durability -= damage;

            if (this.Durability <= 0)
            {
                this.Die();
            }
        }

        public void TakeHealing(int healAmount)
        {
            // Nobody cares about if a weapon got healed  yet so no need to fire an event
            this.Durability += healAmount;
        }

        public void TakeBuff(int attackBuff, int healthBuff)
        {
            this.CurrentAttackPower += attackBuff;

            this.Durability += healthBuff;
        }
    }
}
