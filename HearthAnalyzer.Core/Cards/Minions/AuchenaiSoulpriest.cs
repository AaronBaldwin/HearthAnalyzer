using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Auchenai Soulpriest
    /// 
    /// Your cards and powers that restore Health now deal damage instead.
    /// </summary>
    /// <remarks>
    /// Technically, this card isn't implemented in Hearthstone as a triggered effect card but for our purposes,
    /// it's simpler to do it this way and it should be functionally equivalent.
    /// </remarks>
    public class AuchenaiSoulpriest : BaseMinion, ITriggeredEffectOwner
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 5;

        public AuchenaiSoulpriest(int id = -1)
        {
            this.Id = id;
            this.Name = "Auchenai Soulpriest";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void RegisterEffect()
        {
            GameEventManager.RegisterForEvent(this, (GameEventManager.HealingEventHandler)this.OnHealing);
        }

        private void OnHealing(BasePlayer healer, IDamageableEntity target, int healAmount, out bool shouldAbort)
        {
            shouldAbort = false;

            if (healer == this.Owner)
            {
                shouldAbort = true;
                // Instead, do damage!
                target.TakeDamage(healAmount);
            }
        }
    }
}
