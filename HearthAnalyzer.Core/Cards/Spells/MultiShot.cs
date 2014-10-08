using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Multi-Shot spell
    /// 
    /// Deal $3 damage to two random enemy minions.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class MultiShot : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 3;
        private const int MAX_SPELL_POWER = 3;

        public MultiShot(int id = -1)
        {
            this.Id = id;
            this.Name = "Multi-Shot";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
