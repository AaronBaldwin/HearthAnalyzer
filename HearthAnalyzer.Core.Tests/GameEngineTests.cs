using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Spells;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class GameEngineTests : BaseTestSuite
    {
        private BasePlayer player;
        private BasePlayer opponent;

        private readonly string DeckTestDataPath = @".\TestData\Decks";

        [TestInitialize]
        public void Setup()
        {
            player = HearthEntityFactory.CreatePlayer<Warlock>();
            opponent = HearthEntityFactory.CreatePlayer<Warlock>();

            string zooLockDeckFile = Path.Combine(DeckTestDataPath, "ZooLock.txt");

            player.Deck = Deck.FromDeckFile(zooLockDeckFile);
            opponent.Deck = Deck.FromDeckFile(zooLockDeckFile);

            GameEngine.Initialize(player, opponent);
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        /// <summary>
        /// Verify mulligan logic
        /// </summary>
        [TestMethod]
        public void Mulligan()
        {
            GameEngine.DealPreMulligan();

            // Mulligan the player's hand completely
            var handCount = player.Hand.Count;
            BaseCard[] originalHand = new BaseCard[handCount];
            player.Hand.CopyTo(originalHand);
            GameEngine.Mulligan(player, originalHand);

            // Verify that the player's hand don't contain the same cards
            Assert.IsFalse(player.Hand.SequenceEqual(originalHand.ToList()), "Verify the hand is new.");
            Assert.AreEqual(Constants.MAX_CARDS_IN_DECK - handCount, player.Deck.Cards.Count, "Verify deck size after mulligan");
            Assert.IsFalse(originalHand.Except(player.Deck.Cards).Any(), "Verify the original cards are back in the deck");
            Assert.IsTrue(GameEngine.PlayerMulliganed, "Verify that the player mulliganed flag is set");

            // For the opponent, choose not to mulligan any cards
            var opponentHandCount = opponent.Hand.Count;
            BaseCard[] opponentOriginalHand = new BaseCard[opponentHandCount];
            opponent.Hand.CopyTo(opponentOriginalHand);
            GameEngine.Mulligan(opponent, null);

            // Verify that the opponent's hand contains the same cards
            Assert.IsFalse(opponentOriginalHand.Except(opponent.Hand).Any(), "Verify the hand has the same cards");
            Assert.IsTrue(GameEngine.OpponentMulliganed, "Verify that the opponent mulliganed flag is set");
        }

        /// <summary>
        /// Verify the post mulligan phase
        /// </summary>
        [TestMethod]
        public void PostMulligan()
        {
            var currentPlayer = GameEngine.GameState.CurrentPlayer;
            var waitingPlayer = GameEngine.GameState.WaitingPlayer;

            GameEngine.DealPreMulligan();

            GameEngine.Mulligan(player, null);
            GameEngine.Mulligan(opponent, null);

            // Verify that it's now turn 1
            Assert.AreEqual(1, GameEngine.GameState.TurnNumber, "Verify turn number");
            Assert.AreEqual(1, currentPlayer.MaxMana, "Verify current player max mana");
            Assert.AreEqual(1, currentPlayer.Mana, "Verify current player mana");

            Assert.IsTrue(waitingPlayer.Hand.Any(card => card is TheCoin), "Verify that the waiting player got the coin");
            Assert.AreEqual(5, waitingPlayer.Hand.Count, "Verify the waiting player hand size");

            Assert.AreEqual(4, currentPlayer.Hand.Count, "Verify current player hand size");
        }

        /// <summary>
        /// Verify ending a turn
        /// </summary>
        [TestMethod]
        public void EndTurn()
        {
            GameEngine.DealPreMulligan();

            GameEngine.Mulligan(player, null);
            GameEngine.Mulligan(opponent, null);

            var waitingPlayer = GameEngine.GameState.WaitingPlayer;
            GameEngine.EndTurn();

            Assert.AreEqual(waitingPlayer, GameEngine.GameState.CurrentPlayer, "Verify the current player has switched to the previously waiting player");
        }

        /// <summary>
        /// Verify mana cannot go beyond capacity
        /// </summary>
        [TestMethod]
        public void AddManaCapped()
        {
            GameEngine.GameState.WaitingPlayer.MaxMana = 10;
            GameEngine.EndTurn();

            var currentPlayer = GameEngine.GameState.CurrentPlayer;
            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, currentPlayer.MaxMana, "Verify max mana doesn't exceed maximum value");
            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, currentPlayer.Mana, "Verify mana is replenished");
        }
    }
}
