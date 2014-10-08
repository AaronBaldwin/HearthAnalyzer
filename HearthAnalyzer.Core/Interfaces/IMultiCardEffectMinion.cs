using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core.Interfaces
{
/// <summary>
    /// Represents a card with multiple effects (such as Ancient of Lore, or Power of the Wild)
    /// </summary>
    public interface IMultiCardEffectMinion
    {
        /// <summary>
        /// Uses a card effect
        /// </summary>
        /// <param name="cardEffect">The card effect to use</param>
        /// <param name="target">The target of the effect if applicable</param>
        void UseCardEffect(CardEffect cardEffect, IDamageableEntity target = null);
    }
}
