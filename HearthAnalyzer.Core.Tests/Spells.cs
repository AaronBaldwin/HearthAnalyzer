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
    public class Spells
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
        /// Validate basic spell damage card Fireball
        /// </summary>
        [TestMethod]
        public void Fireball()
        {
            player.Hand.Add(new Fireball(1));
            player.Hand.Add(new Fireball(2));

            player.Hand.ForEach(card => card.CurrentManaCost = 0);

            var rag = new RagnarostheFirelord(3);
            GameEngine.GameState.Board.OpponentPlayZone.Add(rag);

            player.PlayCard(player.Hand.First(), rag);

            Assert.AreEqual(2, rag.CurrentHealth, "Verify rag took 6 damage");

            player.PlayCard(player.Hand.First(), opponent);
            Assert.AreEqual(24, opponent.Health, "Verify the opponent took 6 damage");
        }
    }
}
