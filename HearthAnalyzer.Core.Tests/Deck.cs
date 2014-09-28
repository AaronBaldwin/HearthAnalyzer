using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class Deck
    {
        private BasePlayer player;

        [TestInitialize]
        public void Setup()
        {
            player = new Warlock();

            GameEngine.Initialize(player, null);
            GameEngine.GameState.CurrentPlayer = player;
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        /// <summary>
        /// Verify adding cards
        /// </summary>
        [TestMethod]
        public void AddCards()
        {
            player.Deck.AddCard(new ChillwindYeti());

            Assert.AreEqual(1, player.Deck.Cards.Count, "Verify there is now one card");
            Assert.AreEqual(0, player.Deck.topDeckIndex, "Verify topDeckIndex");

            player.Deck.AddCards(Enumerable.Repeat<BaseCard>(new ChillwindYeti(), 29).ToList());
            Assert.AreEqual(30, player.Deck.Cards.Count, "Verify the deck now has 30 cards");
            Assert.AreEqual(29, player.Deck.topDeckIndex, "Verify topDeckIndex");
        }

        /// <summary>
        /// Verify drawing a single card
        /// </summary>
        [TestMethod]
        public void DrawSingleCard()
        {
            player.Deck.AddCards(Enumerable.Repeat<BaseCard>(new ChillwindYeti(), 30).ToList());

            var card = player.Deck.DrawCard();
            var expectedCard = player.Deck.Cards.Last();

            Assert.AreEqual(expectedCard, card, "Verify the correct card was drawn");
            Assert.AreEqual(29, player.Deck.Cards.Count, "Verify card count decreased");
        }

        /// <summary>
        /// Verify shuffling a card
        /// </summary>
        [TestMethod]
        public void Shuffle()
        {
            for (int i = 0; i < 30; i++)
            {
                player.Deck.AddCard(new ChillwindYeti(i));
            }

            var originalDeck = new BaseCard[30];
            player.Deck.Cards.CopyTo(originalDeck);

            player.Deck.Shuffle();

            var shuffledDeck = new BaseCard[30];
            player.Deck.Cards.CopyTo(shuffledDeck);

            Assert.IsFalse(originalDeck.SequenceEqual(shuffledDeck));
        }
    }
}
