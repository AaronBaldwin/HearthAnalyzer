using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Eaglehorn Bow Weapon
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class EaglehornBow : BaseWeapon
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int DURABILITY = 0;

        public EaglehornBow(int id = -1)
        {
            this.Id = id;
            this.Name = "Eaglehorn Bow";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
