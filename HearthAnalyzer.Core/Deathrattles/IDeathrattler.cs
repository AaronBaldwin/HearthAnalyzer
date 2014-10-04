using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Deathrattles
{
    /// <summary>
    /// Implemented by cards that can deathrattle
    /// </summary>
    public interface IDeathrattler
    {
        /// <summary>
        /// Registers the deathrattle with the GameEventManager
        /// </summary>
        void RegisterDeathrattle();
    }
}
