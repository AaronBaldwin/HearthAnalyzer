using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Fiery War Axe Weapon
    /// </summary>
    public class FieryWarAxe : BaseWeapon
    {
        internal const int MANA_COST = 2;
        internal const int ATTACK_POWER = 3;
        internal const int DURABILITY = 2;

        public FieryWarAxe(int id = -1)
        {
            this.Id = id;
            this.Name = "Fiery War Axe";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
