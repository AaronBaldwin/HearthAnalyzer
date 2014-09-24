using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents the overall game state
    /// </summary>
    public class GameState
    {

        internal GameState(BasePlayer player, BasePlayer opponent, GameBoard board = null, int turnNumber = 0, BasePlayer currentPlayer = null)
        {
            this.Player = player;
            this.Opponent = opponent;
            this.Board = board ?? new GameBoard();
            this.TurnNumber = turnNumber;
            this.CurrentPlayer = currentPlayer ?? (GameEngine.Random.Next(0, 2) % 2 == 0 ? this.Player : this.Opponent);
        }

        /// <summary>
        /// The first player (you)
        /// </summary>
        public BasePlayer Player;

        /// <summary>
        /// The opponent
        /// </summary>
        public BasePlayer Opponent;

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
        public BasePlayer CurrentPlayer;

        /// <summary>
        /// The current player's play zone on the board
        /// </summary>
        public List<BaseCard> CurrentPlayerPlayZone
        {
            get
            {
                return (this.CurrentPlayer == this.Player) ? this.Board.PlayerPlayZone : this.Board.OpponentPlayZone;
            }
        }

        /// <summary>
        /// The waiting player's play zone on the board
        /// </summary>
        public List<BaseCard> WaitingPlayerPlayZone
        {
            get
            {
                return (this.CurrentPlayer == this.Player) ? this.Board.OpponentPlayZone : this.Board.PlayerPlayZone;
            }
        }
    }
}
