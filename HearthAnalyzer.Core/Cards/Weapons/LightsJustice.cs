using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Light's Justice Weapon
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class LightsJustice : BaseWeapon
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int DURABILITY = 0;

        public LightsJustice(int id = -1)
        {
            this.Id = id;
            this.Name = "Light's Justice";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
