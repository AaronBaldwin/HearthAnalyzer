using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Sludge Belcher
    /// 
    /// <b>Taunt.\nDeathrattle:</b> Summon a 1/2 Slime with <b>Taunt</b>.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SludgeBelcher : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 5;

        public SludgeBelcher(int id = -1)
        {
            this.Id = id;
            this.Name = "Sludge Belcher";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
