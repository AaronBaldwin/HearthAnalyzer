using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Runeblade Weapon
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Runeblade : BaseWeapon
    {
        private const int MANA_COST = 0;
        private const int ATTACK_POWER = 2;
        private const int DURABILITY = 0;

        public Runeblade(int id = -1)
        {
            this.Id = id;
            this.Name = "Runeblade";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
