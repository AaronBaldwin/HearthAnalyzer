using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Bloodsail Raider
    /// 
    /// <b>Battlecry:</b> Gain Attack equal to the Attack of your weapon.
    /// </summary>
    public class BloodsailRaider : BaseMinion, IBattlecry
    {
        internal const int MANA_COST = 2;
        internal const int ATTACK_POWER = 2;
        internal const int HEALTH = 3;

        public BloodsailRaider(int id = -1)
        {
            this.Id = id;
            this.Name = "Bloodsail Raider";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.PIRATE;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            if (this.Owner.Weapon != null)
            {
                this.TakeBuff(this.Owner.Weapon.CurrentAttackPower, 0);
            }
        }
    }
}
