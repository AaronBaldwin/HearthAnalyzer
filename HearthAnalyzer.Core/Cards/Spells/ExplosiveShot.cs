using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Explosive Shot spell
    /// 
    /// Deal $5 damage to a minion and $2 damage to adjacent ones.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ExplosiveShot : BaseSpell
    {
        private const int MANA_COST = 5;
        private const int MIN_SPELL_POWER = 5;
        private const int MAX_SPELL_POWER = 2;

        public ExplosiveShot(int id = -1)
        {
            this.Id = id;
            this.Name = "Explosive Shot";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
