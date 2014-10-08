using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Shiv spell
    /// 
    /// Deal $1 damage. Draw a card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Shiv : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 1;
        private const int MAX_SPELL_POWER = 1;

        public Shiv(int id = -1)
        {
            this.Id = id;
            this.Name = "Shiv";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
