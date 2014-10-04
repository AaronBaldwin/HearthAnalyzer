using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Mortal Strike spell
    /// 
    /// Deal $4 damage.  If you have 12 or less Health, deal $6 instead.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MortalStrike : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 4;
        private const int MAX_SPELL_POWER = 6;

        public MortalStrike(int id = -1)
        {
            this.Id = id;
            this.Name = "Mortal Strike";

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
