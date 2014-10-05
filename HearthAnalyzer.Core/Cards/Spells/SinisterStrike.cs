using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Sinister Strike spell
    /// 
    /// Deal $3 damage to the enemy hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SinisterStrike : BaseSpell
    {
        private const int MANA_COST = 1;
        private const int MIN_SPELL_POWER = 3;
        private const int MAX_SPELL_POWER = 3;

        public SinisterStrike(int id = -1)
        {
            this.Id = id;
            this.Name = "Sinister Strike";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
