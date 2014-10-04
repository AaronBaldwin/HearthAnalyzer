using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Cards.Spells;
using HearthAnalyzer.Core.Heroes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class DeckTests : BaseTestSuite
    {
        private BasePlayer player;
        private readonly string DeckTestDataPath = @".\TestData\Decks";

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
        /// Verify the player takes fatigue damage when they run out of cards
        /// </summary>
        [TestMethod]
        public void FatigueDamage()
        {
            player.Deck.Cards.Clear();

            player.DrawCard();

            Assert.AreEqual(1, player.Deck.fatigueDamage, "Verify fatigue damage is set to 1");
            Assert.AreEqual(29, player.Health, "Verify the player took one point of damage");

            player.DrawCard();
            Assert.AreEqual(2, player.Deck.fatigueDamage, "Verify fatigue damage increased");
            Assert.AreEqual(27, player.Health, "Verify the player took more damage");
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

        /// <summary>
        /// Verify constructing a deck from a deck file
        /// </summary>
        [TestMethod]
        public void FromValidDeckFile()
        {
            var zooLockDeckFile = Path.Combine(DeckTestDataPath, "ZooLock.txt");
            var actualDeck = Deck.FromDeckFile(zooLockDeckFile);

            HearthEntityFactory.Reset();

            var expectedDeck = new Deck(new List<BaseCard>()
            {
                HearthEntityFactory.CreateCard<Soulfire>(),
                HearthEntityFactory.CreateCard<Soulfire>(),
                HearthEntityFactory.CreateCard<AbusiveSergeant>(),
                HearthEntityFactory.CreateCard<AbusiveSergeant>(),
                HearthEntityFactory.CreateCard<ArgentSquire>(),
                HearthEntityFactory.CreateCard<ArgentSquire>(),
                HearthEntityFactory.CreateCard<ElvenArcher>(),
                HearthEntityFactory.CreateCard<FlameImp>(),
                HearthEntityFactory.CreateCard<FlameImp>(),
                HearthEntityFactory.CreateCard<Voidwalker>(),
                HearthEntityFactory.CreateCard<Voidwalker>(),
                HearthEntityFactory.CreateCard<DireWolfAlpha>(),
                HearthEntityFactory.CreateCard<DireWolfAlpha>(),
                HearthEntityFactory.CreateCard<KnifeJuggler>(),
                HearthEntityFactory.CreateCard<KnifeJuggler>(),
                HearthEntityFactory.CreateCard<LorewalkerCho>(),
                HearthEntityFactory.CreateCard<BloodKnight>(),
                HearthEntityFactory.CreateCard<HarvestGolem>(),
                HearthEntityFactory.CreateCard<HarvestGolem>(),
                HearthEntityFactory.CreateCard<ScarletCrusader>(),
                HearthEntityFactory.CreateCard<ScarletCrusader>(),
                HearthEntityFactory.CreateCard<ShatteredSunCleric>(),
                HearthEntityFactory.CreateCard<ShatteredSunCleric>(),
                HearthEntityFactory.CreateCard<DarkIronDwarf>(),
                HearthEntityFactory.CreateCard<DarkIronDwarf>(),
                HearthEntityFactory.CreateCard<DefenderofArgus>(),
                HearthEntityFactory.CreateCard<DefenderofArgus>(),
                HearthEntityFactory.CreateCard<Doomguard>(),
                HearthEntityFactory.CreateCard<Doomguard>(),
                HearthEntityFactory.CreateCard<ArgentCommander>()
            });

            Assert.IsTrue(actualDeck.Cards.SequenceEqual(expectedDeck.Cards), "Verify the generated deck is the same.");
        }

        /// <summary>
        /// Verify creating a deck from a deck file with too few cards
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void FromInvalidDeckFileTooFewCards()
        {
            var deckFile = Path.Combine(DeckTestDataPath, "NotEnoughCards.txt");
            Deck.FromDeckFile(deckFile);
        }

        /// <summary>
        /// Verify creating a deck from a deck file with too many cards
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void FromInvalidDeckFileTooManyCards()
        {
            var deckFile = Path.Combine(DeckTestDataPath, "TooManyCards.txt");
            Deck.FromDeckFile(deckFile);
        }

        /// <summary>
        /// Verify creating a deck from a deck file with bogus cards
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void FromInvalidDeckFileBogusCards()
        {
            var deckFile = Path.Combine(DeckTestDataPath, "BogusCards.txt");
            Deck.FromDeckFile(deckFile);
        }
    }
}
