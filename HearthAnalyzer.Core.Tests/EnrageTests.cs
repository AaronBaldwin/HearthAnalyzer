using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class EnrageTests
    {
        private BasePlayer player;
        private BasePlayer opponent;

        [TestInitialize]
        public void Setup()
        {
            player = HearthEntityFactory.CreatePlayer<Warlock>();
            opponent = HearthEntityFactory.CreatePlayer<Warlock>();

            GameEngine.Initialize(player, opponent);

            GameEngine.GameState.CurrentPlayer = player;
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        /// <summary>
        /// +3 attack
        /// </summary>
        [TestMethod]
        public void AmaniBerserker()
        {
            var amani = HearthEntityFactory.CreateCard<AmaniBerserker>();

            amani.TakeDamage(1);

            Assert.AreEqual(amani.OriginalAttackPower + 3, amani.CurrentAttackPower, "Verify amani is enraged");

            amani.TakeHealing(1);

            Assert.AreEqual(amani.OriginalAttackPower, amani.CurrentAttackPower, "Verify amani chilled out");
        }

        /// <summary>
        /// +5 attack
        /// </summary>
        [TestMethod]
        public void AngryChicken()
        {
            var chicken = HearthEntityFactory.CreateCard<AngryChicken>();

            // Give the chicken some more health so we can enrage it
            chicken.TakeBuff(0, 10);

            chicken.TakeDamage(1);
            Assert.AreEqual(chicken.OriginalAttackPower + 5, chicken.CurrentAttackPower, "Verify attack");

            chicken.TakeHealing(1);
            Assert.AreEqual(chicken.OriginalAttackPower, chicken.CurrentAttackPower, "Verify unenraged chicken");
        }
    }
}
