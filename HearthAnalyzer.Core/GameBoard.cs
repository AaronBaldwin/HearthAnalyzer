using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents the game board
    /// </summary>
    public class GameBoard
    {
        public GameBoard()
        {
            this.PlayerPlayZone = new List<BaseCard>(Constants.MAX_CARDS_ON_BOARD);
            this.PlayerPlayZone.AddRange(Enumerable.Repeat<BaseCard>(null, Constants.MAX_CARDS_ON_BOARD));

            this.OpponentPlayZone = new List<BaseCard>(Constants.MAX_CARDS_ON_BOARD);
            this.OpponentPlayZone.AddRange(Enumerable.Repeat<BaseCard>(null, Constants.MAX_CARDS_ON_BOARD));
        }

        /// <summary>
        /// The player's side of the board
        /// </summary>
        public List<BaseCard> PlayerPlayZone;

        /// <summary>
        /// The opponent's side of the baord
        /// </summary>
        public List<BaseCard> OpponentPlayZone;

        /// <summary>
        /// Removes a card from the baord
        /// </summary>
        /// <param name="card">The card to remove</param>
        public void RemoveCard(BaseCard card)
        {
            // First figure out which play zone it's in
            List<BaseCard> playZone;
            if (this.PlayerPlayZone.Contains(card))
            {
                playZone = this.PlayerPlayZone;
            }
            else if (this.OpponentPlayZone.Contains(card))
            {
                playZone = this.OpponentPlayZone;
            }
            else
            {
                Logger.Instance.DebugFormat("{0} was not found on the board. Perhaps it was removed already?", card);
                return;
            }

            // Next, remove the card and shift any cards necessary
            var index = playZone.IndexOf(card);
            playZone[index] = null;

            for (int i = index; i < Constants.MAX_CARDS_ON_BOARD - 1; i++)
            {
                playZone[i] = playZone[i + 1];
            }

            playZone[Constants.MAX_CARDS_ON_BOARD - 1] = null;
        }
    }
}
