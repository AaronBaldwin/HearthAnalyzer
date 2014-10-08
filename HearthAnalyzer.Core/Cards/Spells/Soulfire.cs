using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Soulfire spell
    /// 
    /// Deal $4 damage. Discard a random card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Soulfire : BaseSpell
    {
        private const int MANA_COST = 0;
        private const int MIN_SPELL_POWER = 4;
        private const int MAX_SPELL_POWER = 4;

        public Soulfire(int id = -1)
        {
            this.Id = id;
            this.Name = "Soulfire";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
