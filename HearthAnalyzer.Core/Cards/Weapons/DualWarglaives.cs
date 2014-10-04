using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Dual Warglaives Weapon
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class DualWarglaives : BaseWeapon
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 4;
        private const int DURABILITY = 0;

        public DualWarglaives(int id = -1)
        {
            this.Id = id;
            this.Name = "Dual Warglaives";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
