using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents the game engine and game loop for Hearthstone
    /// </summary>
    public static class GameEngine
    {
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

        /// <summary>
        /// Initialize the game engine
        /// </summary>
        /// <param name="player">Player 1</param>
        /// <param name="opponent">Player 2 (the opponent)</param>
        /// <param name="board">The current game board</param>
        /// <param name="turnNumber">The current turn number</param>
        /// <param name="currentPlayer">The current player</param>
        /// <param name="randomSeed">The random seed to use</param>
        public static void Initialize(BasePlayer player, BasePlayer opponent, GameBoard board = null, int turnNumber = 0, BasePlayer currentPlayer = null, int randomSeed = 0)
        {
            Random = randomSeed != 0 ? new Random(randomSeed) : new Random();

            GameEventManager.Initialize();
            GameState = new GameState(player, opponent, board, turnNumber, currentPlayer);
            DeadMinionsThisTurn = new List<BaseMinion>();
            DeadPlayersThisTurn = new List<BasePlayer>();
        }

        /// <summary>
        /// The current state of the game
        /// </summary>
        public static GameState GameState { get; private set; }

        /// <summary>
        /// The list of dead minions this turn
        /// </summary>
        public static List<BaseMinion> DeadMinionsThisTurn { get; private set; }

        /// <summary>
        /// The list of dead players this turn
        /// </summary>
        public static List<BasePlayer> DeadPlayersThisTurn { get; private set; } 

        /// <summary>
        /// The GameEngine's random number generator
        /// </summary>
        internal static Random Random { get; private set; }

        /// <summary>
        /// Apply damage from the attacker to the target
        /// </summary>
        /// <param name="attacker">The card doing the attacking</param>
        /// <param name="target">The object receiving the attack</param>
        /// <param name="isRetaliation">Whether or not the attack is a retaliation</param>
        public static void ApplyDamage(BaseCard attacker, IDamageableEntity target, bool isRetaliation = false)
        {
            // If the attacker is a spell card or hero power, you can't retaliate
            // If the target is a hero, he can't retaliate
            if (target is BaseMinion)
            {
                var targetMinion = (BaseMinion) target;

                if (!targetMinion.IsImmuneToDamage)
                {
                    targetMinion.TakeDamage(attacker.CurrentAttackPower);
                }

                if (!isRetaliation && (attacker is BaseMinion || attacker is BaseWeapon))
                {
                    if (attacker is BaseWeapon)
                    {
                        var weapon = (BaseWeapon) attacker;
                        ApplyDamage(targetMinion, weapon.Owner, isRetaliation: true);
                    }
                    else
                    {
                        // Then we must be attacking a minion
                        ApplyDamage(targetMinion, (IDamageableEntity)attacker, isRetaliation: true);
                    }
                }
            }
            else if (target is BasePlayer)
            {
                var targetPlayer = (BasePlayer) target;

                if (!targetPlayer.IsImmuneToDamage)
                {
                    targetPlayer.TakeDamage(attacker.CurrentAttackPower);
                }
            }
        }

        /// <summary>
        /// Moves a card from one zone position to another zone position
        /// </summary>
        /// <param name="id">The card to move</param>
        /// <param name="srcZone">The source zone</param>
        /// <param name="srcPos">The source position</param>
        /// <param name="destZone">The destination zone</param>
        /// <param name="destPos">The destination position</param>
        public static void MoveCard(int id, Zones srcZone, int srcPos, Zones destZone, int destPos)
        {
            // First check that the card is even there
            BaseCard cardToMove = null;
            List<BaseCard> srcZoneContainer = null;
            List<BaseCard> destZoneContainer = null;

            switch (srcZone)
            {
                case Zones.FRIENDLY_HAND:
                    srcZoneContainer = GameState.Player.Hand;
                    cardToMove = GameState.Player.Hand.ElementAtOrDefault(srcPos);
                    break;

                case Zones.FRIENDLY_PLAY:
                    srcZoneContainer = GameState.Board.PlayerZone;
                    cardToMove = GameState.Board.PlayerZone.ElementAtOrDefault(srcPos);
                    break;

                case Zones.OPPOSING_HAND:
                    srcZoneContainer = GameState.Opponent.Hand;
                    cardToMove = GameState.Opponent.Hand.ElementAtOrDefault(srcPos);
                    break;

                case Zones.OPPOSING_PLAY:
                    srcZoneContainer = GameState.Board.OpponentZone;
                    cardToMove = GameState.Board.OpponentZone.ElementAtOrDefault(srcPos);
                    break;

                default:
                    throw new InvalidOperationException(string.Format("This shouldn't happen (unless a new game mechanic was added). Don't be moving things from {0}", srcZone));
            }


            if (cardToMove == null || cardToMove.Id != id)
            {
                throw new InvalidOperationException(string.Format("Could not find {0} at {1}[{2}], found {3} instead!", id, srcZone, srcPos, cardToMove));
            }

            // Now move it
            switch (destZone)
            {
                case Zones.FRIENDLY_PLAY:
                    destZoneContainer = GameState.Board.PlayerZone;
                    break;

                case Zones.FRIENDLY_HAND:
                    destZoneContainer = GameState.Player.Hand;
                    break;

                case Zones.FRIENDLY_GRAVEYARD:
                    destZoneContainer = GameState.Player.Graveyard;
                    break;

                case Zones.OPPOSING_PLAY:
                    destZoneContainer = GameState.Board.OpponentZone;
                    break;

                case Zones.OPPOSING_HAND:
                    destZoneContainer = GameState.Opponent.Hand;
                    break;

                case Zones.OPPOSING_GRAVEYARD:
                    destZoneContainer = GameState.Opponent.Graveyard;
                    break;
            }

            if (srcZoneContainer == null || destZoneContainer == null)
            {
                throw new InvalidOperationException("Yo dawg, you forgot to instantiate something.");
            }

            destZoneContainer[destPos] = cardToMove;
            srcZoneContainer[srcPos] = null;

            Logger.Instance.Debug(string.Format("Moving {0}[1}\tFROM {2}[{3}]\tTO {4}[{5}]", cardToMove, id, srcZone, srcPos, destZone, destPos));
        }
    }
}
