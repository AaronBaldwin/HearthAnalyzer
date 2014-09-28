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
    }
}
