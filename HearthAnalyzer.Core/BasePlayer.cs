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
        /// The game board the player is playing on
        /// </summary>
        public GameBoard Board;

        /// <summary>
        /// The players hand
        /// </summary>
        public List<BaseCard> Hand;

        /// <summary>
        /// The player's deck
        /// </summary>
        public List<BaseCard> Deck;

        /// <summary>
        /// The player's graveyard
        /// </summary>
        public List<BaseCard> Graveyard;

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

        /// <summary>
        /// Plays a card
        /// </summary>
        /// <param name="card">The card to play</param>
        /// <param name="gameboardPos">The position on the gameboard to place the card (if applicable)</param>
        /// <remarks>Only really needed for simulation. We can read the logs to determine where a card got placed.</remarks>
        public void PlayCard(BaseCard card, int gameboardPos = 0)
        {
            // Check if it exists in the player's hand
            var cardInHand = this.Hand.FirstOrDefault(c => c.Equals(card));
            if (cardInHand == null)
            {
                throw new InvalidOperationException(string.Format("You can't play a card that's not in hand! {0}", card));
            }

            // if so, remove it
            this.Hand.Remove(cardInHand);

            // then put it on the game board

            // call the card's battlecry 

            // Buff / Damage / kill check

            // place it on the board
        }

        /// <summary>
        /// Returns a list of playable cards given the player's current mana
        /// </summary>
        /// <returns>The list of playable cards given the player's current mana</returns>
        public List<BaseCard> GetPlayableCards()
        {
            return this.Hand.Where(c => c.ManaCost <= this.Mana).ToList();
        }
    }
}
