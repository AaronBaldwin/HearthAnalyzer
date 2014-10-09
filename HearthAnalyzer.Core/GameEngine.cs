using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using HearthAnalyzer.Core.Cards;
using HearthAnalyzer.Core.Cards.Spells;
using HearthAnalyzer.Core.Deathrattles;

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
        /// Represents the result of a game
        /// </summary>
        public enum GameResult
        {
            WIN,
            LOSE,
            DRAW
        }

        internal static bool PlayerMulliganed = false;
        internal static bool OpponentMulliganed = false;

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
            Deathrattles = new Dictionary<BaseCard, BaseDeathrattle>();
            GameState = new GameState(player, opponent, board, turnNumber, currentPlayer);
            DeadCardsThisTurn = new List<BaseCard>();
            DeadPlayersThisTurn = new List<BasePlayer>();
        }

        /// <summary>
        /// Uninitializes the game engine
        /// </summary>
        public static void Uninitialize()
        {
            HearthEntityFactory.Reset();
            GameEventManager.Uninitialize();
            GameState = null;
            DeadCardsThisTurn = null;
            DeadPlayersThisTurn = null;
            Deathrattles = null;
            PlayerMulliganed = false;
            OpponentMulliganed = false;
        }

        /// <summary>
        /// The current state of the game
        /// </summary>
        public static GameState GameState { get; private set; }

        /// <summary>
        /// The list of dead minions this turn
        /// </summary>
        public static List<BaseCard> DeadCardsThisTurn { get; private set; }

        /// <summary>
        /// The list of dead players this turn
        /// </summary>
        public static List<BasePlayer> DeadPlayersThisTurn { get; private set; }

        /// <summary>
        /// The list of deathrattles active on the board
        /// </summary>
        public static Dictionary<BaseCard, BaseDeathrattle> Deathrattles { get; private set; } 

        /// <summary>
        /// The GameEngine's random number generator
        /// </summary>
        internal static Random Random { get; private set; }

        /// <summary>
        /// Handler for when a game is ended
        /// </summary>
        /// <param name="result">The result of the game</param>
        public delegate void GameEndedEventHandler(GameResult result);

        /// <summary>
        /// The event that gets fired when the game is over
        /// </summary>
        public static GameEndedEventHandler GameEnded;

        /// <summary>
        /// Apply damage from the attacker to the target
        /// </summary>
        /// <param name="attacker">The card doing the attacking</param>
        /// <param name="target">The object receiving the attack</param>
        /// <param name="isRetaliation">Whether or not the attack is a retaliation</param>
        public static void ApplyAttackDamage(IAttacker attacker, IDamageableEntity target, bool isRetaliation = false)
        {
            // If the attacker is a spell card or hero power, you can't retaliate
            // If the target is a hero, he can't retaliate
            if (target is BaseMinion)
            {
                var targetMinion = (BaseMinion) target;

                targetMinion.TakeDamage(attacker.GetCurrentAttackPower());

                if (!isRetaliation && (attacker is BaseMinion || attacker is BaseWeapon))
                {
                    if (attacker is BaseWeapon)
                    {
                        var weapon = (BaseWeapon) attacker;
                        ApplyAttackDamage(targetMinion, weapon.WeaponOwner, isRetaliation: true);
                    }
                    else
                    {
                        // Then we must be attacking a minion
                        ApplyAttackDamage(targetMinion, (IDamageableEntity)attacker, isRetaliation: true);
                    }
                }
            }
            else if (target is BasePlayer)
            {
                var targetPlayer = (BasePlayer) target;

                targetPlayer.TakeDamage(attacker.GetCurrentAttackPower());
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
                    srcZoneContainer = GameState.Board.PlayerPlayZone;
                    cardToMove = GameState.Board.PlayerPlayZone.ElementAtOrDefault(srcPos);
                    break;

                case Zones.OPPOSING_HAND:
                    srcZoneContainer = GameState.Opponent.Hand;
                    cardToMove = GameState.Opponent.Hand.ElementAtOrDefault(srcPos);
                    break;

                case Zones.OPPOSING_PLAY:
                    srcZoneContainer = GameState.Board.OpponentPlayZone;
                    cardToMove = GameState.Board.OpponentPlayZone.ElementAtOrDefault(srcPos);
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
                    destZoneContainer = GameState.Board.PlayerPlayZone;
                    break;

                case Zones.FRIENDLY_HAND:
                    destZoneContainer = GameState.Player.Hand;
                    break;

                case Zones.FRIENDLY_GRAVEYARD:
                    destZoneContainer = GameState.Player.Graveyard;
                    break;

                case Zones.OPPOSING_PLAY:
                    destZoneContainer = GameState.Board.OpponentPlayZone;
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

            Logger.Instance.DebugFormat("Moving {0}\tFROM {1}[{2}]\tTO {3}[{4}]", cardToMove, srcZone, srcPos, destZone, destPos);
        }

        /// <summary>
        /// Register a baseDeathrattle with the game engine. If the source card dies, it will trigger.
        /// </summary>
        /// <param name="source">The triggering card</param>
        /// <param name="baseDeathrattle">The baseDeathrattle to perform</param>
        public static void RegisterDeathrattle(BaseCard source, BaseDeathrattle baseDeathrattle)
        {
            GameEngine.Deathrattles[source] = baseDeathrattle;
        }

        /// <summary>
        /// Removes all deathrattles registered by the source card.
        /// </summary>
        /// <param name="source">The triggering card</param>
        public static void UnregisterDeathrattle(BaseMinion source)
        {
            GameEngine.Deathrattles.Remove(source);
        }

        /// <summary>
        /// Triggers death rattles after damage has been calculated.
        /// </summary>
        public static void TriggerDeathrattles()
        {
            // Before we fire trigger any deathrattles, we need to check if the game is over
            if (GameEngine.CheckForGameEnd()) return;

            // Remove dead minions from the board so the deathrattle doesn't trigger on itself
            GameEngine.DeadCardsThisTurn.ForEach(card => GameEngine.GameState.Board.RemoveCard(card));

            // Deathrattles trigger by TimePlayed first.
            var sortedDeadCards = GameEngine.DeadCardsThisTurn.OrderBy(card => card.TimePlayed).ToList();

            bool deathrattleTriggered = false;
            foreach (var card in sortedDeadCards)
            {
                if (GameEngine.Deathrattles.ContainsKey(card))
                {
                    GameEngine.Deathrattles[card].Deathrattle();

                    deathrattleTriggered = true;

                    GameEngine.Deathrattles.Remove(card);
                }
            }

            // We may have actually triggered yet more deathrattles with this so we need to check for it
            if (deathrattleTriggered)
            {
                GameEngine.TriggerDeathrattles();
            }
        }

        /// <summary>
        /// Shuffles and deals the pre-mulligan hands
        /// </summary>
        public static void DealPreMulligan()
        {
            // First, shuffle each player's deck
            GameState.Player.Deck.Shuffle();
            GameState.Opponent.Deck.Shuffle();

            // Deal cards to the players and give them a chance to mulligan
            // First player gets 3 cards, second player gets 4 cards
            GameState.CurrentPlayer.Hand.AddRange(GameState.CurrentPlayer.Deck.DrawCards(3));
            GameState.WaitingPlayer.Hand.AddRange(GameState.WaitingPlayer.Deck.DrawCards(4));
        }

        /// <summary>
        /// Performs a mulligan for the specified player
        /// </summary>
        /// <param name="player">The player performing the mulligan</param>
        /// <param name="mulligans">The cards to toss if any</param>
        public static void Mulligan(BasePlayer player, IEnumerable<BaseCard> mulligans)
        {
            if (mulligans != null && mulligans.Any())
            {
                var mulligansList = mulligans.ToList();
                foreach (var card in mulligansList)
                {
                    if (!player.Hand.Contains(card))
                    {
                        throw new InvalidOperationException(
                            string.Format("Can't mulligan {0} because it could not be found it the player's hand!", card));
                    }

                    Logger.Instance.InfoFormat("{0} is mulliganing {1}", player.LogString(), card);
                    player.Hand.Remove(card);
                }

                // Draw new cards equal to the amount mulliganed
                player.DrawCards(mulligansList.Count());

                // Shuffle the mulliganed cards back into the deck
                player.Deck.AddCards(mulligansList);
                player.Deck.Shuffle();
            }

            if (player == GameEngine.GameState.Player)
            {
                PlayerMulliganed = true;
            }
            else
            {
                OpponentMulliganed = true;
            }

            if (PlayerMulliganed && OpponentMulliganed)
            {
                var theCoin = HearthEntityFactory.CreateCard<TheCoin>();
                Logger.Instance.InfoFormat("Giving {0} {1}", GameEngine.GameState.WaitingPlayer.LogString(), theCoin);
                GameEngine.GameState.WaitingPlayer.AddCardToHand(theCoin);
                GameEngine.StartTurn(GameEngine.GameState.CurrentPlayer);
            }
        }

        /// <summary>
        /// Starts a turn for the specified player
        /// </summary>
        /// <param name="player">The player who is starting their turn</param>
        public static void StartTurn(BasePlayer player)
        {
            GameEngine.GameState.CurrentPlayer = player;
            GameEngine.GameState.TurnNumber++;

            var currentPlayer = GameEngine.GameState.CurrentPlayer;

            // Increment their max mana
            currentPlayer.AddManaCrystal();

            // Move any pending overload to active
            currentPlayer.Overload = currentPlayer.PendingOverload;
            currentPlayer.PendingOverload = 0;

            // Set the mana to max - overload
            currentPlayer.Mana = currentPlayer.MaxMana - currentPlayer.Overload;

            GameEventManager.TurnStart(player);

            // Draw a card
            currentPlayer.DrawCard();
        }

        /// <summary>
        /// Ends the turn for the current player
        /// </summary>
        public static void EndTurn()
        {
            var currentPlayer = GameEngine.GameState.CurrentPlayer;

            // Fire turn end event
            GameEventManager.TurnEnd(currentPlayer);

            // Clear any overloads
            currentPlayer.Overload = 0;

            // Clear GameEngine graveyards
            DeadCardsThisTurn.Clear();

            // Unexhaust all player owned minions and remove temporary boons
            foreach (var minion in GameEngine.GameState.CurrentPlayerPlayZone)
            {
                if (minion != null)
                {
                    ((BaseMinion)minion).RemoveStatusEffects(MinionStatusEffects.EXHAUSTED);
                    ((BaseMinion)minion).ResetAttacksThisTurn();
                    minion.TemporaryAttackBuff = 0;
                }
            }

            foreach (var minion in GameEngine.GameState.WaitingPlayerPlayZone)
            {
                if (minion != null)
                {
                    minion.TemporaryAttackBuff = 0;
                }
            }

            GameEngine.GameState.CurrentPlayer.TemporaryAttackBuff = 0;
            GameEngine.GameState.WaitingPlayer.TemporaryAttackBuff = 0;

            // Unexhaust players
            GameEngine.GameState.CurrentPlayer.ResetAttacksThisTurn();
            GameEngine.GameState.CurrentPlayer.RemoveStatusEffects(PlayerStatusEffects.EXHAUSTED);

            // Start the turn for the next player
            GameEngine.StartTurn(GameEngine.GameState.WaitingPlayer);
        }

        /// <summary>
        /// Checks to see if the game has ended
        /// </summary>
        /// <returns>Whether or not the game is over</returns>
        public static bool CheckForGameEnd()
        {
            if (!DeadPlayersThisTurn.Any()) return false;

            var subscribers = GameEnded.GetInvocationList().ToList();

            // Asynchrnously inform the subscribers that the game has ended so we can start stabilizing the game state
            if (DeadPlayersThisTurn.Count == 2)
            {
                // Draw!
                subscribers.ForEach(handler => ((GameEndedEventHandler) handler).BeginInvoke(GameResult.DRAW, null, null));
            }
            else if (DeadPlayersThisTurn.First() == GameState.Player)
            {
                // Lose!
                subscribers.ForEach(handler => ((GameEndedEventHandler)handler).BeginInvoke(GameResult.LOSE, null, null));
            }
            else
            {
                // Win!
                subscribers.ForEach(handler => ((GameEndedEventHandler)handler).BeginInvoke(GameResult.WIN, null, null));
            }

            return true;
        }
    }
}
