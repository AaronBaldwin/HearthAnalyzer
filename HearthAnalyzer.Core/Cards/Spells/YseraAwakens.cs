using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Ysera Awakens spell
    /// 
    /// Deal $5 damage to all characters except Ysera.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class YseraAwakens : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 5;
        private const int MAX_SPELL_POWER = 5;

        public YseraAwakens(int id = -1)
        {
            this.Id = id;
            this.Name = "Ysera Awakens";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
