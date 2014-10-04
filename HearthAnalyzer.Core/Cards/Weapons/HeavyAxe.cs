using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Heavy Axe Weapon
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HeavyAxe : BaseWeapon
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int DURABILITY = 0;

        public HeavyAxe(int id = -1)
        {
            this.Id = id;
            this.Name = "Heavy Axe";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
