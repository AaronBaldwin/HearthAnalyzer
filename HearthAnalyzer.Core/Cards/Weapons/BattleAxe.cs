using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Battle Axe Weapon
    /// </summary>
    public class BattleAxe : BaseWeapon
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 2;
        private const int DURABILITY = 2;

        public BattleAxe(int id = -1)
        {
            this.Id = id;
            this.Name = "Battle Axe";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }
    }
}
