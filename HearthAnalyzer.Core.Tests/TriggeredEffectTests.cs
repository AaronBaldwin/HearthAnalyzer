using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Cards.Spells;
using HearthAnalyzer.Core.Cards.Weapons;
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
            var acolyte = HearthEntityFactory.CreateCard<AcolyteOfPain>();
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

        /// <summary>
        /// At the beginning of your turn, switch places with a random minion in your hand
        /// </summary>
        [TestMethod]
        public void AlarmoBot()
        {
            var alarmo = HearthEntityFactory.CreateCard<AlarmoBot>();
            alarmo.CurrentManaCost = 0;
            alarmo.Owner = player;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var rag = HearthEntityFactory.CreateCard<RagnarostheFirelord>();
            var azureDrake = HearthEntityFactory.CreateCard<AzureDrake>();
            var weapon = HearthEntityFactory.CreateCard<FieryWarAxe>();
            var fireball = HearthEntityFactory.CreateCard<Fireball>();

            player.Hand.AddRange(new List<BaseCard>() {yeti, rag, azureDrake, weapon, fireball, alarmo});

            player.PlayCard(alarmo, null);

            GameEngine.EndTurn();
            GameEngine.EndTurn();

            var expectedMinionsOnBoard = new List<BaseCard>() {yeti, rag, azureDrake};
            
            Assert.IsTrue(player.Hand.Contains(alarmo), "Verify alarmobot ended up back in hand");
            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Any(expectedMinionsOnBoard.Contains), "Verify a random minion was placed on board");
        }

        /// <summary>
        /// Verify that alarmobot summon doesn't have battlecry
        /// </summary>
        [TestMethod]
        public void AlarmoBotSummonNoBattlecry()
        {
            var alarmo = HearthEntityFactory.CreateCard<AlarmoBot>();
            alarmo.CurrentManaCost = 0;
            alarmo.Owner = player;

            var azureDrake = HearthEntityFactory.CreateCard<AzureDrake>();

            player.Hand.AddRange(new List<BaseCard>() {alarmo, azureDrake});

            player.PlayCard(alarmo, null);

            GameEngine.EndTurn();

            var handSize = player.Hand.Count;

            GameEngine.EndTurn();

            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Contains(azureDrake), "Verify azure drake was summoned");
            Assert.IsTrue(player.Hand.Count < handSize + 2, "Verify an extra card wasn't drawn from azure drake being summoned");
        }

        /// <summary>
        /// Verify that when a spell is casted, a fireball is added to the player's hand
        /// </summary>
        [TestMethod]
        public void ArchmageAntonidas()
        {
            var antonidas = HearthEntityFactory.CreateCard<ArchmageAntonidas>();
            antonidas.Owner = player;
            antonidas.CurrentManaCost = 0;

            var fireball = HearthEntityFactory.CreateCard<Fireball>();
            fireball.CurrentManaCost = 0;

            player.AddCardToHand(antonidas);
            player.AddCardToHand(fireball);

            player.PlayCard(antonidas, null);
            player.PlayCard(fireball, opponent);

            // Yo dawg, I heard you liked fireballs...
            var newFireball = player.Hand.FirstOrDefault();

            Assert.IsNotNull(newFireball, "Verify player got a new card in hand");
            Assert.IsFalse(newFireball.Id == fireball.Id, "Verify it is a new fireball");
        }

        /// <summary>
        /// Verify that when a friendly minion is damaged, gain +1 armor
        /// </summary>
        [TestMethod]
        public void Armorsmith()
        {
            var armorsmith = HearthEntityFactory.CreateCard<Armorsmith>();
            armorsmith.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;
            GameEngine.GameState.CurrentPlayerPlayZone[1] = faerie;

            player.AddCardToHand(armorsmith);
            player.PlayCard(armorsmith, null);

            // Deal damage to yeti and faerie
            yeti.TakeDamage(1);
            faerie.TakeDamage(1);

            Assert.AreEqual(2, player.Armor, "Verify player gained armor");
        }

        /// <summary>
        /// Verify that heals do damage instead
        /// </summary>
        [TestMethod]
        public void AuchenaiSoulpriest()
        {
            var auchenai = HearthEntityFactory.CreateCard<AuchenaiSoulpriest>();
            auchenai.CurrentManaCost = 0;

            var circleOfHealing = HearthEntityFactory.CreateCard<CircleofHealing>();

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            GameEngine.GameState.WaitingPlayerPlayZone[0] = yeti;

            player.AddCardToHand(auchenai);
            player.AddCardToHand(circleOfHealing);

            player.PlayCard(auchenai, null);
            player.PlayCard(circleOfHealing, null);

            Assert.AreEqual(auchenai.MaxHealth - 4, auchenai.CurrentHealth, "Verify friendly minion took damage instead of heal");
            Assert.AreEqual(yeti.MaxHealth - 4, yeti.CurrentHealth, "Verify enemy minion took damage instead of heal");
        }

        /// <summary>
        /// Verify that heals do damage instead and that healing spells are affected by spell power
        /// </summary>
        [TestMethod]
        public void AuchenaiSoulpriestWithSpellPower()
        {
            var auchenai = HearthEntityFactory.CreateCard<AuchenaiSoulpriest>();
            auchenai.CurrentManaCost = 0;
            auchenai.BonusSpellPower = 1;

            var circleOfHealing = HearthEntityFactory.CreateCard<CircleofHealing>();

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            GameEngine.GameState.WaitingPlayerPlayZone[0] = yeti;

            player.AddCardToHand(auchenai);
            player.AddCardToHand(circleOfHealing);

            player.PlayCard(auchenai, null);
            player.PlayCard(circleOfHealing, null);

            Assert.AreEqual(auchenai.MaxHealth - 5, auchenai.CurrentHealth, "Verify friendly minion took damage instead of heal");
            Assert.AreEqual(yeti.MaxHealth - 5, yeti.CurrentHealth, "Verify enemy minion took damage instead of heal");
        }

        /// <summary>
        /// At end of turn, deal 2 damage to all other characters
        /// </summary>
        [TestMethod]
        public void BaronGeddon()
        {
            var baron = HearthEntityFactory.CreateCard<BaronGeddon>();
            baron.CurrentManaCost = 0;

            var playerYeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var opponentYeti = HearthEntityFactory.CreateCard<ChillwindYeti>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = playerYeti;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = opponentYeti;

            player.AddCardToHand(baron);
            player.PlayCard(baron, null);

            GameEngine.EndTurn();

            Assert.AreEqual(28, player.Health, "Verify player took damage");
            Assert.AreEqual(27, opponent.Health, "Verify opponent took damage"); // One extra damage from drawing fatigue
            Assert.AreEqual(playerYeti.MaxHealth - 2, playerYeti.CurrentHealth, "Verify player yeti took damage");
            Assert.AreEqual(opponentYeti.MaxHealth - 2, opponentYeti.CurrentHealth, "Verify opponent yeti took damage");
        }

        /// <summary>
        /// Your minions trigger their deathrattles twice
        /// </summary>
        [TestMethod]
        public void BaronRivendare()
        {
            var baron = HearthEntityFactory.CreateCard<BaronRivendare>();

            var playerAbom = HearthEntityFactory.CreateCard<Abomination>();
            playerAbom.CurrentManaCost = 0;

            var opponentAbom = HearthEntityFactory.CreateCard<Abomination>();
            opponentAbom.TakeBuff(0, 10); // Just so it doesn't die to double player abom deathrattle
            opponentAbom.ApplyStatusEffects(MinionStatusEffects.CHARGE);
            opponentAbom.CurrentManaCost = 0;

            GameEngine.GameState.CurrentPlayerPlayZone[0] = baron;

            player.AddCardToHand(playerAbom);
            opponent.AddCardToHand(opponentAbom);

            player.PlayCard(playerAbom, null);

            GameEngine.EndTurn();

            // Kill the player Abom which should trigger its deathrattle twice doing 4 damage to everybody
            opponent.PlayCard(opponentAbom, null);
            opponentAbom.Attack(playerAbom);

            Assert.AreEqual(baron.MaxHealth - 4, baron.CurrentHealth, "Verify baron got hit by deathrattle twice");
            Assert.AreEqual(6, opponentAbom.CurrentHealth, "Verify opponent abom took 4 damage from retaliation and 4 from double deathrattle");
        }

        /// <summary>
        /// At end of turn, give another random friendly minion +1 health
        /// </summary>
        [TestMethod]
        public void BloodImp()
        {
            var imp = HearthEntityFactory.CreateCard<BloodImp>();
            imp.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();
            
            player.AddCardToHand(imp);
            player.PlayCard(imp, null);

            GameEngine.EndTurn();

            Assert.AreEqual(1, imp.MaxHealth, "Verify imp didn't buff itself");

            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;
            GameEngine.GameState.CurrentPlayerPlayZone[1] = faerie;

            GameEngine.EndTurn();
            GameEngine.EndTurn();

            if (yeti.MaxHealth != 6 && faerie.MaxHealth != 3)
            {
                Assert.Fail("No other minion got bonus health.");
            }
        }
    }
}
