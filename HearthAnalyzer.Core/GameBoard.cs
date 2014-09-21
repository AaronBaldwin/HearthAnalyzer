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
        /// The player's side of the board
        /// </summary>
        public List<BaseCard> PlayerZone;

        /// <summary>
        /// The opponent's side of the baord
        /// </summary>
        public List<BaseCard> OpponentZone;
    }
}
