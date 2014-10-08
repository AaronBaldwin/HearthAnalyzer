using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Weapons;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Arathi Weaponsmith
    /// 
    /// <b>Battlecry:</b> Equip a 2/2 weapon.
    /// </summary>
    public class ArathiWeaponsmith : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public ArathiWeaponsmith(int id = -1)
        {
            this.Id = id;
            this.Name = "Arathi Weaponsmith";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            var battleAxe = HearthEntityFactory.CreateCard<BattleAxe>();

            // kill the old weapon
            if (this.Owner.Weapon != null)
            {
                this.Owner.Weapon.Die();
            }

            this.Owner.Weapon = battleAxe;
            battleAxe.Owner = this.Owner;
            battleAxe.WeaponOwner = this.Owner;
        }
    }
}
