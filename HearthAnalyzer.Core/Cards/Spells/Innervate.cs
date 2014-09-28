using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Innervate spell
    /// 
    /// Gain 2 Mana Crystals this turn only.
    /// </summary>
    public class Innervate : BaseSpell
    {
        private const int MANA_COST = 0;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public Innervate(int id = -1)
        {
            this.Id = id;
            this.Name = "Innervate";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            this.Owner.Mana = Math.Min(this.Owner.Mana + 2, Constants.MAX_MANA_CAPACITY);
        }
    }
}
