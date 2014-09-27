using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Holy Fire spell
    /// 
    /// Deal $5 damage.  Restore #5 Health to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HolyFire : BaseSpell
    {
        private const int MANA_COST = 6;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public HolyFire(int id = -1)
        {
            this.Id = id;
            this.Name = "Holy Fire";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
