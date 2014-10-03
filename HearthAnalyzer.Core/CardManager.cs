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
    public static class CardManager
    {
        /// <summary>
        /// The internal monotomically increasing card id
        /// </summary>
        internal static int CardId;

        /// <summary>
        /// Resets the CardManager
        /// </summary>
        public static void Reset()
        {
            CardId = 0;
        }

        /// <summary>
        /// Creates a new instance of a card
        /// </summary>
        /// <typeparam name="T">The type of card to create</typeparam>
        public static T CreateCard<T>() where T : BaseCard
        {
            var cardId = CardId++;

            Logger.Instance.Debug(string.Format("Creating instance of {0}[{1}]", typeof(T).FullName, cardId));

            return (T)Activator.CreateInstance(typeof(T), cardId);
        }
    }
}
