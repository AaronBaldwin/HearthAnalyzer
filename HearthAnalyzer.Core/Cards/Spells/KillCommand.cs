using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Kill Command spell
    /// 
    /// Deal $3 damage.  If you have a Beast, deal $5 damage instead.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class KillCommand : BaseSpell
    {
        private const int MANA_COST = 3;
        private const int MIN_SPELL_POWER = 3;
        private const int MAX_SPELL_POWER = 5;

        public KillCommand(int id = -1)
        {
            this.Id = id;
            this.Name = "Kill Command";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
