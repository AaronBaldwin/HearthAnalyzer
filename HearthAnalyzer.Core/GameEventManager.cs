using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Responsible for handling the various game events.
    /// 
    /// Classes can register to be called by the GameEventManager when certain events
    /// happen.
    /// </summary>
    /// <remarks>
    /// Call GameEventManager.Initialize() to ensure it receives your events!!
    /// </remarks>
    /// 
    /// Good references:
    ///  - General sequencing of events: http://us.battle.net/hearthstone/en/forum/topic/13681168564
	///  - Order of AOE events: http://www.liquidhearth.com/forum/hearthstone/456767-myth-busted-redemption-revive-minion-randomly
    ///  - Triggered effects: http://hearthstone.gamepedia.com/Triggered_effect
    public static class GameEventManager
    {

        public static void Initialize()
        {
            MinionPlayed += OnMinionPlayed;
            
            _minionPlayedListeners = new List<Tuple<BaseCard, MinionPlayedEventHandler>>();

            MinionAttacking += OnMinionAttacking;
            _minionAttackingListeners = new List<Tuple<BaseCard, MinionAttackingEventHandler>>();
        }

        #region Event Definitions

        /// <summary>
        /// Handler for when a minion is played
        /// </summary>
        /// <param name="minionPlayed">The mininon that was just played</param>
        /// <param name="gameState">The current state of the game</param>
        /// <remarks>
        /// Typically called by Secrets like Mirror Entity
        /// </remarks>
        public delegate void MinionPlayedEventHandler(BaseMinion minionPlayed, GameState gameState);
        public static MinionPlayedEventHandler MinionPlayed;
        private static List<Tuple<BaseCard, MinionPlayedEventHandler>> _minionPlayedListeners;

        /// <summary>
        /// Handler for when a minion is attacking but before damage is dealt
        /// </summary>
        /// <param name="attacker">The attacking minion</param>
        /// <param name="target">The unfortunate target</param>
        /// <param name="gameState">The current state of the game</param>
        /// <param name="shouldAbort">Whether or not this event should abort</param>
        /// <remarks>
        /// Handlers include Misdirection Trap, Vaporize, and the game engine itself.
        /// </remarks>
        public delegate void MinionAttackingEventHandler(BaseMinion attacker, object target, GameState gameState, out bool shouldAbort);
        public static MinionAttackingEventHandler MinionAttacking;
        private static List<Tuple<BaseCard, MinionAttackingEventHandler>> _minionAttackingListeners; 

        #endregion

        #region Listener Registration

        /// <summary>
        /// Register with GameEventManager to get called when a minion is played
        /// </summary>
        /// <param name="self">The instance requesting to be called</param>
        /// <param name="callback">The callback to call</param>
        public static void RegisterForEvent(BaseCard self, MinionPlayedEventHandler callback)
        {
            _minionPlayedListeners.Add(new Tuple<BaseCard, MinionPlayedEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, MinionAttackingEventHandler callback)
        {
            _minionAttackingListeners.Add(new Tuple<BaseCard, MinionAttackingEventHandler>(self, callback));
        }

        /// <summary>
        /// Unregister from all events
        /// </summary>
        /// <param name="self">The instance to be unregistered</param>
        public static void UnregisterForEvents(BaseCard self)
        {
            _minionPlayedListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _minionAttackingListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
        }

        #endregion

        #region Handlers

        public static void OnMinionPlayed(BaseMinion minionPlayed, GameState gameState)
        {
            // If a tree falls in a forest without any one listening...
            if (!_minionPlayedListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _minionPlayedListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(minionPlayed, gameState);
            }
        }

        public static void OnMinionAttacking(BaseMinion attacker, object target, GameState gameState, out bool shouldAbort)
        {
            shouldAbort = false;

            if (_minionAttackingListeners.Any())
            {
                // Secrets get called first, then the game engine
                // TODO: If a card is hit with
                var sortedListeners = _minionAttackingListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
                foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
                {
                    handler(attacker, target, gameState, out shouldAbort);
                }
            }

            if (!shouldAbort)
            {
                // TODO: Fire damage event
            }
        }

        #endregion
    }
}
