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
        /// Moves a card from one zone position to another zone position
        /// </summary>
        /// <param name="id">The card to move</param>
        /// <param name="srcZone">The source zone</param>
        /// <param name="srcPos">The source position</param>
        /// <param name="destZone">The destination zone</param>
        /// <param name="destPos">The destination position</param>
        public void MoveCard(int id, Zones srcZone, int srcPos, Zones destZone, int destPos)
        {
            // First check that the card is even there
            BaseCard cardToMove = null;
            List<BaseCard> srcZoneContainer = null;
            List<BaseCard> destZoneContainer = null;

            switch (srcZone)
            {
                case Zones.FRIENDLY_HAND:
                    srcZoneContainer = this.Player.Hand;
                    cardToMove = this.Player.Hand.ElementAtOrDefault(srcPos);
                    break;

                case Zones.FRIENDLY_PLAY:
                    srcZoneContainer = this.Board.PlayerZone;
                    cardToMove = this.Board.PlayerZone.ElementAtOrDefault(srcPos);
                     break;

                case Zones.OPPOSING_HAND:
                    srcZoneContainer = this.Opponent.Hand;
                    cardToMove = this.Opponent.Hand.ElementAtOrDefault(srcPos);
                     break;

                case Zones.OPPOSING_PLAY:
                    srcZoneContainer = this.Board.OpponentZone;
                    cardToMove = this.Board.OpponentZone.ElementAtOrDefault(srcPos);
                    break;

                default:
                    throw new InvalidOperationException(string.Format("This shouldn't happen (unless a new game mechanic was added). Don't be moving things from {0}", srcZone));
            }


            if (cardToMove == null || cardToMove.Id != id)
            {
                throw new InvalidOperationException(string.Format("Could not find {0} at {1}[{2}], found {3} instead!", id, srcZone, srcPos, cardToMove));
            }

            // Now move it
            switch(destZone)
            {
                case Zones.FRIENDLY_PLAY:
                    destZoneContainer = this.Board.PlayerZone;
                    break;

                case Zones.FRIENDLY_HAND:
                    destZoneContainer = this.Player.Hand;
                    break;

                case Zones.FRIENDLY_GRAVEYARD:
                    destZoneContainer = this.Player.Graveyard;
                    break;

                case Zones.OPPOSING_PLAY:
                    destZoneContainer = this.Board.OpponentZone;
                    break;

                case Zones.OPPOSING_HAND:
                    destZoneContainer = this.Opponent.Hand;
                    break;

                case Zones.OPPOSING_GRAVEYARD:
                    destZoneContainer = this.Opponent.Graveyard;
                    break;
            }

            destZoneContainer[destPos] = cardToMove;
            srcZoneContainer[srcPos] = null;

            Logger.Instance.Debug(string.Format("Moving {0}[1}\tFROM {2}[{3}]\tTO {4}[{5}]", cardToMove, id, srcZone, srcPos, destZone, destPos));
        }

        /// <summary>
        /// Represents the zones
        /// </summary>
        public enum Zones
        {
            FRIENDLY_HAND,
            OPPOSING_HAND,
            FRIENDLY_PLAY,
            OPPOSING_PLAY,
            FRIENDLY_GRAVEYARD,
            OPPOSING_GRAVEYARD
        }
    }
}
