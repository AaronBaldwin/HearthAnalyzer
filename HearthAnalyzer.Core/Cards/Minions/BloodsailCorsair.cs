using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Weapons;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Bloodsail Corsair
    /// 
    /// <b>Battlecry:</b> Remove 1 Durability from your opponent's weapon.
    /// </summary>
    public class BloodsailCorsair : BaseMinion, IBattlecry
    {
        internal const int MANA_COST = 1;
        internal const int ATTACK_POWER = 1;
        internal const int HEALTH = 2;
        internal const int BATTLECRY_DAMAGE = 1;

        public BloodsailCorsair(int id = -1)
        {
            this.Id = id;
            this.Name = "Bloodsail Corsair";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.PIRATE;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            var opponent = GameEngine.GameState.WaitingPlayer;
            if (opponent.Weapon != null)
            {
                if (opponent.Weapon is Gorehowl)
                {
                    // HACK: this card can bypass gorehowl's override of TakeDamage
                    ((Gorehowl)opponent.Weapon).TakeDamage(BATTLECRY_DAMAGE, forceUseBaseImplementation: true);
                }
                else
                {
                    opponent.Weapon.TakeDamage(BATTLECRY_DAMAGE);
                }
            }
        }
    }
}
