using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Unleash the Hounds spell
    /// 
    /// For each enemy minion, summon a 1/1 Hound with <b>Charge</b>.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class UnleashtheHounds : BaseSpell
    {
        private const int MANA_COST = 3;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public UnleashtheHounds(int id = -1)
        {
            this.Id = id;
            this.Name = "Unleash the Hounds";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
