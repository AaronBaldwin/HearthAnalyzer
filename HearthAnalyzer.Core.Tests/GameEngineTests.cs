using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Cards.Spells;
using HearthAnalyzer.Core.Cards.Weapons;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class GameEngineTests : BaseTestSuite
    {
        private BasePlayer player;
        private BasePlayer opponent;
        private bool gameEnded;
        private GameEngine.GameResult? gameResult;

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
            gameEnded = false;
            gameResult = null;
            GameEngine.GameEnded += OnGameEnded;
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

        /// <summary>
        /// Verify death due to fatigue
        /// </summary>
        [TestMethod]
        public void GameEndDueToFatigueDamage()
        {
            player.Health = 1;
            player.Deck = new Deck();

            player.DrawCard();

            // wait for game end
            Task.Factory.StartNew(() => this.WaitUntilGameEnded(250, 8)).Wait();

            // Game should have ended
            Assert.IsTrue(this.gameEnded, "Verify the game has ended");
            Assert.AreEqual(this.gameResult, GameEngine.GameResult.LOSE, "Verify we lost because we fatigued ourself to death");
        }

        /// <summary>
        /// Verify the game ended due to attack
        /// </summary>
        [TestMethod]
        public void GameEndDueToAttack()
        {
            opponent.Health = 1;
            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            GameEngine.GameState.Board.PlayerPlayZone.Add(yeti);
            yeti.RemoveStatusEffects(MinionStatusEffects.EXHAUSTED);

            yeti.Attack(opponent);

            Task.Factory.StartNew(() => this.WaitUntilGameEnded(250, 8)).Wait();

            Assert.IsTrue(this.gameEnded, "Verify the game has ended");
            Assert.AreEqual(this.gameResult, GameEngine.GameResult.WIN, "Verify we won because we killed the opponent");
        }

        /// <summary>
        /// Verify the game was a draw
        /// </summary>
        [TestMethod]
        public void GameDraw()
        {
            player.Health = 1;
            opponent.Health = 1;

            GameEngine.GameState.CurrentPlayer = player;
            var hellfire = HearthEntityFactory.CreateCard<Hellfire>();
            hellfire.Owner = player;
            hellfire.CurrentManaCost = 0;
            player.Hand.Add(hellfire);
            player.PlayCard(hellfire, null);

            Task.Factory.StartNew(() => this.WaitUntilGameEnded(250, 8)).Wait();

            Assert.IsTrue(this.gameEnded, "Verify the game has ended");
            Assert.AreEqual(this.gameResult, GameEngine.GameResult.DRAW, "Verify it was a draw");
        }

        /// <summary>
        /// Verify the game ends if the opponent is killed as a result of playing a card
        /// </summary>
        [TestMethod]
        public void GameEndDueToCardPlayed()
        {
            opponent.Health = 1;
            var stormpikeCommando = HearthEntityFactory.CreateCard<StormpikeCommando>();
            stormpikeCommando.CurrentManaCost = 0;
            player.Hand.Add(stormpikeCommando);

            GameEngine.GameState.CurrentPlayer = player;
            player.PlayCard(stormpikeCommando, opponent);

            Task.Factory.StartNew(() => this.WaitUntilGameEnded(250, 8)).Wait();

            Assert.IsTrue(this.gameEnded, "Verify the game has ended");
            Assert.AreEqual(this.gameResult, GameEngine.GameResult.WIN, "Verify we won because we killed the opponent");
        }

        /// <summary>
        /// Verify overload logic
        /// </summary>
        [TestMethod]
        public void Overload()
        {
            player.MaxMana = Constants.MAX_MANA_CAPACITY;
            var stormAxe = HearthEntityFactory.CreateCard<StormforgedAxe>();
            stormAxe.CurrentManaCost = 0;
            player.Hand.Add(stormAxe);

            GameEngine.GameState.CurrentPlayer = player;
            player.PlayCard(stormAxe, null);

            Assert.AreEqual(stormAxe, player.Weapon, "Verify axe was equipped");
            Assert.AreEqual(1, player.PendingOverload, "Verify player's pending overload");

            GameEngine.EndTurn();
            GameEngine.EndTurn();

            Assert.AreEqual(0, player.PendingOverload, "Verify pending overload is now gone");
            Assert.AreEqual(1, player.Overload, "Verify overload");
            Assert.AreEqual(Constants.MAX_MANA_CAPACITY - 1, player.Mana, "Verify mana less overload");

            GameEngine.EndTurn();
            GameEngine.EndTurn();

            Assert.AreEqual(0, player.Overload, "Verify overload is now gone");
            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, player.Mana, "Verify mana is back to normal");
        }

        /// <summary>
        /// Verify you can't attack through taunt
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Taunt()
        {
            var alakir = HearthEntityFactory.CreateCard<AlAkirtheWindlord>();
            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();

            GameEngine.GameState.CurrentPlayer = player;

            GameEngine.GameState.CurrentPlayerPlayZone[0] = faerie;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = alakir;
            GameEngine.GameState.WaitingPlayerPlayZone[1] = yeti;

            faerie.Attack(yeti);
            faerie.Attack(opponent);
        }

        /// <summary>
        /// Verify charge and windfury mechanics
        /// </summary>
        [TestMethod]
        public void ChargeAndWindfury()
        {
            var alakir = HearthEntityFactory.CreateCard<AlAkirtheWindlord>();
            alakir.CurrentManaCost = 0;

            GameEngine.GameState.CurrentPlayer = player;
            player.Hand.Add(alakir);

            player.PlayCard(alakir, null);
            alakir.Attack(opponent);

            Assert.AreEqual(30 - alakir.CurrentAttackPower, opponent.Health, "Verify the opponent took damage");

            alakir.Attack(opponent);
            Assert.AreEqual(30 - (alakir.CurrentAttackPower * 2), opponent.Health, "Verify the opponent got hit again");

            try
            {
                alakir.Attack(opponent);
                Assert.Fail("Alakir shouldn't be able to attack a third time!");
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// Verify divine shield mechanics
        /// </summary>
        [TestMethod]
        public void DivineShield()
        {
            var alakir = HearthEntityFactory.CreateCard<AlAkirtheWindlord>();
            var playerAlakir = HearthEntityFactory.CreateCard<AlAkirtheWindlord>();

            GameEngine.GameState.CurrentPlayer = player;
            GameEngine.GameState.CurrentPlayerPlayZone[0] = playerAlakir;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = alakir;

            playerAlakir.Attack(alakir);

            Assert.IsFalse(playerAlakir.HasDivineShield, "Verify player's alakir lost divine shield");
            Assert.IsFalse(alakir.HasDivineShield, "Verify opponent's alakir lost divine shield");

            playerAlakir.Attack(alakir);

            Assert.AreEqual(5 - alakir.CurrentAttackPower, playerAlakir.CurrentHealth, "Verify player's alakir took damage");
            Assert.AreEqual(5 - playerAlakir.CurrentAttackPower, alakir.CurrentHealth, "Verify opponent's alakir took damage");
        }

        /// <summary>
        /// Triggered when the game has ended
        /// </summary>
        /// <param name="result"></param>
        public void OnGameEnded(GameEngine.GameResult result)
        {
            this.gameEnded = true;
            this.gameResult = result;
        }

        /// <summary>
        /// Waits for the game to end within a timeout
        /// </summary>
        /// <param name="intervalInMs">The time to wait between retries in milliseconds</param>
        /// <param name="retries">The number of retries</param>
        private Task WaitUntilGameEnded(int intervalInMs, int retries)
        {
            for (int i = 0; i < retries; i++)
            {
                if (this.gameEnded)
                {
                    break;
                }

                Thread.Sleep(intervalInMs);
            }

            return null;
        }
    }
}
