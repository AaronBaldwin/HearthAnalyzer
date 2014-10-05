using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class BattlecryTests : BaseTestSuite
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
        /// Give minion +2 attack
        /// </summary>
        [TestMethod]
        public void AbusiveSergeant()
        {
            var sergeant = HearthEntityFactory.CreateCard<AbusiveSergeant>();
            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();
            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = faerie;

            // Verify buffing friendly minion
            sergeant.CurrentManaCost = 0;
            player.Hand.Add(sergeant);
            player.PlayCard(sergeant, yeti);

            Assert.AreEqual(yeti.OriginalAttackPower + 2, yeti.CurrentAttackPower, "Verify yeti got an attack buff");

            // Verify buffing enemy minion
            player.Hand.Add(sergeant);
            player.PlayCard(sergeant, faerie);

            Assert.AreEqual(faerie.OriginalAttackPower + 2, faerie.CurrentAttackPower, "Verify faerie got an attack buff");

            // Verify can't buff players
            player.Hand.Add(sergeant);
            try
            {
                player.PlayCard(sergeant, player);
                Assert.Fail("Shouldn't be able to buff player!");
            }
            catch (InvalidOperationException)
            {
            }
            
        }

        /// <summary>
        /// Destroy opponent's weapon
        /// </summary>
        [TestMethod]
        public void AcidicSwampOoze()
        {
            var ooze = HearthEntityFactory.CreateCard<AcidicSwampOoze>();
            ooze.CurrentManaCost = 0;

            var weapon = HearthEntityFactory.CreateCard<Gorehowl>();
            opponent.Weapon = weapon;
            opponent.Weapon.WeaponOwner = opponent;

            player.Hand.Add(ooze);
            player.PlayCard(ooze, null);

            Assert.IsNull(opponent.Weapon, "Verify opponent weapon got destroyed");

            // Just make sure it doesn't poop itself if there are no weapons to destroy
            player.Hand.Add(ooze);
            player.PlayCard(ooze, null);
        }

        /// <summary>
        /// Change an enemy minion's attack to 1
        /// </summary>
        [TestMethod]
        public void AldorPeacekeeper()
        {
            var aldor = HearthEntityFactory.CreateCard<AldorPeacekeeper>();
            aldor.CurrentManaCost = 0;

            var rag = HearthEntityFactory.CreateCard<RagnarostheFirelord>();

            GameEngine.GameState.WaitingPlayerPlayZone[0] = rag;

            player.Hand.Add(aldor);
            player.PlayCard(aldor, rag);

            Assert.AreEqual(1, rag.CurrentAttackPower, "Verify rag's attack is changed to 1");
        }

        /// <summary>
        /// Change a hero's health to 15
        /// </summary>
        [TestMethod]
        public void Alexstrasza()
        {
            var alex = HearthEntityFactory.CreateCard<Alexstrasza>();
            alex.CurrentManaCost = 0;

            player.Hand.Add(alex);
            player.PlayCard(alex, opponent);

            Assert.AreEqual(15, opponent.Health, "Verify opponent health is set to 15");

            player.Hand.Add(alex);
            player.PlayCard(alex, player);

            Assert.AreEqual(15, player.Health, "Verify player health is set to 15");
        }

        /// <summary>
        /// Freeze a character
        /// </summary>
        [TestMethod]
        public void FrostElemental()
        {
            var frostElemental = HearthEntityFactory.CreateCard<FrostElemental>();
            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();

            // Verify targeting a minion on the board
            GameEngine.GameState.WaitingPlayerPlayZone[0] = faerie;

            player.Hand.Add(frostElemental);
            frostElemental.CurrentManaCost = 0;

            player.PlayCard(frostElemental, faerie);

            Assert.IsTrue(faerie.IsFrozen, "Verify the faerie dragon is frozen");

            // Verify targeting a character
            frostElemental = HearthEntityFactory.CreateCard<FrostElemental>();
            player.Hand.Add(frostElemental);
            frostElemental.CurrentManaCost = 0;

            player.PlayCard(frostElemental, opponent);

            Assert.IsTrue(opponent.IsFrozen, "Verify the opponent is now frozen");
        }

        /// <summary>
        /// Deal 2 damage to a character
        /// </summary>
        [TestMethod]
        public void StormpikeCommando()
        {
            var commando = HearthEntityFactory.CreateCard<StormpikeCommando>();
            var raptor = HearthEntityFactory.CreateCard<BloodfenRaptor>();

            GameEngine.GameState.WaitingPlayerPlayZone[0] = raptor;

            player.Hand.Add(commando);
            commando.CurrentManaCost = 0;

            player.PlayCard(commando, raptor, 0);
            Assert.AreEqual(commando, GameEngine.GameState.Board.PlayerPlayZone[0], "Verify that the commando was placed on the board");
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(raptor), "Verify the raptor died due to battlecry");
        }
    }
}
