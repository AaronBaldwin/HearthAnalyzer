using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Whirlwind spell
    /// 
    /// Deal $1 damage to ALL minions.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Whirlwind : BaseSpell
    {
        private const int MANA_COST = 1;
        private const int MIN_SPELL_POWER = 1;
        private const int MAX_SPELL_POWER = 1;

        public Whirlwind(int id = -1)
        {
            this.Id = id;
            this.Name = "Whirlwind";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
