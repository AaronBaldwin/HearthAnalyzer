using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Fireball spell
    /// 
    /// Deals 6 damage.
    /// </summary>
    public class Fireball : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 6;
        private const int MAX_SPELL_POWER = 6;

        public Fireball(int id = -1)
        {
            this.Id = id;
            this.Name = "Fireball";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        /// <summary>
        /// The amount of bonus spell power to add
        /// </summary>
        public int BonusSpellPower;

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
