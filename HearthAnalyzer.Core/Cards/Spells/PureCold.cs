using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Pure Cold spell
    /// 
    /// Deal $8 damage to the enemy hero, and <b>Freeze</b> it.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class PureCold : BaseSpell
    {
        private const int MANA_COST = 5;
        private const int MIN_SPELL_POWER = 8;
        private const int MAX_SPELL_POWER = 8;

        public PureCold(int id = -1)
        {
            this.Id = id;
            this.Name = "Pure Cold";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
