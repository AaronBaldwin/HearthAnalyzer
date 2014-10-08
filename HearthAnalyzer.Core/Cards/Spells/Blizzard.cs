using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Blizzard spell
    /// 
    /// Deal $2 damage to all enemy minions and <b>Freeze</b> them.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Blizzard : BaseSpell
    {
        private const int MANA_COST = 6;
        private const int MIN_SPELL_POWER = 2;
        private const int MAX_SPELL_POWER = 2;

        public Blizzard(int id = -1)
        {
            this.Id = id;
            this.Name = "Blizzard";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
