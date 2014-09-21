using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents the base card type
    /// </summary>
    public abstract class BaseCard
    {
        /// <summary>
        /// The id of this card
        /// </summary>
        public int Id;

        /// <summary>
        /// The name of this card
        /// </summary>
        public string Name;

        /// <summary>
        /// The type of this card
        /// </summary>
        public CardType Type;

        /// <summary>
        /// The mana cost for this card
        /// </summary>
        public int ManaCost;

        /// <summary>
        /// The current mana cost for this card
        /// </summary>
        public int CurrentManaCost;
    }

    /// <summary>
    /// Represents the types of status effects that can be applied to a card
    /// </summary>
    [Flags]
    public enum CardStatusEffects
    {
        DIVINE_SHIELD = 0,
        CANT_ATTACK = 1,
        TAUNT = 2,
        STEALTHED = 4,
        SUMMONING_SICKNESS = 8,
        WINDFURY = 16,
        FROZEN = 32,
        SILENCED = 64
    }

    /// <summary>
    /// Represents the various card types
    /// </summary>
    public enum CardType
    {
        BEAST,
        DEMON,
        DRAGON,
        MURLOC,
        TOTEM,
        NORMAL_MINION,
        SPELL,
        WEAPON
    }
}
