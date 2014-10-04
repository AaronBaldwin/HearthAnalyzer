using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Ancestral Spirit spell
    /// 
    /// Give a minion "<b>Deathrattle:</b> Resummon this minion."
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class AncestralSpirit : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public AncestralSpirit(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancestral Spirit";

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
