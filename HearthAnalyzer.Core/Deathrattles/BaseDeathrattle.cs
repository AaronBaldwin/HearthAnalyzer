using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core.Deathrattles
{
    /// <summary>
    /// Base class for implementing a deathrattle effect
    /// </summary>
    public abstract class BaseDeathrattle
    {
        public BaseMinion Source;

        /// <summary>
        /// Performs the deathrattle action. Called when a minion with deathrattle dies.
        /// </summary>
        public abstract void Deathrattle();
    }
}
