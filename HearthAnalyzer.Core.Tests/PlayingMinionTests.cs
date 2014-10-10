using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class PlayingMinionTests : BaseTestSuite
    {
        private BasePlayer player;

        [TestInitialize]
        public void Setup()
        {
            player = new Warlock();
            for (int i = 0; i < 10; i++)
            {
                player.AddCardToHand(new ChillwindYeti(i));
            }

            GameEngine.Initialize(player, null);
            GameEngine.GameState.CurrentPlayer = player;
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        /// <summary>
        /// Play 7 minions to fill up the board.
        /// Play an 8th minion and verify that it is illegal
        /// </summary>
        [TestMethod]
        public void PlaceMinionsUntilFull()
        {
            // For this test's purpose, we don't care about mana
            player.hand.ForEach(yeti => yeti.CurrentManaCost = 0);

            for (int i = 0; i < 7; i++)
            {
                player.PlayCard(player.Hand.FirstOrDefault(), null, 0);

                for (int j = 0; j < i+1; j++)
                {
                    Assert.AreEqual(i-j, GameEngine.GameState.Board.PlayerPlayZone[j].Id, "Verify that the chillwind yetis are played in reverse order");
                }
            }

            // Try to play an 8th card on the board
            try
            {
                player.PlayCard(player.Hand.FirstOrDefault(), null, 0);
                Assert.Fail(
                    "Expected to get an InvalidOperationException for playing on a full board. Board Size: {0}",
                    GameEngine.GameState.Board.PlayerPlayZone.Count(card => card != null));
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// Play a minion with insufficient mana and verify it is illegal
        /// </summary>
        [TestMethod]
        public void PlayMinionWithInsufficientMana()
        {
            player.Mana = 0;

            var minionToPlay = player.Hand.FirstOrDefault();

            try
            {
                player.PlayCard(minionToPlay, null, 0);
                Assert.Fail(
                    "Expected to get an InvalidOperationException for playing a minion with insufficient mana. Mana: {0} Cost: {1}",
                    player.Mana, minionToPlay.CurrentManaCost);
            }
            catch (InvalidOperationException)
            {
            }
        }
        
        /// <summary>
        /// Verify minions shifting when placed in between
        /// </summary>
        [TestMethod]
        public void MinionShifting()
        {
            // Don't care about mana cost for this test
            player.hand.ForEach(card => card.CurrentManaCost = 0);

            GameEngine.GameState.Board.PlayerPlayZone = new List<BaseCard>(Constants.MAX_CARDS_ON_BOARD)
            {
                new BloodfenRaptor(10),
                new BloodfenRaptor(11),
                new BloodfenRaptor(12),
                new BloodfenRaptor(13),
                null,
                null,
                null
            };

            // Place a yeti in between raptor 11 and raptor 12
            player.PlayCard(player.Hand.First(), null, 2);

            var expectedIdList = new List<int>()
            {
                10,
                11,
                0,
                12,
                13
            };

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(expectedIdList[i], GameEngine.GameState.Board.PlayerPlayZone[i].Id, "Verify position {0}", i);
            }

            // Now place a yeti before raptor 10
            player.PlayCard(player.Hand.First(), null, 0);

            expectedIdList = new List<int>()
            {
                1,
                10,
                11,
                0,
                12,
                13
            };

            for (int i = 0; i < 6; i++)
            {
                Assert.AreEqual(expectedIdList[i], GameEngine.GameState.Board.PlayerPlayZone[i].Id, "Verify position {0}", i);
            }
        }

        /// <summary>
        /// Verify playing a minion decreases the player's mana
        /// </summary>
        [TestMethod]
        public void ManaDecreasing()
        {
            const int startingMana = 10;
            player.Mana = startingMana;

            var minionToPlay = player.Hand.First();
            // Playing a yeti should decrease the player's mana
            player.PlayCard(minionToPlay, null, 0);

            Assert.AreEqual(startingMana - minionToPlay.CurrentManaCost, player.Mana, "Verify mana was decremented properly");
        }
    }
}
