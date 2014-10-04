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
    public class DeathrattleTests : BaseTestSuite
    {
        private BaseMinion yeti1;
        private BaseMinion yeti2;
        private BaseMinion yeti3;
        private BaseMinion yeti4;
        private BaseMinion abom1;
        private BasePlayer player;
        private BasePlayer opponent;

        [TestInitialize]
        public void Setup()
        {
            player = HearthEntityFactory.CreatePlayer<Warlock>();
            opponent = HearthEntityFactory.CreatePlayer<Warlock>();

            yeti1 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            yeti2 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            yeti3 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            yeti4 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            abom1 = HearthEntityFactory.CreateCard<Abomination>();

            var gameBoard = new GameBoard()
            {
                PlayerPlayZone = new List<BaseCard>()
                {
                    yeti1,
                    yeti2,
                    null,
                    null,
                    null,
                    null,
                    null
                },

                OpponentPlayZone = new List<BaseCard>()
                {
                    yeti3,
                    yeti4,
                    null,
                    null,
                    null,
                    null,
                    null
                }
            };

            GameEngine.Initialize(player, opponent, gameBoard, 0, player);
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        [TestMethod]
        public void DeathrattleDamageAllMinions()
        {
            abom1.CurrentManaCost = 0;
            player.Hand.Add(abom1);
            player.PlayCard(abom1, null);

            GameEngine.EndTurn();

            // This should kill the abomination
            // The yeti should also die from the deathrattle
            yeti3.Attack(abom1);

            // The rest of the yetis should have taken 2 damage from the abom deathrattle
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(abom1));
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(yeti3));
            Assert.AreEqual(3, yeti1.CurrentHealth, "Verify that the other yetis are hurt from the deathrattle");
            Assert.AreEqual(3, yeti2.CurrentHealth, "Verify that the other yetis are hurt from the deathrattle");
            Assert.AreEqual(3, yeti4.CurrentHealth, "Verify that the other yetis are hurt from the deathrattle");
        }
    }
}
