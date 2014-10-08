using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Massive Runeblade Weapon
    /// 
    /// Deals double damage to heroes.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MassiveRuneblade : BaseWeapon
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 10;
        private const int DURABILITY = 2;

        public MassiveRuneblade(int id = -1)
        {
            this.Id = id;
            this.Name = "Massive Runeblade";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
