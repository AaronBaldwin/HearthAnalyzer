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
        /// Sends a friendly minion on the battlefield back to owner's hand
        /// </summary>
        [TestMethod]
        public void AncientBrewmaster()
        {
            var brewmaster = HearthEntityFactory.CreateCard<AncientBrewmaster>();
            brewmaster.Owner = player;
            brewmaster.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();

            // Verify exception when playing on an empty board with a target
            player.Hand.Add(brewmaster);

            try
            {
                player.PlayCard(brewmaster, yeti);
                Assert.Fail("Can't play brewmaster on an empty board with a target!");
            }
            catch (InvalidOperationException)
            {
            }
            finally
            {
                GameEngine.GameState.Board.RemoveCard(brewmaster);
                player.Hand.Add(brewmaster);
            }

            // Verify playing on an empty board with no target is valid
            player.PlayCard(brewmaster, null);

            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Contains(brewmaster), "Verify brewmaster was played");

            GameEngine.GameState.Board.RemoveCard(brewmaster);

            // Verify playing brewmaster returns the friendly minion back to the hand
            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;

            player.Hand.Add(brewmaster);
            player.PlayCard(brewmaster, yeti);

            Assert.IsTrue(player.Hand.Contains(yeti), "Verify yeti was returned to the owner's hand");
            Assert.IsFalse(GameEngine.GameState.CurrentPlayerPlayZone.Contains(yeti), "Verify yeti is no longer on the baord");
            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Contains(brewmaster), "Verify brewmaster is on the baord");

            GameEngine.GameState.Board.RemoveCard(brewmaster);

            // Verify playing brewmaster and targeting an enemy minion is not valid
            player.Hand.Remove(yeti);
            GameEngine.GameState.WaitingPlayerPlayZone[0] = yeti;
            player.Hand.Add(brewmaster);

            try
            {
                player.PlayCard(brewmaster, yeti);
                Assert.Fail("Can't target enemy minions!");
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// Adjacent minions get +1 spell power
        /// </summary>
        [TestMethod]
        public void AncientMage()
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
        }

        /// <summary>
        /// Equip a 2/2 battle axe
        /// </summary>
        [TestMethod]
        public void ArathiWeaponsmith()
        {
            var weaponsmith = HearthEntityFactory.CreateCard<ArathiWeaponsmith>();
            weaponsmith.Owner = player;
            weaponsmith.CurrentManaCost = 0;

            player.Hand.Add(weaponsmith);
            player.PlayCard(weaponsmith, null);

            Assert.IsNotNull(player.Weapon, "Verify player has a weapon");
            Assert.IsTrue(player.Weapon is BattleAxe, "Verify the player's weapon is a battle axe");
        }

        /// <summary>
        /// Give opponent a mana crystal
        /// </summary>
        [TestMethod]
        public void ArcaneGolem()
        {
            var arcaneGolem = HearthEntityFactory.CreateCard<ArcaneGolem>();
            arcaneGolem.CurrentManaCost = 0;
            arcaneGolem.Owner = player;

            var opponentCurrentMaxMana = opponent.MaxMana;

            player.Hand.Add(arcaneGolem);
            player.PlayCard(arcaneGolem, null);

            Assert.AreEqual(opponentCurrentMaxMana + 1, opponent.MaxMana, "Verify opponent max mana increased");
        }

        /// <summary>
        /// Give a friendly minion divine shield
        /// </summary>
        [TestMethod]
        public void ArgentProtector()
        {
            var protector = HearthEntityFactory.CreateCard<ArgentProtector>();
            protector.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;

            player.AddCardToHand(protector);
            player.PlayCard(protector, yeti);

            Assert.IsTrue(yeti.HasDivineShield, "Verify divine shield");
        }

        /// <summary>
        /// Draw a card
        /// </summary>
        [TestMethod]
        public void AzureDrake()
        {
            var azureDrake = HearthEntityFactory.CreateCard<AzureDrake>();
            azureDrake.CurrentManaCost = 0;

            player.AddCardToHand(azureDrake);
            player.PlayCard(azureDrake, null);

            Assert.AreEqual(1, player.BonusSpellPower, "Verify bonus spell power");
            Assert.AreEqual(29, player.Health, "Verify player drew a fatigue card");
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
            Assert.IsTrue(GameEngine.DeadCardsThisTurn.Contains(raptor), "Verify the raptor died due to battlecry");
        }
    }
}
