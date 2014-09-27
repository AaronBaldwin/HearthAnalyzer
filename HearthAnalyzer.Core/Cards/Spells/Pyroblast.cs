using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Pyroblast spell
    /// 
    /// Deal $10 damage.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Pyroblast : BaseSpell
    {
        private const int MANA_COST = 10;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public Pyroblast(int id = -1)
        {
            this.Id = id;
            this.Name = "Pyroblast";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
