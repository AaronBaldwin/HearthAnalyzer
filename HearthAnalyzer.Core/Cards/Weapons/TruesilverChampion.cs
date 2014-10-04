using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Truesilver Champion Weapon
    /// 
    /// Whenever your hero attacks, restore 2 Health to it.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class TruesilverChampion : BaseWeapon
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 4;
        private const int DURABILITY = 0;

        public TruesilverChampion(int id = -1)
        {
            this.Id = id;
            this.Name = "Truesilver Champion";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
