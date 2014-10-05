using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Interfaces
{
    /// <summary>
    /// Represents a card that can be enraged
    /// </summary>
    public interface IEnragable
    {
        /// <summary>
        /// Enrages the card
        /// </summary>
        void Enrage();

        /// <summary>
        /// Un-enrages the card
        /// </summary>
        void Derage();
    }
}
