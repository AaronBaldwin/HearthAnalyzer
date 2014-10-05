using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents cards that can battle cry
    /// </summary>
    public interface IBattlecry
    {
        /// <summary>
        /// Triggers the card's battlecry
        /// </summary>
        /// <param name="subTarget">The sub target of the battle cry</param>
        void Battlecry(IDamageableEntity subTarget);
    }
}
