using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Spells;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Archmage Antonidas
    /// 
    /// Whenever you cast a spell, add a 'Fireball' spell to your hand.
    /// </summary>
    public class ArchmageAntonidas : BaseMinion, ITriggeredEffectOwner
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 7;

        public ArchmageAntonidas(int id = -1)
        {
            this.Id = id;
            this.Name = "Archmage Antonidas";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void RegisterEffect()
        {
            GameEventManager.RegisterForEvent(this, (GameEventManager.SpellCastingEventHandler)this.OnSpellCasting);
        }

        private void OnSpellCasting(BaseSpell spell, IDamageableEntity target, out bool shouldAbort)
        {
            var fireball = HearthEntityFactory.CreateCard<Fireball>();
            this.Owner.AddCardToHand(fireball);

            shouldAbort = false;
        }
    }
}
