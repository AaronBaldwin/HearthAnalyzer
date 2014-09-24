using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Death's Bite Weapon
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class DeathsBite : BaseWeapon
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 4;
        private const int DURABILITY = 0;

        public DeathsBite(int id = -1)
        {
            this.Id = id;
            this.Name = "Death's Bite";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
