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
        public CardStatusEffects StatusEffects;
    }
}
