using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents the base spell card
    /// </summary>
    public abstract class BaseSpell : BaseCard
    {
        protected BaseSpell()
        {
            this.Type = CardType.SPELL;
        }

        /// <summary>
        /// Activates the spell card with an optional target
        /// </summary>
        /// <param name="target">The target for the spell card if applicable</param>
        public abstract void Activate(IDamageableEntity target = null);
    }
}
