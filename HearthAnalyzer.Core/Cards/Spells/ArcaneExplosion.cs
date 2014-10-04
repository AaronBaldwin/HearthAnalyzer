using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Arcane Explosion spell
    /// 
    /// Deal $1 damage to all enemy minions.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ArcaneExplosion : BaseSpell
    {
        private const int MANA_COST = 2;
        private const int MIN_SPELL_POWER = 1;
        private const int MAX_SPELL_POWER = 1;

        public ArcaneExplosion(int id = -1)
        {
            this.Id = id;
            this.Name = "Arcane Explosion";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

			this.BonusSpellPower = 0;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            throw new NotImplementedException();
        }
    }
}
