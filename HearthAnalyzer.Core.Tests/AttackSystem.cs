using System;
using System.Security.Cryptography.X509Certificates;
using HearthAnalyzer.Core.Cards.Weapons;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HearthAnalyzer.Core;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;

namespace HearthAnalyzer.Core.Tests
{

    [TestClass]
    public class AttackSystem
    {
        private ChillwindYeti yeti1;
        private ChillwindYeti yeti2;
        private BloodfenRaptor raptor1;
        private FieryWarAxe fieryWarAxe;
        private Gorehowl gorehowl;

        private Warlock player;

        [TestInitialize]
        public void Setup()
        {
            yeti1 = new ChillwindYeti(1);
            yeti2 = new ChillwindYeti(2);
            raptor1 = new BloodfenRaptor(3);

            fieryWarAxe = new FieryWarAxe(4);
            gorehowl = new Gorehowl(5);

            player = new Warlock()
            {
                Health = 30,
                Weapon = fieryWarAxe
            };

            fieryWarAxe.Owner = player;

            GameEngine.Initialize(player, null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameEngine.Uninitialize();
        }

        [TestMethod]
        public void BasicMinionsAttacking()
        {
            // 4/5 attacking into another 4/5 should yeild two 4/1s
            yeti1.Attack(yeti2, GameEngine.GameState);

            Assert.AreEqual(1, yeti1.CurrentHealth, "Verify Yeti_1 is at 1 health.");
            Assert.AreEqual(1, yeti2.CurrentHealth, "Verify Yeti_2 is at 1 health.");

            // Now, kill yeti 1 with raptor 1. This should kill both the yeti and the raptor
            raptor1.Attack(yeti1, GameEngine.GameState);

            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(yeti1), "Verify Yeti_1 is dead");
            Assert.IsTrue(GameEngine.DeadMinionsThisTurn.Contains(raptor1), "Verify Raptor_1 is dead");
            Assert.AreEqual(-2, yeti1.CurrentHealth, "Verify Yeti_1 is at -2 health");
            Assert.AreEqual(-2, raptor1.CurrentHealth, "Verify Raptor_2 is at -2 health");
        }

        [TestMethod]
        public void BasicWeaponsAttacking()
        {
            player.Attack(yeti1, GameEngine.GameState);
            Assert.AreEqual(2, yeti1.CurrentHealth, "Verify Yeti_1 is at 2 health");
            Assert.AreEqual(26, player.Health, "Verify the player is now at 26 health");
            Assert.AreEqual(1, fieryWarAxe.Durability, "Verify the war axe is now at 1 durability");

            player.Attack(yeti1, GameEngine.GameState);
            Assert.AreEqual(-1, yeti1.CurrentHealth, "Verify Yeti_1 is at -1 health");
            Assert.AreEqual(22, player.Health, "Verify the player is now at 22 health");
            Assert.AreEqual(0, fieryWarAxe.Durability, "Verify the war axe is now at 0 durability");

            Assert.IsNull(player.Weapon, "Verify that the player no longer has the weapon equipped");
            Assert.IsTrue(player.Graveyard.Contains(fieryWarAxe), "Verify that the war axe is now in the graveyard");
        }

        [TestMethod]
        public void Gorehowl()
        {
            // Make a super 4/28 yeti
            yeti1.TakeBuff(0, 23);

            player.Weapon = gorehowl;
            gorehowl.Owner = player;

            int gorehowlAttack = 7;
            int yetiHealth = 28;
            int playerHealth = 30;

            for (int i = 0; i < 7; i++)
            {
                player.Attack(yeti1, GameEngine.GameState);

                yetiHealth -= gorehowlAttack;
                gorehowlAttack--;
                playerHealth -= yeti1.CurrentAttackPower;

                Assert.AreEqual(yetiHealth, yeti1.CurrentHealth, "Verify that the yeti's health is {0}", yetiHealth);
                Assert.AreEqual(gorehowlAttack, gorehowl.CurrentAttackPower, "Verify that gorehowl's current attack power is {0}", gorehowlAttack);
                Assert.AreEqual(playerHealth, player.Health, "Verify that the player is at {0} health", playerHealth);
            }

            Assert.IsNull(player.Weapon, "Verify that gorehowl broke");
            Assert.IsTrue(player.Graveyard.Contains(gorehowl));
        }
    }
}
