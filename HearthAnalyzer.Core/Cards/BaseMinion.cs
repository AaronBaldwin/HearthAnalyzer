﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Interfaces;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a minion at the most basic level
    /// </summary>
    public abstract class BaseMinion : BaseCard, IAttacker, IDamageableEntity
    {
        internal int attacksThisTurn = 0;

        protected BaseMinion()
        {
            this.Type = CardType.NORMAL_MINION;
        }

        /// <summary>
        /// The maximum health of this card
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// The bonus health of this card
        /// </summary>
        public int BonusHealth;

        /// <summary>
        /// The current health of this card
        /// </summary>
        public int CurrentHealth;

        /// <summary>
        /// The amount of bonus spell power this minion adds
        /// </summary>
        public int BonusSpellPower;

        /// <summary>
        /// Represents the current status effects applied to the card
        /// </summary>
        public MinionStatusEffects StatusEffects;

        /// <summary>
        /// Whether or not this minion has Divine Shield
        /// </summary>
        public bool HasDivineShield { get { return this.StatusEffects.HasFlag(MinionStatusEffects.DIVINE_SHIELD); } }

        /// <summary>
        /// Whether or not this minion is frozen
        /// </summary>
        public bool IsFrozen { get { return this.StatusEffects.HasFlag(MinionStatusEffects.FROZEN); } }

        /// <summary>
        /// Whether or not this minion is exhausted
        /// </summary>
        public bool IsExhausted { get { return this.StatusEffects.HasFlag(MinionStatusEffects.EXHAUSTED); } }

        /// <summary>
        /// Whether or not this minion can attack
        /// </summary>
        public bool CanAttack 
        { 
            get
            {
                return !this.IsFrozen
                    && !this.StatusEffects.HasFlag(MinionStatusEffects.CANT_ATTACK)
                    && !this.IsExhausted;
            }
        }

        /// <summary>
        /// Whether or not this minion has taunt
        /// </summary>
        public bool HasTaunt { get { return this.StatusEffects.HasFlag(MinionStatusEffects.TAUNT); } }

        /// <summary>
        /// Whether or not this minion is stealthed
        /// </summary>
        public bool IsStealthed { get { return this.StatusEffects.HasFlag(MinionStatusEffects.STEALTHED); } }

        /// <summary>
        /// Whether or not this minion has windfury
        /// </summary>
        public bool HasWindfury { get { return this.StatusEffects.HasFlag(MinionStatusEffects.WINDFURY); } }

        /// <summary>
        /// Whether or not this minion was silenced
        /// </summary>
        public bool IsSilenced { get { return this.StatusEffects.HasFlag(MinionStatusEffects.SILENCED); } }

        /// <summary>
        /// Whether or not this minion is immune to death
        /// </summary>
        public bool IsImmuneToDeath { get { return this.StatusEffects.HasFlag(MinionStatusEffects.IMMUNE_TO_DEATH); } }

        /// <summary>
        /// Whether or not this minion is immune to damage
        /// </summary>
        public bool IsImmuneToDamage { get { return this.StatusEffects.HasFlag(MinionStatusEffects.IMMUNE_TO_DAMAGE); } }

        /// <summary>
        /// Whether or not the minion has charge
        /// </summary>
        public bool HasCharge { get { return this.StatusEffects.HasFlag(MinionStatusEffects.CHARGE); } }

        /// <summary>
        /// Whether or not the minion is immune to spell targeting
        /// </summary>
        public bool IsImmuneToSpellTarget { get { return this.StatusEffects.HasFlag(MinionStatusEffects.IMMUNE_TO_SPELL_TARGET); } }

        /// <summary>
        /// Applies the provided effects to the minion
        /// </summary>
        /// <param name="effects">The effects to apply</param>
        public void ApplyStatusEffects(MinionStatusEffects effects)
        {
            if (effects.HasFlag(MinionStatusEffects.WINDFURY))
            {
                // If we're applying windfury, we need to unexhaust the minion if it has
                // only attacked once so far this turn
                if (this.attacksThisTurn < 2)
                {
                    this.RemoveStatusEffects(MinionStatusEffects.EXHAUSTED);
                }
            }

            this.StatusEffects |= effects;
        }

        /// <summary>
        /// Removes the specified effects from the minion
        /// </summary>
        /// <param name="effects">The effects to remove</param>
        public void RemoveStatusEffects(MinionStatusEffects effects)
        {
            this.StatusEffects = this.StatusEffects & ~effects;
        }

        /// <summary>
        /// Called when a minion is silenced
        /// </summary>
        public void Silence()
        {
            Logger.Instance.Debug(string.Format("Minion {0}[{1}] has been silenced!", this.Name, this.Id));

            // Unregister all event listeners, including death rattles
            GameEventManager.UnregisterForEvents(this);
            GameEngine.UnregisterDeathrattle(this);

            // Remove any effects but keep it exhausted if it was already
            bool wasExhausted = this.StatusEffects.HasFlag(MinionStatusEffects.EXHAUSTED);
            this.StatusEffects = MinionStatusEffects.SILENCED | (wasExhausted ? MinionStatusEffects.EXHAUSTED : 0);

            // Remove any bonus health
            this.CurrentHealth = Math.Min(this.MaxHealth - this.BonusHealth, this.CurrentHealth);

            // Reset the attack power to its original value
            this.PermanentAttackBuff = 0;
            this.TemporaryAttackBuff = 0;
        }

        /// <summary>
        /// Called when a minion dies
        /// </summary>
        public void Die()
        {
            Logger.Instance.Debug(string.Format("Minion {0}[{1}] has died with {2} health remaining.", this.Name, this.Id, this.CurrentHealth));

            // Unregister all event listeners except for deathrattle
            GameEventManager.UnregisterForEvents(this);

            // Add to GameEngine's list of minion's dead for deathrattle handling
            GameEngine.DeadCardsThisTurn.Add(this);

            // Let GameEngine clean up minions from the baord
        }

        public int GetCurrentAttackPower()
        {
            return this.CurrentAttackPower;
        }

        /// <summary>
        /// Attacks another target
        /// </summary>
        /// <param name="target">The target to attack</param>
        public void Attack(IDamageableEntity target)
        {
            // Make sure the minion isn't exhausted first or can't attack
            if (!this.CanAttack)
            {
                throw new InvalidOperationException("This minion can't attack yet!");
            }

            // Make sure we're not attacking through a taunt
            var enemyPlayZone = GameEngine.GameState.WaitingPlayerPlayZone;
            var targetMinion = target as BaseMinion;
            if (((targetMinion != null && !targetMinion.HasTaunt) || target is BasePlayer) &&
                enemyPlayZone.Any(minion => minion != null && ((BaseMinion) minion).HasTaunt))
            {
                throw new InvalidOperationException("Can't attack through taunt!");
            }

            if (targetMinion != null && targetMinion.IsStealthed)
            {
                throw new InvalidOperationException("Can't attack a minion that is stealthed!");
            }

            if (this.IsStealthed)
            {
                this.RemoveStatusEffects(MinionStatusEffects.STEALTHED);
            }

            this.attacksThisTurn++;

            // Fire attacking event
            bool shouldAbort;
            GameEventManager.Attacking(this, target, isRetaliation: false, shouldAbort: out shouldAbort);

            if (!this.HasWindfury || (this.HasWindfury && this.attacksThisTurn >= 2))
            {
                this.ApplyStatusEffects(MinionStatusEffects.EXHAUSTED);
            }
        }

        /// <summary>
        /// Resets the number of attacks this minion has performed this turn.
        /// </summary>
        public void ResetAttacksThisTurn()
        {
            this.attacksThisTurn = 0;
        }

        #region IDamageableEntity

        public void TakeDamage(int damage)
        {
            if (!this.IsImmuneToDamage)
            {
                if (this.HasDivineShield)
                {
                    this.RemoveStatusEffects(MinionStatusEffects.DIVINE_SHIELD);
                    return;
                }

                this.CurrentHealth -= damage;

                // fire damage dealt event
                GameEventManager.DamageDealt(this, damage);

                // Check if we should enrage
                if (this is IEnragable && this.CurrentHealth < this.MaxHealth)
                {
                    ((IEnragable)this).Enrage();
                }

                if (this.CurrentHealth <= 0 && !this.IsImmuneToDeath)
                {
                    this.Die();
                }
            }
        }

        public void TakeHealing(int healAmount)
        {
            this.CurrentHealth = Math.Min(this.CurrentHealth + healAmount, this.MaxHealth);

            // fire healing dealt event

            // Check if we need to be un-enraged
            if (this is IEnragable && this.CurrentHealth == this.MaxHealth)
            {
                ((IEnragable)this).Derage();
            }
        }

        public void TakeBuff(int attackBuff, int healthBuff)
        {
            this.PermanentAttackBuff += attackBuff;

            this.MaxHealth += healthBuff;
            this.CurrentHealth = Math.Min(this.CurrentHealth + healthBuff, this.MaxHealth);
        }

        public void TakeTemporaryBuff(int attackBuff)
        {
            this.TemporaryAttackBuff += attackBuff;
        }

        #endregion
    }

    /// <summary>
    /// Represents the types of status effects that can be applied to a card
    /// </summary>
    [Flags]
    public enum MinionStatusEffects
    {
        DIVINE_SHIELD = 1,
        CANT_ATTACK = 2,
        TAUNT = 4,
        STEALTHED = 8,
        EXHAUSTED = 16,
        WINDFURY = 32,
        FROZEN = 64,
        SILENCED = 128,
        IMMUNE_TO_DEATH = 256,
        IMMUNE_TO_DAMAGE = 512,
        CHARGE = 1024,
        IMMUNE_TO_SPELL_TARGET = 2048
    }
}
