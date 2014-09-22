using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core.Heroes
{
    public class Warlock : BasePlayer
    {
        public Warlock()
        {
            this.Graveyard = new List<BaseCard>();
        }
    }
}
