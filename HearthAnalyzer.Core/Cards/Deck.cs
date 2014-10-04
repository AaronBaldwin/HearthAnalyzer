using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a deck of cards
    /// </summary>
    public class Deck
    {
        internal int topDeckIndex;
        internal int fatigueDamage;

        public List<BaseCard> Cards;

        /// <summary>
        /// Initialize a new empty deck
        /// </summary>
        public Deck()
        {
            this.Cards = new List<BaseCard>();
            this.topDeckIndex = -1;
            this.fatigueDamage = 0;
        }
        
        /// <summary>
        /// Initialize a deck with a list of cards
        /// </summary>
        /// <param name="cards">The cards to fill the deck with</param>
        public Deck(List<BaseCard> cards)
        {
            this.Cards = cards;
            this.topDeckIndex = this.Cards.Count - 1;
            this.fatigueDamage = 0;
        }

        public BaseCard DrawCard()
        {
            if (topDeckIndex < 0)
            {
                fatigueDamage++;
                return new FatigueCard(fatigueDamage);
            }

            BaseCard card = this.Cards[topDeckIndex];
            this.Cards.RemoveAt(topDeckIndex);
            this.topDeckIndex--;
            return card;
        }

        /// <summary>
        /// Adds a card to the current deck
        /// </summary>
        /// <param name="card">The card to add</param>
        /// <remarks>Adds to the end of the deck by default</remarks>
        public void AddCard(BaseCard card)
        {
            this.Cards.Add(card);
            this.topDeckIndex++;
        }

        /// <summary>
        /// Adds a list of cards to the current deck
        /// </summary>
        /// <param name="cards">The list of cards to add</param>
        /// <remarks>Adds to the end of the deck by default</remarks>
        public void AddCards(IEnumerable<BaseCard> cards)
        {
            this.Cards.AddRange(cards);
            this.topDeckIndex = this.Cards.Count - 1;
        }

        /// <summary>
        /// Draws n cards
        /// </summary>
        /// <param name="n">The number of cards to draw</param>
        /// <returns>The list of cards drawn.</returns>
        public List<BaseCard> DrawCards(int n = 1)
        {
            List<BaseCard> cards = new List<BaseCard>();
            for (int i = 0; i < n; i++)
            {
                cards.Add(DrawCard());
            }

            return cards;
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        /// <param name="n">The number of times to shuffle the deck</param>
        public void Shuffle(int n = 5)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < this.Cards.Count; j++)
                {
                    var swapIndex = GameEngine.Random.Next(this.Cards.Count);
                    BaseCard temp = this.Cards[swapIndex];
                    this.Cards[swapIndex] = this.Cards[j];
                    this.Cards[j] = temp;
                }
            }
        }

        /// <summary>
        /// Creates a deck from a deck file
        /// </summary>
        /// <param name="pathToFile">Path to the deck file</param>
        /// <returns>The deck based on the deck file</returns>
        /// <remarks>
        /// A deck file follows the following format:
        /// [count], [card name]
        /// with each card on a separate line
        /// </remarks>
        public static Deck FromDeckFile(string pathToFile)
        {
            var deck = new Deck();
            using (var stream = new StreamReader(pathToFile))
            {
                while (stream.Peek() >= 0)
                {
                    var line = stream.ReadLine();

                    if (line == null) continue;

                    // lines follow the format of [count], [card name]
                    var splitLine = line.Split(',');

                    if (splitLine.Count() != 2)
                    {
                        throw new InvalidDataException(string.Format("Expected format of line is \"[count], [card name]\". Got {0} instead", line));    
                    }

                    var count = int.Parse(splitLine[0]);
                    var cardName = splitLine[1];
                    var cleanedCardName = Regex.Replace(cardName, @"[\W]", "", RegexOptions.None);

                    for (int i = 0; i < count; i++)
                    {
                        var assembly = Assembly.Load("HearthAnalyzer.Core");
                        var cardType = assembly.GetTypes().FirstOrDefault(t => String.Equals(t.Name, cleanedCardName, StringComparison.InvariantCultureIgnoreCase));
                        if (cardType == null)
                        {
                            throw new InvalidDataException(string.Format("Failed to find card type with name: {0}", cleanedCardName));
                        }

                        var createCardMethod = typeof (HearthEntityFactory).GetMethod("CreateCard");
                        var createCardTypeMethod = createCardMethod.MakeGenericMethod(new[] {cardType});
                        dynamic card = createCardTypeMethod.Invoke(null, null);

                        deck.AddCard(card);
                    }
                }
            }

            if (deck.Cards.Count() > Constants.MAX_CARDS_IN_DECK)
            {
                throw new InvalidDataException("There are too many cards in this deck!");
            }

            if (deck.Cards.Count() < Constants.MAX_CARDS_IN_DECK)
            {
                throw new InvalidDataException("There are too few cards in this deck!");
            }

            return deck;
        }
    }
}
