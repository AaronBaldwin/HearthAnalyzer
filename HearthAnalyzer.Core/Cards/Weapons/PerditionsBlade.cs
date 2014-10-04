using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Perdition's Blade Weapon
    /// 
    /// <b>Battlecry:</b> Deal 1 damage. <b>Combo:</b> Deal 2 instead.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class PerditionsBlade : BaseWeapon
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 2;
        private const int DURABILITY = 0;

        public PerditionsBlade(int id = -1)
        {
            this.Id = id;
            this.Name = "Perdition's Blade";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
