using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Swipe spell
    /// 
    /// Deal $4 damage to an enemy and $1 damage to all other enemies.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Swipe : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 4;
        private const int MAX_SPELL_POWER = 1;

        public Swipe(int id = -1)
        {
            this.Id = id;
            this.Name = "Swipe";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

			this.BonusSpellPower = 0;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
