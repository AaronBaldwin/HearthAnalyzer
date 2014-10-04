using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Stormforged Axe Weapon
    /// </summary>
    public class StormforgedAxe : BaseWeapon, IBattlecry
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 2;
        private const int DURABILITY = 0;
        private const int OVERLOAD_COST = 1;

        public StormforgedAxe(int id = -1)
        {
            this.Id = id;
            this.Name = "Stormforged Axe";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }

        #region IBattlecry

        public void Battlecry(IDamageableEntity subTarget)
        {
            this.WeaponOwner.PendingOverload += OVERLOAD_COST;
        }

        #endregion
    }
}
