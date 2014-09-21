using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents at the most basic level, something that can be targeted
    /// </summary>
    public interface IDamageableEntity
    {
        /// <summary>
        /// Takes a specified amount of damage
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(int damage);

        /// <summary>
        /// Takes a specified amount of healing
        /// </summary>
        /// <param name="healAmount"></param>
        void TakeHealing(int healAmount);

        /// <summary>
        /// Takes a buff to attack and health if applicable
        /// </summary>
        /// <param name="attackBuff">Attack Buff</param>
        /// <param name="healthBuff">Health Buff</param>
        void TakeBuff(int attackBuff, int healthBuff);
    }
}
