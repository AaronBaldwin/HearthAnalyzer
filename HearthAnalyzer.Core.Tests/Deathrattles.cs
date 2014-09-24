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
    public class Deathrattles
    {
        private BaseMinion yeti1;
        private BaseMinion yeti2;
        private BaseMinion yeti3;
        private BaseMinion yeti4;
        private BaseMinion abom1;

        [TestInitialize]
        public void Setup()
        {
            GameEngine.Initialize(null, null);

            yeti1 = new ChillwindYeti(1);
            yeti2 = new ChillwindYeti(2);
            yeti3 = new ChillwindYeti(3);
            yeti4 = new ChillwindYeti(4);
            abom1 = new Abomination(5);

            var gameBoard = new GameBoard()
            {
                PlayerPlayZone = new List<BaseCard>()
                {
                    yeti1,
                    yeti2,
                    abom1
                },

                OpponentPlayZone = new List<BaseCard>()
                {
                    yeti3,
                    yeti4
                }
            };

            GameEngine.GameState.Board = gameBoard;
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        [TestMethod]
        public void DeathrattleDamageAllMinions()
        {
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
