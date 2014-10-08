using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Argent Commander
    /// 
    /// <b>Charge</b>, <b>Divine Shield</b>
    /// </summary>
    public class ArgentCommander : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;

        public ArgentCommander(int id = -1)
        {
            this.Id = id;
            this.Name = "Argent Commander";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;

            this.ApplyStatusEffects(MinionStatusEffects.CHARGE | MinionStatusEffects.DIVINE_SHIELD);
        }
    }
}
