using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Inner Rage spell
    /// 
    /// Deal $1 damage to a minion and give it +2 Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class InnerRage : BaseSpell
    {
        private const int MANA_COST = 0;
        private const int MIN_SPELL_POWER = 1;
        private const int MAX_SPELL_POWER = 1;

        public InnerRage(int id = -1)
        {
            this.Id = id;
            this.Name = "Inner Rage";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
