using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Cards.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            player.AddCardToHand(sergeant);
            player.PlayCard(sergeant, yeti);

            Assert.AreEqual(yeti.OriginalAttackPower + 2, yeti.CurrentAttackPower, "Verify yeti got an attack buff");

            // Verify buffing enemy minion
            player.AddCardToHand(sergeant);
            player.PlayCard(sergeant, faerie);

            Assert.AreEqual(faerie.OriginalAttackPower + 2, faerie.CurrentAttackPower, "Verify faerie got an attack buff");

            // Verify can't buff players
            player.AddCardToHand(sergeant);
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

            player.AddCardToHand(ooze);
            player.PlayCard(ooze, null);

            Assert.IsNull(opponent.Weapon, "Verify opponent weapon got destroyed");

            // Just make sure it doesn't poop itself if there are no weapons to destroy
            player.AddCardToHand(ooze);
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

            player.AddCardToHand(aldor);
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

            player.AddCardToHand(alex);
            player.PlayCard(alex, opponent);

            Assert.AreEqual(15, opponent.Health, "Verify opponent health is set to 15");

            player.AddCardToHand(alex);
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
            player.AddCardToHand(brewmaster);

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
                player.AddCardToHand(brewmaster);
            }

            // Verify playing on an empty board with no target is valid
            player.PlayCard(brewmaster, null);

            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Contains(brewmaster), "Verify brewmaster was played");

            GameEngine.GameState.Board.RemoveCard(brewmaster);

            // Verify playing brewmaster returns the friendly minion back to the hand
            GameEngine.GameState.CurrentPlayerPlayZone[0] = yeti;

            player.AddCardToHand(brewmaster);
            player.PlayCard(brewmaster, yeti);

            Assert.IsTrue(player.Hand.Contains(yeti), "Verify yeti was returned to the owner's hand");
            Assert.IsFalse(GameEngine.GameState.CurrentPlayerPlayZone.Contains(yeti), "Verify yeti is no longer on the baord");
            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Contains(brewmaster), "Verify brewmaster is on the baord");

            GameEngine.GameState.Board.RemoveCard(brewmaster);

            // Verify playing brewmaster and targeting an enemy minion is not valid
            player.RemoveCardFromHand(yeti);
            GameEngine.GameState.WaitingPlayerPlayZone[0] = yeti;
            player.AddCardToHand(brewmaster);

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

            player.AddCardToHand(ancientMage);
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

            player.AddCardToHand(weaponsmith);
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

            player.AddCardToHand(arcaneGolem);
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
        /// Destroy a minion with 7 attack or more
        /// </summary>
        [TestMethod]
        public void BigGameHunter()
        {
            var hunter = HearthEntityFactory.CreateCard<BigGameHunter>();
            hunter.CurrentManaCost = 0;

            var yeti = HearthEntityFactory.CreateCard<ChillwindYeti>();
            yeti.TakeBuff(10, 0);

            var faerie = HearthEntityFactory.CreateCard<FaerieDragon>();

            // No valid target, no supplied target
            player.AddCardToHand(hunter);
            player.PlayCard(hunter, null);
            Assert.IsTrue(GameEngine.GameState.CurrentPlayerPlayZone.Contains(hunter));

            GameEngine.GameState.Board.RemoveCard(hunter);
            player.AddCardToHand(hunter);

            // No valid target, supplied target
            try
            {
                player.PlayCard(hunter, yeti);
                Assert.Fail("Shouldn't be able to mark yeti as target");
            }
            catch (InvalidOperationException)
            {
            }
            finally
            {
                GameEngine.GameState.Board.RemoveCard(hunter);
                player.AddCardToHand(hunter);
            }

            GameEngine.GameState.WaitingPlayerPlayZone[0] = yeti;
            GameEngine.GameState.WaitingPlayerPlayZone[1] = faerie;

            // Valid target, no supplied target
            try
            {
                player.PlayCard(hunter, null);
                Assert.Fail("Can't play without a target");
            }
            catch (InvalidOperationException)
            {
            }
            finally
            {
                GameEngine.GameState.Board.RemoveCard(hunter);
                player.AddCardToHand(hunter);
            }

            // Valid target on board, invalid supplied target
            try
            {
                player.PlayCard(hunter, faerie);
                Assert.Fail("Shouldn't be able to target invalid minions");
            }
            catch (InvalidOperationException)
            {
            }
            finally
            {
                GameEngine.GameState.Board.RemoveCard(hunter);
                player.AddCardToHand(hunter);
            }

            // Valid target, supplied target
            player.PlayCard(hunter, yeti);

            Assert.IsTrue(GameEngine.DeadCardsThisTurn.Contains(yeti), "Verify yeti died");
        }

        /// <summary>
        /// All minions lose divine shield. Gain +3/3 for each divine shield lost.
        /// </summary>
        [TestMethod]
        public void BloodKnight()
        {
            var bloodKnight = HearthEntityFactory.CreateCard<BloodKnight>();
            bloodKnight.CurrentManaCost = 0;

            // No divine shields
            player.AddCardToHand(bloodKnight);
            player.PlayCard(bloodKnight, null);

            Assert.AreEqual(Cards.Minions.BloodKnight.ATTACK_POWER, bloodKnight.CurrentAttackPower, "Verify attack unchanged");
            Assert.AreEqual(Cards.Minions.BloodKnight.HEALTH, bloodKnight.MaxHealth, "Verify health unchanged");

            GameEngine.GameState.Board.RemoveCard(bloodKnight);
            player.AddCardToHand(bloodKnight);

            // Multiple divine shields
            var playerArgentSquire = HearthEntityFactory.CreateCard<ArgentSquire>();
            var opponentArgentSquire = HearthEntityFactory.CreateCard<ArgentSquire>();

            GameEngine.GameState.CurrentPlayerPlayZone[0] = playerArgentSquire;
            GameEngine.GameState.WaitingPlayerPlayZone[0] = opponentArgentSquire;

            player.PlayCard(bloodKnight, null);

            Assert.IsFalse(playerArgentSquire.HasDivineShield, "Verify player argent squire lost divine shield");
            Assert.IsFalse(opponentArgentSquire.HasDivineShield, "Verify opponent argent squire lost divine shield");
            Assert.AreEqual(Cards.Minions.BloodKnight.ATTACK_POWER + 6, bloodKnight.CurrentAttackPower, "Verify bloodknight attack was buffed");
            Assert.AreEqual(Cards.Minions.BloodKnight.HEALTH + 6, bloodKnight.MaxHealth, "Verify bloodknight health was buffed");
        }

        /// <summary>
        /// Remove one durability from opponent's weapon
        /// </summary>
        [TestMethod]
        public void BloodsailCorsairTest()
        {
            var bloodsail = HearthEntityFactory.CreateCard<BloodsailCorsair>();
            bloodsail.CurrentManaCost = 0;

            var gorehowl = HearthEntityFactory.CreateCard<Gorehowl>();
            opponent.Weapon = gorehowl;
            gorehowl.WeaponOwner = opponent;

            player.AddCardToHand(bloodsail);
            player.PlayCard(bloodsail, null);

            Assert.IsNull(opponent.Weapon, "Verify opponent weapon got destroyed");
            Assert.IsTrue(GameEngine.DeadCardsThisTurn.Contains(gorehowl), "Verify gorehowl got destroyed");

            var fieryWarAxe = HearthEntityFactory.CreateCard<FieryWarAxe>();
            opponent.Weapon = fieryWarAxe;
            fieryWarAxe.WeaponOwner = opponent;

            GameEngine.GameState.Board.RemoveCard(bloodsail);
            player.AddCardToHand(bloodsail);

            player.PlayCard(bloodsail, null);

            Assert.AreEqual(FieryWarAxe.DURABILITY - 1, fieryWarAxe.Durability, "Verify fiery war axe lost a durability");
        }

        /// <summary>
        /// Gain attack equal the attack of your weapon
        /// </summary>
        [TestMethod]
        public void BloodsailRaiderTest()
        {
            var raider = HearthEntityFactory.CreateCard<BloodsailRaider>();
            raider.CurrentManaCost = 0;

            var gorehowl = HearthEntityFactory.CreateCard<Gorehowl>();
            gorehowl.WeaponOwner = player;
            player.Weapon = gorehowl;

            player.AddCardToHand(raider);
            player.PlayCard(raider, null);

            Assert.AreEqual(BloodsailRaider.ATTACK_POWER + gorehowl.CurrentAttackPower, raider.CurrentAttackPower, "Verify raider got buffed");
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

            player.AddCardToHand(frostElemental);
            frostElemental.CurrentManaCost = 0;

            player.PlayCard(frostElemental, faerie);

            Assert.IsTrue(faerie.IsFrozen, "Verify the faerie dragon is frozen");

            // Verify targeting a character
            frostElemental = HearthEntityFactory.CreateCard<FrostElemental>();
            player.AddCardToHand(frostElemental);
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

            player.AddCardToHand(commando);
            commando.CurrentManaCost = 0;

            player.PlayCard(commando, raptor, 0);
            Assert.AreEqual(commando, GameEngine.GameState.Board.PlayerPlayZone[0], "Verify that the commando was placed on the board");
            Assert.IsTrue(GameEngine.DeadCardsThisTurn.Contains(raptor), "Verify the raptor died due to battlecry");
        }
    }
}
