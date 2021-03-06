using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Naturalize spell
    /// 
    /// Destroy a minion. Your opponent draws 2 cards.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Naturalize : BaseSpell
    {
        private const int MANA_COST = 1;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public Naturalize(int id = -1)
        {
            this.Id = id;
            this.Name = "Naturalize";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
