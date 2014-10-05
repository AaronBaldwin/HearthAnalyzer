using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Starfire spell
    /// 
    /// Deal $5 damage.  Draw a card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Starfire : BaseSpell
    {
        private const int MANA_COST = 6;
        private const int MIN_SPELL_POWER = 5;
        private const int MAX_SPELL_POWER = 5;

        public Starfire(int id = -1)
        {
            this.Id = id;
            this.Name = "Starfire";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
