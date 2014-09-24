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
        /// <summary>
        /// The max number of cards in the play zones
        /// </summary>
        public const int MAX_CARDS_IN_PLAY_ZONE = 7;

        public GameBoard()
        {
            this.PlayerPlayZone = new List<BaseCard>(MAX_CARDS_IN_PLAY_ZONE);
            this.PlayerPlayZone.AddRange(Enumerable.Repeat<BaseCard>(null, MAX_CARDS_IN_PLAY_ZONE));

            this.OpponentPlayZone = new List<BaseCard>(MAX_CARDS_IN_PLAY_ZONE);
            this.OpponentPlayZone.AddRange(Enumerable.Repeat<BaseCard>(null, MAX_CARDS_IN_PLAY_ZONE));
        }

        /// <summary>
        /// The player's side of the board
        /// </summary>
        public List<BaseCard> PlayerPlayZone;

        /// <summary>
        /// The opponent's side of the baord
        /// </summary>
        public List<BaseCard> OpponentPlayZone;
    }
}
