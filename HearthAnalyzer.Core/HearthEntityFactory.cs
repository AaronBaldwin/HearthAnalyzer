using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Manages creation of cards
    /// </summary>
    public static class HearthEntityFactory
    {
        /// <summary>
        /// The internal monotomically increasing id
        /// </summary>
        internal static int Id;

        /// <summary>
        /// Resets the HearthEntityFactory
        /// </summary>
        public static void Reset()
        {
            Id = 0;
        }

        /// <summary>
        /// Creates a new instance of a card
        /// </summary>
        /// <typeparam name="T">The type of card to create</typeparam>
        public static T CreateCard<T>() where T : BaseCard
        {
            var cardId = Id++;

            Logger.Instance.DebugFormat("Creating instance of {0}[{1}]", typeof(T).FullName, cardId);

            return (T)Activator.CreateInstance(typeof(T), cardId);
        }

        /// <summary>
        /// Creates a new instance of a player
        /// </summary>
        /// <typeparam name="T">The type of player to create</typeparam>
        public static T CreatePlayer<T>() where T : BasePlayer
        {
            var cardId = Id++;

            Logger.Instance.DebugFormat("Creating instance of {0}[{1}]", typeof(T).FullName, cardId);

            return (T)Activator.CreateInstance(typeof(T), cardId);
        }
    }
}
