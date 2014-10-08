using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Lightning Storm spell
    /// 
    /// Deal $2-$3 damage to all enemy minions. <b>Overload:</b> (2)
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class LightningStorm : BaseSpell
    {
        private const int MANA_COST = 3;
        private const int MIN_SPELL_POWER = 2;
        private const int MAX_SPELL_POWER = 3;

        public LightningStorm(int id = -1)
        {
            this.Id = id;
            this.Name = "Lightning Storm";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
