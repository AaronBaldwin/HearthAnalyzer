using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents a card that can attack
    /// </summary>
    public interface IAttacker
    {
        /// <summary>
        /// Gets the current attack power of the attacker
        /// </summary>
        /// <returns></returns>
        int GetCurrentAttackPower();

        /// <summary>
        /// Attacks the target
        /// </summary>
        /// <param name="target">The target to attack</param>
        void Attack(IDamageableEntity target);
    }
}
