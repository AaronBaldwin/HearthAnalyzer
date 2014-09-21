using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents the overall game state
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// The first player (you)
        /// </summary>
        public Player Player;

        /// <summary>
        /// The opponent
        /// </summary>
        public Player Opponent;

        /// <summary>
        /// The current game board
        /// </summary>
        public GameBoard Board;

        /// <summary>
        /// The current turn number
        /// </summary>
        public int TurnNumber;

        /// <summary>
        /// The current player
        /// </summary>
        public Player CurrentPlayer;
    }
}
