using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Blade Flurry spell
    /// 
    /// Destroy your weapon and deal its damage to all enemies.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class BladeFlurry : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public BladeFlurry(int id = -1)
        {
            this.Id = id;
            this.Name = "Blade Flurry";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
