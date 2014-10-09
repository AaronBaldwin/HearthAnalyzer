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

            Healing += OnHealing;
            _healingListeners = new List<Tuple<BaseCard, HealingEventHandler>>();

            HealingDealt += OnHealingDealt;
            _healingDealtListeners = new List<Tuple<BaseCard, HealingDealtEventHandler>>();

            MinionPlaced += OnMinionPlaced;
            _minionPlacedListeners = new List<Tuple<BaseCard, MinionPlacedEventHandler>>();

            MinionPlayed += OnMinionPlayed;
            _minionPlayedListeners = new List<Tuple<BaseCard, MinionPlayedEventHandler>>();

            SpellCasting += OnSpellCasting;
            _spellCastingListeners = new List<Tuple<BaseCard, SpellCastingEventHandler>>();

            TurnEnd += OnTurnEnd;
            _turnEndListeners = new List<Tuple<BaseCard, TurnEndEventHandler>>();

            TurnStart += OnTurnStart;
            _turnStartListeners = new List<Tuple<BaseCard, TurnStartEventHandler>>();
        }

        public static void Uninitialize()
        {
            Attacking -= OnAttacking;
            DamageDealt -= OnDamageDealt;
            Healing -= OnHealing;
            HealingDealt -= OnHealingDealt;
            MinionPlaced -= OnMinionPlaced;
            MinionPlayed -= OnMinionPlayed;
            SpellCasting -= OnSpellCasting;
            TurnEnd -= OnTurnEnd;
            TurnStart -= OnTurnStart;
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
        public delegate void AttackingEventHandler(IAttacker attacker, IDamageableEntity target, bool isRetaliation, out bool shouldAbort);
        public static AttackingEventHandler Attacking;
        internal static List<Tuple<BaseCard, AttackingEventHandler>> _minionAttackingListeners;

        /// <summary>
        /// Handler for when damage is dealt
        /// </summary>
        /// <param name="target">The target of the damage</param>
        /// <param name="damageDealt">How much damage was dealt</param>
        public delegate void DamageDealtEventHandler(IDamageableEntity target, int damageDealt);
        public static DamageDealtEventHandler DamageDealt;
        internal static List<Tuple<BaseCard, DamageDealtEventHandler>> _damageDealtListeners;

        /// <summary>
        /// Handler for when healing is happening (but target has not been healed yet)
        /// </summary>
        /// <param name="healer">The player doing the healing</param>
        /// <param name="target">The target of the heal</param>
        /// <param name="healAmount">The amount to heal for</param>
        /// <param name="shouldAbort">Whether or not it should be aborted</param>
        public delegate void HealingEventHandler(BasePlayer healer, IDamageableEntity target, int healAmount, out bool shouldAbort);
        public static HealingEventHandler Healing;
        internal static List<Tuple<BaseCard, HealingEventHandler>> _healingListeners;

        /// <summary>
        /// Handler for when healing has been dealt
        /// </summary>
        /// <param name="target">The target of the heal</param>
        /// <param name="healAmount">The amount of heals dealt</param>
        public delegate void HealingDealtEventHandler(IDamageableEntity target, int healAmount);
        public static HealingDealtEventHandler HealingDealt;
        internal static List<Tuple<BaseCard, HealingDealtEventHandler>> _healingDealtListeners; 

        /// <summary>
        /// Handler for when a minion is placed (before his battle cry)
        /// </summary>
        /// <param name="minion">The minion that was just placed</param>
        public delegate void MinionPlacedEventHandler(BaseMinion minion);
        public static MinionPlacedEventHandler MinionPlaced;
        internal static List<Tuple<BaseCard, MinionPlacedEventHandler>> _minionPlacedListeners;

        /// <summary>
        /// Handler for when a minion is played
        /// </summary>
        /// <param name="minionPlayed">The mininon that was just played</param>
        /// <remarks>
        /// Typically called by Secrets like Mirror Entity
        /// </remarks>
        public delegate void MinionPlayedEventHandler(BaseMinion minionPlayed);
        public static MinionPlayedEventHandler MinionPlayed;
        internal static List<Tuple<BaseCard, MinionPlayedEventHandler>> _minionPlayedListeners;

        /// <summary>
        /// Handler for when a spell is casting (but hasn't triggered its effect yet)
        /// </summary>
        /// <param name="spell">The spell being cast</param>
        /// <param name="target">The target of the spell if applicable</param>
        /// <param name="shouldAbort">Whether or not the spell should abort casting</param>
        public delegate void SpellCastingEventHandler(BaseSpell spell, IDamageableEntity target, out bool shouldAbort);
        public static SpellCastingEventHandler SpellCasting;
        internal static List<Tuple<BaseCard, SpellCastingEventHandler>> _spellCastingListeners;

        /// <summary>
        /// Handler for when a turn ends
        /// </summary>
        /// <param name="player">The player whose turn just ended</param>
        public delegate void TurnEndEventHandler(BasePlayer player);
        public static TurnEndEventHandler TurnEnd;
        internal static List<Tuple<BaseCard, TurnEndEventHandler>> _turnEndListeners; 

        /// <summary>
        /// Handler for when a turn starts (before card draw)
        /// </summary>
        /// <param name="player">The player who is starting their turn</param>
        public delegate void TurnStartEventHandler(BasePlayer player);
        public static TurnStartEventHandler TurnStart;
        internal static List<Tuple<BaseCard, TurnStartEventHandler>> _turnStartListeners; 

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

        public static void RegisterForEvent(BaseCard self, HealingEventHandler callback)
        {
            _healingListeners.Add(new Tuple<BaseCard, HealingEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, HealingDealtEventHandler callback)
        {
            _healingDealtListeners.Add(new Tuple<BaseCard, HealingDealtEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, MinionPlacedEventHandler callback)
        {
            _minionPlacedListeners.Add(new Tuple<BaseCard, MinionPlacedEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, MinionPlayedEventHandler callback)
        {
            _minionPlayedListeners.Add(new Tuple<BaseCard, MinionPlayedEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, SpellCastingEventHandler callback)
        {
            _spellCastingListeners.Add(new Tuple<BaseCard, SpellCastingEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, TurnEndEventHandler callback)
        {
            _turnEndListeners.Add(new Tuple<BaseCard, TurnEndEventHandler>(self, callback));
        }

        public static void RegisterForEvent(BaseCard self, TurnStartEventHandler callback)
        {
            _turnStartListeners.Add(new Tuple<BaseCard, TurnStartEventHandler>(self, callback));
        }

        /// <summary>
        /// Unregister from all events
        /// </summary>
        /// <param name="self">The instance to be unregistered</param>
        public static void UnregisterForEvents(BaseCard self)
        {
            _minionAttackingListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _damageDealtListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _healingListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _healingDealtListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _minionPlacedListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _minionPlayedListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _spellCastingListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _turnEndListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
            _turnStartListeners.RemoveAll(kvp => kvp.Item1.Id == self.Id);
        }

        #endregion

        #region Handlers

        public static void OnAttacking(IAttacker attacker, IDamageableEntity target, bool isRetaliation, out bool shouldAbort)
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
                // If it was a weapon attacking, wait until after the weapon has taken durability hit
                if (!(attacker is BaseWeapon))
                {
                    GameEngine.TriggerDeathrattles();
                }
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

        public static void OnHealing(BasePlayer healer, IDamageableEntity target, int healAmount, out bool shouldAbort)
        {
            shouldAbort = false;
            if (!_healingListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _healingListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(healer, target, healAmount, out shouldAbort);
            }
        }

        public static void OnHealingDealt(IDamageableEntity target, int healAmount)
        {
            if (!_healingDealtListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _healingDealtListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(target, healAmount);
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

        public static void OnSpellCasting(BaseSpell spell, IDamageableEntity target, out bool shouldAbort)
        {
            shouldAbort = false;

            if (!_spellCastingListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _spellCastingListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(spell, target, out shouldAbort);
            }
        }

        public static void OnTurnEnd(BasePlayer player)
        {
            if (!_turnEndListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _turnEndListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(player);
            }
        }

        public static void OnTurnStart(BasePlayer player)
        {
            if (!_turnStartListeners.Any()) return;

            // Listeners for this are called in the order in which they were played on the board
            var sortedListeners = _turnStartListeners.OrderBy(kvp => kvp.Item1.TimePlayed).ToList();
            foreach (var handler in sortedListeners.Select(kvp => kvp.Item2))
            {
                handler(player);
            }
        }

        #endregion
    }
}
