using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Rogues Do It... spell
    /// 
    /// Deal $4 damage. Draw a card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class RoguesDoIt : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 4;
        private const int MAX_SPELL_POWER = 4;

        public RoguesDoIt(int id = -1)
        {
            this.Id = id;
            this.Name = "Rogues Do It...";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
