using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Jaws Weapon
    /// 
    /// Whenever a minion with <b>Deathrattle</b> dies, gain +2 Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Jaws : BaseWeapon
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int DURABILITY = 0;

        public Jaws(int id = -1)
        {
            this.Id = id;
            this.Name = "Jaws";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
