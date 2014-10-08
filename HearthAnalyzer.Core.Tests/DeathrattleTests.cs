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
        private BasePlayer player;
        private BasePlayer opponent;

        [TestInitialize]
        public void Setup()
        {
            player = HearthEntityFactory.CreatePlayer<Warlock>();
            opponent = HearthEntityFactory.CreatePlayer<Warlock>();

            GameEngine.Initialize(player, opponent, null, 0, player);
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        [TestMethod]
        public void DeathrattleDamageAllMinions()
        {
            var yeti1 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var yeti2 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var yeti3 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var yeti4 = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var abom1 = HearthEntityFactory.CreateCard<Abomination>();

            abom1.CurrentManaCost = 0;
            player.Hand.Add(abom1);
            
            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti1;
            GameEngine.GameState.CurrentPlayerPlayZone[1] = yeti2;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = yeti3;
            GameEngine.GameState.WaitingPlayerPlayZone[1] = yeti4;

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

        /// <summary>
        /// Verify a random friendly minion was returned to the hand
        /// </summary>
        [TestMethod]
        public void DeathRattleReturnFriendlyMinion()
        {
            var ambusher = HearthEntityFactory.CreateCard<AnubarAmbusher>();
            ambusher.Owner = player;
            ambusher.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();
            var giant = HearthEntityFactory.CreateCard<SeaGiant>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;
            GameEngine.GameState.CurrentPlayerPlayZone[1] = faerie;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = giant;

            player.Hand.Add(ambusher);

            player.PlayCard(ambusher, null);

            GameEngine.EndTurn();
            giant.Attack(ambusher);

            // Ambusher should die and return a random friendly minion back to the owner's hand
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(ambusher), "Verify ambusher died");
            Assert.IsTrue(player.Hand.Contains(yeti) || player.Hand.Contains(faerie), "Verify minion returned to hand");
        }
    }
}
