using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Savage Roar spell
    /// 
    /// Give your characters +2 Attack this turn.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SavageRoar : BaseSpell
    {
        private const int MANA_COST = 3;
        private const int MIN_SPELL_POWER = 0;
        private const int MAX_SPELL_POWER = 0;

        public SavageRoar(int id = -1)
        {
            this.Id = id;
            this.Name = "Savage Roar";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
