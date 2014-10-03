using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class BattlecryTests : BaseTestSuite
    {
        private BaseMinion raptor1;
        private BaseMinion commando1;
        private BasePlayer player;

        [TestInitialize]
        public void Setup()
        {
            player = new Warlock();

            raptor1 = new BloodfenRaptor(1);
            commando1 = new StormpikeCommando(2);

            player.Hand = new List<BaseCard>()
            {
                commando1
            };

            player.Mana = 5;

            GameEngine.Initialize(player, null);

            var playerPlayZone = new List<BaseCard>(Constants.MAX_CARDS_ON_BOARD);
            playerPlayZone.AddRange(Enumerable.Repeat<BaseCard>(null, playerPlayZone.Capacity));

            var opponentPlayZone = new List<BaseCard>(Constants.MAX_CARDS_ON_BOARD);
            opponentPlayZone.AddRange(Enumerable.Repeat<BaseCard>(null, opponentPlayZone.Capacity));
            opponentPlayZone.Insert(0, raptor1);

            var gameBoard = new GameBoard()
            {
                PlayerPlayZone = playerPlayZone,
                OpponentPlayZone = opponentPlayZone
            };

            GameEngine.GameState.Board = gameBoard;
            GameEngine.GameState.CurrentPlayer = player;
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        [TestMethod]
        public void StormpikeCommando()
        {
            player.PlayCard(commando1, raptor1, 0);
            Assert.AreEqual(commando1, GameEngine.GameState.Board.PlayerPlayZone[0], "Verify that the commando was placed on the board");
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(raptor1), "Verify the raptor died due to battlecry");
        }
    }
}
