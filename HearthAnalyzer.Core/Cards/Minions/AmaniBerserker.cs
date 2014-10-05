using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Interfaces;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Amani Berserker
    /// 
    /// <b>Enrage:</b> +3 Attack
    /// </summary>
    public class AmaniBerserker : BaseMinion, IEnragable
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 3;
        private const int ENRAGE_POWER = 3;

        public AmaniBerserker(int id = -1)
        {
            this.Id = id;
            this.Name = "Amani Berserker";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Enrage()
        {
            this.TakeBuff(ENRAGE_POWER, 0);
        }

        public void Derage()
        {
            this.TakeBuff(-ENRAGE_POWER, 0);
        }
    }
}
