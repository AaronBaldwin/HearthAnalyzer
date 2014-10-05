using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Arcane Shot spell
    /// 
    /// Deal $2 damage.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ArcaneShot : BaseSpell
    {
        private const int MANA_COST = 1;
        private const int MIN_SPELL_POWER = 2;
        private const int MAX_SPELL_POWER = 2;

        public ArcaneShot(int id = -1)
        {
            this.Id = id;
            this.Name = "Arcane Shot";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
