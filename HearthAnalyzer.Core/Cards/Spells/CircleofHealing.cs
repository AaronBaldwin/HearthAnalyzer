using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards.Minions;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Circle of Healing spell
    /// 
    /// Restore #4 Health to ALL minions.
    /// </summary>
    public class CircleofHealing : BaseSpell
    {
        private const int MANA_COST = 0;
        private const int MIN_SPELL_POWER = 4;
        private const int MAX_SPELL_POWER = 4;

        public CircleofHealing(int id = -1)
        {
            this.Id = id;
            this.Name = "Circle of Healing";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;
        }

        public override void Activate(IDamageableEntity target = null, CardEffect cardEffect = CardEffect.NONE)
        {
            GameEngine.GameState.WaitingPlayerPlayZone.ForEach(minion => this.HealTarget((BaseMinion)minion, MAX_SPELL_POWER));
            GameEngine.GameState.CurrentPlayerPlayZone.ForEach(minion => this.HealTarget((BaseMinion)minion, MAX_SPELL_POWER));
        }
    }
}
