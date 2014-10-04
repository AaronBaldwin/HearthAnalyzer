using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Priestess of Elune
    /// 
    /// <b>Battlecry:</b> Restore 4 Health to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class PriestessofElune : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 4;

        public PriestessofElune(int id = -1)
        {
            this.Id = id;
            this.Name = "Priestess of Elune";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
