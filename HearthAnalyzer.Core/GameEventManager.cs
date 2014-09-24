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
    ///  - Deathrattle: http://imgur.com/a/jJhb2
    ///  - Events: https://github.com/danielyule/hearthbreaker/blob/master/events.md
    ///  - Ordering: https://github.com/danielyule/hearthbreaker/issues/42
    public static class GameEventManager
    {

        public static void Initialize()
        {
            Attacking += OnAttacking;
            _minionAttackingListeners = new List<Tuple<BaseCard, AttackingEventHandler>>();

            DamageDealt += OnDamageDealt;
            _damageDealtListeners = new List<Tuple<BaseCard, DamageDealtEventHandler>>();

            MinionPlaced += OnMinionPlaced;
            _minionPlacedListeners = new List<Tuple<BaseCard, MinionPlacedEventHandler>>();

            MinionPlayed += OnMinionPlayed;
            _minionPlayedListeners = new List<Tuple<BaseCard, MinionPlayedEventHandler>>();
        }

        public static void Uninitialize()
        {
            Attacking -= OnAttacking;
            DamageDealt -= OnDamageDealt;
            MinionPlaced -= OnMinionPlaced;
            MinionPlayed -= OnMinionPlayed;
        }

        #region Event Definitions

        /// <summary>
        /// Handler for when a card is attacking but before damage is dealt
        /// </summary>
        /// <param name="attacker">The attacking card</param>
        /// <param name="target">The unfortunate target</param>
        /// <param name="shouldAbort">Whether or not this event should abort</param>
        /// <remarks>
        /// Handlers include Misdirection Trap, Vaporize, and the game engine itself.
        /// </remarks>
        public delegate void AttackingEventHandler(BaseCard attacker, IDamageableEntity target, bool isRetaliation, out bool shouldAbort);
        public static AttackingEventHandler Attacking;
        private static List<Tuple<BaseCard, AttackingEventHandler>> _minionAttackingListeners;

        /// <summary>
        /// Handler for when damage is dealt
        /// </summary>
        /// <param name="target">The target of the damage</param>
        /// <param name="damageDealt">How much damage was dealt</param>
        public delegate void DamageDealtEventHandler(IDamageableEntity target, int damageDealt);
        public static DamageDealtEventHandler DamageDealt;
        private static List<Tuple<BaseCard, DamageDealtEventHandler>> _damageDealtListeners;

        /// <summary>
        /// Handler for when a minion is placed (before his battle cry)
        /// </summary>
        /// <param name="minion">The minion that was just placed</param>
        public delegate void MinionPlacedEventHandler(BaseMinion minion);
        public static MinionPlacedEventHandler MinionPlaced;
        private static List<Tuple<BaseCard, MinionPlacedEventHandler>> _minionPlacedListeners;

        /// <summary>
        /// Handler for when a minion is played
        /// </summary>
        /// <param name="minionPlayed">The mininon that was just played</param>
        /// <remarks>
        /// Typically called by Secrets like Mirror Entity
        /// </remarks>
        public delegate void MinionPlayedEventHandler(BaseMinion minionPlayed);
        public static MinionPlayedEventHandler MinionPlayed;
        private static List<Tuple<BaseCard, MinionPlayedEventHandler>> _minionPlayedListeners;


        #endregion

        #region Listener Registration

        /// <summary>
        /// Register with GameEventManager to get called when an attack happens
        /// </summary>
        /// <param name="self">The instance requesting to be called</param>
        /// <param name="callback">The callback to call</param>
        public static void RegisterForEvent(BaseCard self, AttackingEventHandler callback)
        {
            _minionAttackingListeners.Add(new Tuple<BaseCard, AttackingEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, DamageDealtEventHandler callback)
        {
            _damageDealtListeners.Add(new Tuple<BaseCard, DamageDealtEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, MinionPlacedEventHandler callback)
        {
            _minionPlacedListeners.Add(new Tuple<BaseCard, MinionPlacedEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, MinionPlayedEventHandler callback)
        {
            _minionPlayedListeners.Add(new Tuple<BaseCard, MinionPlayedEventHandler>(self, callback));
        }

        /// <summary>
        /// Unregister from all events
        /// </summary>
        /// <param name="self">The instance to be unregistered</param>
        public static void UnregisterForEvents(BaseCard self)
        {
            _minionAttackingListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _damageDealtListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _minionPlacedListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _minionPlayedListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
        }

        #endregion

        #region Handlers

        public static void OnAttacking(BaseCard attacker, IDamageableEntity target, bool isRetaliation, out bool shouldAbort)
        {
            shouldAbort = false;

            if (_minionAttackingListeners.Any())
            {
                // Triggered Effects get called first, then Secrets, then the game engine
                var sortedListeners = _minionAttackingListeners.Where(kvp => kvp.Item1.Type != CardType.ACTIVE_SECRET).OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
                var secrets = _minionAttackingListeners.Where(kvp => kvp.Item1.Type == CardType.ACTIVE_SECRET).OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
                sortedListeners.AddRange(secrets);

                foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
                {
                    handler(attacker, target, isRetaliation, out shouldAbort);
                }
            }

            if (!shouldAbort)
            {
                GameEngine.ApplyAttackDamage(attacker, target, isRetaliation);

                // Time to trigger deathrattles
                GameEngine.TriggerDeathrattles();
            }
        }

        public static void OnDamageDealt(IDamageableEntity target, int damageDealt)
        {
            if (!_damageDealtListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _damageDealtListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(target, damageDealt);
            }
        }

        public static void OnMinionPlaced(BaseMinion minionPlaced)
        {
            if (!_minionPlacedListeners.Any()) return;

            // Listeners for this are called in the order in which they are played on the baord
            var sortedListeners = _minionPlacedListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(minionPlaced);
            }
        }

        public static void OnMinionPlayed(BaseMinion minionPlayed)
        {
            // If a tree falls in a forest without any one listening...
            if (!_minionPlayedListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _minionPlayedListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(minionPlayed);
            }
        }

        #endregion
    }
}
