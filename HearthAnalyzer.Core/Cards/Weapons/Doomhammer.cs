using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Doomhammer Weapon
    /// 
    /// <b>Windfury, Overload:</b> (2)
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Doomhammer : BaseWeapon
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 2;
        private const int DURABILITY = 0;

        public Doomhammer(int id = -1)
        {
            this.Id = id;
            this.Name = "Doomhammer";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
