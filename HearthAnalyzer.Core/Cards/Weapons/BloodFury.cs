using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Blood Fury Weapon
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class BloodFury : BaseWeapon
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int DURABILITY = 8;

        public BloodFury(int id = -1)
        {
            this.Id = id;
            this.Name = "Blood Fury";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
