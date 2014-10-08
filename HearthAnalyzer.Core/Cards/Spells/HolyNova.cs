using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Holy Nova spell
    /// 
    /// Deal $2 damage to all enemies.  Restore #2 Health to all  friendly characters.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HolyNova : BaseSpell
    {
        private const int MANA_COST = 5;
        private const int MIN_SPELL_POWER = 2;
        private const int MAX_SPELL_POWER = 2;

        public HolyNova(int id = -1)
        {
            this.Id = id;
            this.Name = "Holy Nova";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            throw new NotImplementedException();
        }
    }
}
