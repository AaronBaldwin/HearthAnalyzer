using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Explosive Trap spell
    /// 
    /// <b>Secret:</b> When your hero is attacked, deal $2 damage to all enemies.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ExplosiveTrap : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public ExplosiveTrap(int id = -1)
        {
            this.Id = id;
            this.Name = "Explosive Trap";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
