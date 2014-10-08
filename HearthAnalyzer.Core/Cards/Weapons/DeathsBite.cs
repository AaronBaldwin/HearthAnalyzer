using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Deathrattles;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Death's Bite Weapon
    /// 
    /// <b>Deathrattle:</b> Deal 1 damage to all minions.
    /// </summary>
    public class DeathsBite : BaseWeapon, IDeathrattler
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 4;
        private const int DURABILITY = 2;
        private const int DEATHRATTLE_DAMAGE = 1;

        public DeathsBite(int id = -1)
        {
            this.Id = id;
            this.Name = "Death's Bite";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.OriginalAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }

        public void RegisterDeathrattle()
        {
            GameEngine.RegisterDeathrattle(this, new DeathrattleDamageAllMinions(DEATHRATTLE_DAMAGE));
        }
    }
}
