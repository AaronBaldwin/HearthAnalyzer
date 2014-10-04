using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Wicked Knife Weapon
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class WickedKnife : BaseWeapon
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int DURABILITY = 0;

        public WickedKnife(int id = -1)
        {
            this.Id = id;
            this.Name = "Wicked Knife";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
