using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Avenging Wrath spell
    /// 
    /// Deal $8 damage randomly split among enemy characters.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AvengingWrath : BaseSpell
    {
        private const int MANA_COST = 6;
        private const int MIN_SPELL_POWER = 8;
        private const int MAX_SPELL_POWER = 8;

        public AvengingWrath(int id = -1)
        {
            this.Id = id;
            this.Name = "Avenging Wrath";

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
