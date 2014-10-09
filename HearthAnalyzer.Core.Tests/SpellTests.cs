using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Cards.Spells;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class SpellTests : BaseTestSuite
    {
        private BasePlayer player;
        private BasePlayer opponent;

        [TestInitialize]
        public void Setup()
        {
            player = new Warlock();
            opponent = new Warlock();

            GameEngine.Initialize(player, opponent);
            GameEngine.GameState.CurrentPlayer = player;
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        /// <summary>
        /// Verify circle of healing mechanics
        /// </summary>
        [TestMethod]
        public void CircleOfHealing()
        {
            var circleOfHealing = HearthEntityFactory.CreateCard<CircleofHealing>();
            circleOfHealing.CurrentManaCost = 0;

            var playerYeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            playerYeti.BonusSpellPower = 1;
            playerYeti.MaxHealth = 10;
            playerYeti.CurrentHealth = 1;

            var opponentYeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            opponentYeti.MaxHealth = 10;
            opponentYeti.CurrentHealth = 1;

            GameEngine.GameState.CurrentPlayerPlayZone[0] = playerYeti;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = opponentYeti;

            player.AddCardToHand(circleOfHealing);
            player.PlayCard(circleOfHealing, null);

            // Circle of healing shouldn't be affected by spell power so it should just heal for 4
            Assert.AreEqual(5, playerYeti.CurrentHealth, "Verify player yeti was healed");
            Assert.AreEqual(5, opponentYeti.CurrentHealth, "Verify opponent yeti was healed");
        }

        /// <summary>
        /// Validate basic spell damage card Fireball
        /// </summary>
        [TestMethod]
        public void Fireball()
        {
            var fireball = HearthEntityFactory.CreateCard<Fireball>();
            fireball.Owner = player;
            fireball.CurrentManaCost = 0;

            player.Hand.Add(fireball);

            var rag = new RagnarostheFirelord(3);
            GameEngine.GameState.Board.OpponentPlayZone.Add(rag);

            player.PlayCard(fireball, rag);

            Assert.AreEqual(2, rag.CurrentHealth, "Verify rag took 6 damage");

            player.Hand.Add(fireball);
            player.PlayCard(fireball, opponent);
            Assert.AreEqual(24, opponent.Health, "Verify the opponent took 6 damage");
        }

        /// <summary>
        /// Gives 2 mana this turn only
        /// </summary>
        [TestMethod]
        public void Innervate()
        {
            var innervate = HearthEntityFactory.CreateCard<Innervate>();

            player.MaxMana = Constants.MAX_MANA_CAPACITY;
            player.Mana = Constants.MAX_MANA_CAPACITY - 2;

            player.AddCardToHand(innervate);
            player.PlayCard(innervate, null);

            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, player.Mana, "Verify player got 2 mana");

            player.Mana = Constants.MAX_MANA_CAPACITY - 1;
            player.AddCardToHand(innervate);
            player.PlayCard(innervate, null);

            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, player.Mana, "Verify mana can't go above maximum");
        }

        /// <summary>
        /// Gives 1 mana this turn only
        /// </summary>
        [TestMethod]
        public void TheCoin()
        {
            var theCoin = HearthEntityFactory.CreateCard<TheCoin>();

            player.MaxMana = Constants.MAX_MANA_CAPACITY;
            player.Mana = Constants.MAX_MANA_CAPACITY - 1;

            player.AddCardToHand(theCoin);
            player.PlayCard(theCoin, null);

            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, player.Mana, "Verify player got 1 mana");

            player.Mana = Constants.MAX_MANA_CAPACITY;
            player.AddCardToHand(theCoin);
            player.PlayCard(theCoin, null);

            Assert.AreEqual(Constants.MAX_MANA_CAPACITY, player.Mana, "Verify mana can't go above maximum");
        }

        /// <summary>
        /// Validate bonus spell power
        /// </summary>
        [TestMethod]
        public void BonusSpellPower()
        {
            var ancientMage = HearthEntityFactory.CreateCard<AncientMage>();
            ancientMage.Owner = player;
            ancientMage.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var azureDrake = HearthEntityFactory.CreateCard<AzureDrake>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;
            GameEngine.GameState.CurrentPlayerPlayZone[1] = azureDrake;

            player.Hand.Add(ancientMage);
            player.PlayCard(ancientMage, null, 1);

            // Yeti should have +1 SP and Azure Drake should have +2
            Assert.AreEqual(1, yeti.BonusSpellPower, "Verify yeti bonus spell power");
            Assert.AreEqual(2, azureDrake.BonusSpellPower, "Verify azure drake bonus spell power");
            Assert.AreEqual(3, player.BonusSpellPower, "Verify player's bonus spell power");

            var fireball = HearthEntityFactory.CreateCard<Fireball>();
            fireball.Owner = player;
            fireball.CurrentManaCost = 0;

            player.Hand.Add(fireball);

            // Fireball should do 6 + 3 damage
            player.PlayCard(fireball, opponent);
            Assert.AreEqual(21, opponent.Health, "Verify the opponent took 9 points of damage");
        }
    }
}
