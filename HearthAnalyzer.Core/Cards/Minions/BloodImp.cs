using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Blood Imp
    /// 
    /// <b>Stealth</b>. At the end of your turn, give another random friendly minion +1 Health.
    /// </summary>
    public class BloodImp : BaseMinion, ITriggeredEffectOwner
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 1;
        private const int EFFECT_POWER = 1;

        public BloodImp(int id = -1)
        {
            this.Id = id;
            this.Name = "Blood Imp";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;

            this.ApplyStatusEffects(MinionStatusEffects.STEALTHED);
        }

        public void RegisterEffect()
        {
            GameEventManager.RegisterForEvent(this, (GameEventManager.TurnEndEventHandler)this.OnTurnEnd);
        }

        private void OnTurnEnd(BasePlayer player)
        {
            var minions = GameEngine.GameState.CurrentPlayerPlayZone.Where(card => card != null && card != this).ToList();
            if (minions.Count > 0)
            {
                var randomMinion = minions[GameEngine.Random.Next(minions.Count)];
                ((BaseMinion)randomMinion).TakeBuff(0, EFFECT_POWER);
            }
        }
    }
}
