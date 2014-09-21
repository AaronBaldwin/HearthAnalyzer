using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents an unkown card, typically a card in the enemy's deck or hand that has not
    /// been revealed yet.
    /// </summary>
    public class UnknownCard : BaseCard
    {
        public UnknownCard(int id = -1)
        {
            this.Id = id;
            this.Name = "Unknown";
        }
    }
}
