using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Deathbloom spell
    /// 
    /// Deal $5 damage to a minion. Summon a Spore.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Deathbloom : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 5;
        private const int MAX_SPELL_POWER = 5;

        public Deathbloom(int id = -1)
        {
            this.Id = id;
            this.Name = "Deathbloom";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
