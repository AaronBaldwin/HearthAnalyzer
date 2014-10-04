using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Spells
{
    /// <summary>
    /// Implements the Hellfire spell
    /// 
    /// Deal $3 damage to ALL characters.
    /// </summary>
    public class Hellfire : BaseSpell
    {
        private const int MANA_COST = 4;
        private const int MIN_SPELL_POWER = 3;
        private const int MAX_SPELL_POWER = 3;

        public Hellfire(int id = -1)
        {
            this.Id = id;
            this.Name = "Hellfire";

            this.OriginalManaCost = MANA_COST;
            this.CurrentManaCost = MANA_COST;

			this.BonusSpellPower = 0;
        }

        public override void Activate(IDamageableEntity target = null)
        {
            int totalSpellDamage = MAX_SPELL_POWER + this.BonusSpellPower;
            GameEngine.GameState.CurrentPlayer.TakeDamage(totalSpellDamage);
            GameEngine.GameState.WaitingPlayer.TakeDamage(totalSpellDamage);
            GameEngine.GameState.CurrentPlayerPlayZone.Where(card => card != null).ToList().ForEach(card => ((IDamageableEntity)card).TakeDamage(totalSpellDamage));
            GameEngine.GameState.WaitingPlayerPlayZone.Where(card => card != null).ToList().ForEach(card => ((IDamageableEntity)card).TakeDamage(totalSpellDamage));
        }
    }
}
