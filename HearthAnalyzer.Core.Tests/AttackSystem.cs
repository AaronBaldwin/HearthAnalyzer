using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HearthAnalyzer.Core;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class AttackSystem
    {
        [TestMethod]
        public void TestMethod1()
        {
            var yeti1 = new ChillwindYeti(1);
            var yeti2 = new ChillwindYeti(2);
            var raptor1 = new BloodfenRaptor(3);
            var raptor2 = new BloodfenRaptor(4);

            GameEngine.Initialize(null, null, null);
            var gameState = GameEngine.GameState;

            // 4/5 attacking into another 4/5 should yeild two 4/1s
            yeti1.Attack(yeti2, gameState);

            Assert.AreEqual(1, yeti1.CurrentHealth, "Verify Yeti_1 is at 1 health.");
            Assert.AreEqual(1, yeti2.CurrentHealth, "Verify Yeti_2 is at 1 health.");

            // Now, kill yeti 1 with raptor 1. This should kill both the yeti and the raptor
            raptor1.Attack(yeti1, GameEngine.GameState);

            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(yeti1), "Verify Yeti_1 is dead");
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(raptor1), "Verify Raptor_1 is dead");
            Assert.AreEqual(-2, yeti1.CurrentHealth, "Verify Yeti_1 is at -2 health");
            Assert.AreEqual(-2, raptor1.CurrentHealth, "Verify Raptor_2 is at -2 health");
        }
    }
}
