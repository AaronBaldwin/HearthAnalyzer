using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Assassin's Blade Weapon
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AssassinsBlade : BaseWeapon
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 3;
        private const int DURABILITY = 4;

        public AssassinsBlade(int id = -1)
        {
            this.Id = id;
            this.Name = "Assassin's Blade";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
