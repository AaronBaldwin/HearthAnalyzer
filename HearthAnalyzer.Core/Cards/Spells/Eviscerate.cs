using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Eviscerate spell
    /// 
    /// Deal $2 damage. <b>Combo:</b> Deal $4 damage instead.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Eviscerate : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 2;
        private const int MAX_SPELL_POWER = 4;

        public Eviscerate(int id = -1)
        {
            this.Id = id;
            this.Name = "Eviscerate";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
