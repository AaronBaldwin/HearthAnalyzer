using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Sword of Justice Weapon
    /// 
    /// Whenever you summon a minion, give it +1/+1 and this loses 1 Durability.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SwordofJustice : BaseWeapon
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 1;
        private const int DURABILITY = 0;

        public SwordofJustice(int id = -1)
        {
            this.Id = id;
            this.Name = "Sword of Justice";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
