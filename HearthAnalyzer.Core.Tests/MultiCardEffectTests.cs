using System;
using System.Collections.Generic;
using System.Linq;
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
            var lore = HearthEntityFactory.CreateCard<AncientofLore>();
            lore.CurrentManaCost = 0;
            lore.Owner = player;

            player.Hand.Add(lore);
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
                player.Hand.Add(lore);
            }

            // Verify draw 2 cards
            player.PlayCard(lore, null, 0, CardEffect.FIRST);
            Assert.AreEqual(27, player.Health, "Verify the player drew two fatigue cards");

            GameEngine.GameState.Board.RemoveCard(lore);
            player.Hand.Add(lore);

            // Verify heal character
            opponent.Health = 25;
            player.PlayCard(lore, opponent, 0, CardEffect.SECOND);

            Assert.AreEqual(30, opponent.Health, "Verify the opponent got healed");
        }
    }
}
