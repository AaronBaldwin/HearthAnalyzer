using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Weapons
{
    /// <summary>
    /// Implements the Gorehowl weapon
    /// </summary>
    public class Gorehowl : BaseWeapon, IAttacker, IDamageableEntity
    {
        private const int MANA_COST = 7;
        private const int ATTACK_POWER = 7;
        private const int DURABILITY = 1;

        public Gorehowl(int id = -1)
        {
            this.Id = id;
            this.Name = "Gorehowl";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

            this.CurrentAttackPower = ATTACK_POWER;

            this.Durability = DURABILITY;
        }

        public override void TakeDamage(int damage)
        {
            // Gorehowl decreases in attack power isntead of durability each time it attacks
            this.CurrentAttackPower -= damage;

            if (this.CurrentAttackPower <= 0 || this.Durability <= 0)
            {
                this.Die();
            }
        }
    }
}
