using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Minions;
using HearthAnalyzer.Core.Heroes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class MultiCardEffectTests
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
        /// Effect one: Draw 2 cards
        /// Effect two: Restore 5 health
        /// </summary>
        [TestMethod]
        public void AncientOfLore()
        {
            // Verify invalid playing of card
            var lore = HearthEntityFactory.CreateCard<AncientOfLore>();
            lore.CurrentManaCost = 0;
            lore.Owner = player;

            player.AddCardToHand(lore);
            try
            {
                player.PlayCard(lore, null);
                Assert.Fail("You need to choose a card effect to play it!");
            }
            catch (InvalidOperationException)
            {
            }
            finally
            {
                GameEngine.GameState.Board.RemoveCard(lore);
                player.AddCardToHand(lore);
            }

            // Verify draw 2 cards
            player.PlayCard(lore, null, 0, CardEffect.FIRST);
            Assert.AreEqual(27, player.Health, "Verify the player drew two fatigue cards");

            GameEngine.GameState.Board.RemoveCard(lore);
            player.AddCardToHand(lore);

            // Verify heal character
            opponent.Health = 25;
            player.PlayCard(lore, opponent, 0, CardEffect.SECOND);

            Assert.AreEqual(30, opponent.Health, "Verify the opponent got healed");
        }

        /// <summary>
        /// Effect one: Attack +5
        /// Effect two: Health +5 and Taunt
        /// </summary>
        [TestMethod]
        public void AncientOfWar()
        {
            var war = HearthEntityFactory.CreateCard<AncientOfWar>();
            war.CurrentManaCost = 0;
            war.Owner = player;

            player.AddCardToHand(war);

            // Verify Attack Buff
            player.PlayCard(war, null, 0, CardEffect.FIRST);
            Assert.AreEqual(war.OriginalAttackPower + 5, war.CurrentAttackPower, "Verify attack buff");

            GameEngine.GameState.Board.RemoveCard(war);
            player.AddCardToHand(war);

            // Verify health and taunt
            player.PlayCard(war, null, 0, CardEffect.SECOND);
            Assert.AreEqual(10, war.MaxHealth, "Verify max health");
            Assert.AreEqual(war.MaxHealth, war.CurrentHealth, "Verify current health");
            Assert.IsTrue(war.HasTaunt, "Verify taunt");
        }
    }
}
