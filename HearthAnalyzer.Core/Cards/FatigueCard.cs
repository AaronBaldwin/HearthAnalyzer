using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a fatigue card
    /// </summary>
    public class FatigueCard : BaseCard
    {
        public FatigueCard(int fatigueDamage, int id = -1)
        {
            this.Id = id;
            this.Name = "Fatigue";

            this.FatigueDamage = fatigueDamage;
        }

        /// <summary>
        /// The amount of fatigue damage
        /// </summary>
        public int FatigueDamage;
    }
}
