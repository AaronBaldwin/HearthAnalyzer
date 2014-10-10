using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Deathrattles;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Bloodmage Thalnos
    /// 
    /// <b>Spell Damage +1</b>. <b>Deathrattle:</b> Draw a card.
    /// </summary>
    public class BloodmageThalnos : BaseMinion, IDeathrattler
    {
        internal const int MANA_COST = 2;
        internal const int ATTACK_POWER = 1;
        internal const int HEALTH = 1;
        internal const int BONUS_SPELL_POWER = 1;

        public BloodmageThalnos(int id = -1)
        {
            this.Id = id;
            this.Name = "Bloodmage Thalnos";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;

            this.BonusSpellPower = BONUS_SPELL_POWER;
        }

        public void RegisterDeathrattle()
        {
            GameEngine.RegisterDeathrattle(this, new DeathrattleDrawCard(this.Owner, 1));
        }
    }
}
