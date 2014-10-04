using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Sacrificial Pact spell
    /// 
    /// Destroy a Demon. Restore #5 Health to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SacrificialPact : BaseSpell
    {
        private const int MANA_COST = 0;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public SacrificialPact(int id = -1)
        {
            this.Id = id;
            this.Name = "Sacrificial Pact";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

			this.BonusSpellPower = 0;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
