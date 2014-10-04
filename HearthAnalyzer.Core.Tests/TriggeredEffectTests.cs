using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class TriggeredEffectTests
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
        /// Draw a card whenever damaged
        /// </summary>
        [TestMethod]
        public void AcolyteOfPain()
        {
            var acolyte = HearthEntityFactory.CreateCard<AcolyteofPain>();
            acolyte.Owner = player;
            acolyte.CurrentManaCost = 0;

            player.Hand.Add(acolyte);
            player.PlayCard(acolyte, null);

            acolyte.TakeDamage(1);
            Assert.AreEqual(29, player.Health, "Verify the player drew a fatigue card");

            acolyte.TakeDamage(2);
            Assert.AreEqual(27, player.Health, "Verify the player drew another fatigue card");

            // Acolyte should be dead now so make sure there are no more registered triggered effects
            Assert.IsFalse(GameEventManager._damageDealtListeners.Any(x => x.Item1 == acolyte));
        }
    }
}
