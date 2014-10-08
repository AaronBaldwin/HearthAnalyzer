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
        protected BaseWeapon()
        {
            this.Type = CardType.WEAPON;
        }

        /// <summary>
        /// Back reference to the player that owns and has this weapon equipped
        /// </summary>
        public BasePlayer WeaponOwner;

        /// <summary>
        /// The amount of durability remaining on this weapon.
        /// </summary>
        public int Durability;

        /// <summary>
        /// This kills the weapon
        /// </summary>
        public void Die()
        {
            this.WeaponOwner.Graveyard.Add(this);

            this.WeaponOwner.Weapon = null;
            this.WeaponOwner = null;

            GameEventManager.UnregisterForEvents(this);
            GameEngine.DeadCardsThisTurn.Add(this);
        }

        #region IAttacker

        public int GetCurrentAttackPower()
        {
            return this.CurrentAttackPower;
        }

        public virtual void Attack(IDamageableEntity target)
        {
            // Fire attacking event
            bool shouldAbort;
            GameEventManager.Attacking(this, target, isRetaliation: false, shouldAbort: out shouldAbort);

            if (!shouldAbort)
            {
                // Use up a durability charge
                this.TakeDamage(1);
                GameEngine.TriggerDeathrattles();
            }
        }

        #endregion

        public virtual void TakeDamage(int damage)
        {
            // Nobody cares about weapons taking damage yet so no need to fire an event
            this.Durability -= damage;

            if (this.Durability <= 0)
            {
                this.Die();
            }
        }

        public virtual void TakeHealing(int healAmount)
        {
            // Nobody cares about if a weapon got healed  yet so no need to fire an event
            this.Durability += healAmount;
        }

        public virtual void TakeBuff(int attackBuff, int healthBuff)
        {
            this.PermanentAttackBuff += attackBuff;

            this.Durability += healthBuff;
        }

        public void TakeTemporaryBuff(int attackBuff)
        {
            this.TemporaryAttackBuff += attackBuff;
        }
    }
}
