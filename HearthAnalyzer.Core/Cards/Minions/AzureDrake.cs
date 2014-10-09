using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Azure Drake
    /// 
    /// <b>Spell Damage +1</b>. <b>Battlecry:</b> Draw a card.
    /// </summary>
    public class AzureDrake : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 4;

        public AzureDrake(int id = -1)
        {
            this.Id = id;
            this.Name = "Azure Drake";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DRAGON;

            this.BonusSpellPower = 1;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            this.Owner.DrawCard();
        }
    }
}
