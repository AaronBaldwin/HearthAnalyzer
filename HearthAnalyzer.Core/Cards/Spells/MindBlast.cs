using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Mind Blast spell
    /// 
    /// Deal $5 damage to the enemy hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MindBlast : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 5;
        private const int MAX_SPELL_POWER = 5;

        public MindBlast(int id = -1)
        {
            this.Id = id;
            this.Name = "Mind Blast";

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
