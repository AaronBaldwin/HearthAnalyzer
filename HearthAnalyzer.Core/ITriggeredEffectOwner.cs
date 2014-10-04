using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core
{
    /// <summary>
    /// Represents a card that has a triggered effect
    /// </summary>
    public interface ITriggeredEffectOwner
    {
        /// <summary>
        /// Register the triggered effect with the GameEventManager
        /// </summary>
        void RegisterEffect();
    }
}
