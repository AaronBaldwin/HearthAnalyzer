using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a minion at the most basic level
    /// </summary>
    public abstract class BaseMinion : BaseCard
    {
        /// <summary>
        /// The attack power of this card
        /// </summary>
        public int OriginalAttack;

        /// <summary>
        /// The current attack power of this card
        /// </summary>
        public int CurrentAttack;

        /// <summary>
        /// The health of this card
        /// </summary>
        public int OriginalHealth;

        /// <summary>
        /// The current health of this card
        /// </summary>
        public int CurrentHealth;

        /// <summary>
        /// Represents the current status effects applied to the card
        /// </summary>
        public MinionStatusEffects StatusEffects;
    }

    /// <summary>
    /// Represents the types of status effects that can be applied to a card
    /// </summary>
    [Flags]
    public enum MinionStatusEffects
    {
        DIVINE_SHIELD = 0,
        CANT_ATTACK = 1,
        TAUNT = 2,
        STEALTHED = 4,
        EXHAUSTED = 8,
        WINDFURY = 16,
        FROZEN = 32,
        SILENCED = 64
    }
}
