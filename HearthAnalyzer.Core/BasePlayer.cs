using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents a player in Hearthstone
    /// </summary>
    public abstract class BasePlayer
    {
        /// <summary>
        /// The players hand
        /// </summary>
        public List<BaseCard> Hand;

        /// <summary>
        /// The player's deck
        /// </summary>
        public List<BaseCard> Deck;

        /// <summary>
        /// The player's remaining health
        /// </summary>
        public int Health;

        /// <summary>
        /// The player's armor value
        /// </summary>
        public int Armor;

        /// <summary>
        /// The player's remaining mana
        /// </summary>
        public int Mana;

        /// <summary>
        /// The player's maximum mana
        /// </summary>
        public int MaxMana;

        /// <summary>
        /// The player's weapon
        /// </summary>
        public BaseCard Weapon;

        /// <summary>
        /// The player's hero power
        /// </summary>
        public BaseCard HeroPower;
    }
}
