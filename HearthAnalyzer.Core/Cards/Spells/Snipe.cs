using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Snipe spell
    /// 
    /// <b>Secret:</b> When your opponent plays a minion, deal $4 damage to it.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Snipe : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 4;
        private const int MAX_SPELL_POWER = 4;

        public Snipe(int id = -1)
        {
            this.Id = id;
            this.Name = "Snipe";

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
