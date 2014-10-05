using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Deathrattles;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Alarm-o-Bot
    /// 
    /// At the start of your turn, swap this minion with a random one in your hand.
    /// </summary>
    public class AlarmoBot : BaseMinion, ITriggeredEffectOwner
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 3;

        public AlarmoBot(int id = -1)
        {
            this.Id = id;
            this.Name = "Alarm-o-Bot";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void RegisterEffect()
        {
            GameEventManager.RegisterForEvent(this, (GameEventManager.TurnStartEventHandler)this.OnTurnStart);
        }

        internal void OnTurnStart(BasePlayer player)
        {
            if (player == this.Owner)
            {
                var minionsInHand = this.Owner.Hand.Where(card => card is BaseMinion).ToList();
                int randomMinionIndex = GameEngine.Random.Next(minionsInHand.Count());
                var randomMinion = minionsInHand[randomMinionIndex] as BaseMinion;

                GameEngine.GameState.Board.RemoveCard(this);
                player.Hand.Add(this);
                GameEventManager.UnregisterForEvents(this);

                var firstEmptySlot = GameEngine.GameState.CurrentPlayerPlayZone.FindIndex(card => card == null);
                player.PlayCard(randomMinion, null, firstEmptySlot, forceSummoned: true);
            }
        }
    }
}
