using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Frostbolt spell
    /// 
    /// Deal $3 damage to a character and <b>Freeze</b> it.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Frostbolt : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 3;
        private const int MAX_SPELL_POWER = 3;

        public Frostbolt(int id = -1)
        {
            this.Id = id;
            this.Name = "Frostbolt";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
