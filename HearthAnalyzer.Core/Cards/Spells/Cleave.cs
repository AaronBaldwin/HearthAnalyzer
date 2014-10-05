using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Cleave spell
    /// 
    /// Deal $2 damage to two random enemy minions.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Cleave : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 2;
        private const int MAX_SPELL_POWER = 2;

        public Cleave(int id = -1)
        {
            this.Id = id;
            this.Name = "Cleave";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
