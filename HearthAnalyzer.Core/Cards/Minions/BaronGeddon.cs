using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Baron Geddon
    /// 
    /// At the end of your turn, deal 2 damage to ALL other characters.
    /// </summary>
    public class BaronGeddon : BaseMinion, ITriggeredEffectOwner
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 7;
        private const int HEALTH = 5;
        private const int EFFECT_POWER = 2;

        public BaronGeddon(int id = -1)
        {
            this.Id = id;
            this.Name = "Baron Geddon";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void RegisterEffect()
        {
            GameEventManager.RegisterForEvent(this, (GameEventManager.TurnEndEventHandler)this.OnTurnEnd);
        }

        private void OnTurnEnd(BasePlayer player)
        {
            GameEngine.GameState.WaitingPlayer.TakeDamage(EFFECT_POWER);
            GameEngine.GameState.WaitingPlayerPlayZone.Where(card => card != null)
                .ToList()
                .ForEach(card => ((BaseMinion) card).TakeDamage(EFFECT_POWER));

            GameEngine.GameState.CurrentPlayer.TakeDamage(EFFECT_POWER);
            GameEngine.GameState.CurrentPlayerPlayZone.Where(card => card != null && card != this)
                .ToList()
                .ForEach(card => ((BaseMinion) card).TakeDamage(EFFECT_POWER));
        }
    }
}
