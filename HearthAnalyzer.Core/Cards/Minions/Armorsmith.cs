using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Armorsmith
    /// 
    /// Whenever a friendly minion takes damage, gain 1 Armor.
    /// </summary>
    public class Armorsmith : BaseMinion, ITriggeredEffectOwner
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 4;

        public Armorsmith(int id = -1)
        {
            this.Id = id;
            this.Name = "Armorsmith";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void RegisterEffect()
        {
            GameEventManager.RegisterForEvent(this, (GameEventManager.DamageDealtEventHandler)this.OnDamageDealt);
        }

        private void OnDamageDealt(IDamageableEntity target, int damageDealt)
        {
            var targetMinion = target as BaseMinion;
            if (targetMinion != null && GameEngine.GameState.CurrentPlayerPlayZone.Contains(targetMinion))
            {
                this.Owner.Armor++;
            }
        }
    }
}
