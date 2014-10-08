using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Interfaces;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Angry Chicken
    /// 
    /// <b>Enrage:</b> +5 Attack.
    /// </summary>
    public class AngryChicken : BaseMinion, IEnragable
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 1;
        private const int ATTACK_BUFF = 5;

        public AngryChicken(int id = -1)
        {
            this.Id = id;
            this.Name = "Angry Chicken";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }

        public void Enrage()
        {
            this.TakeBuff(ATTACK_BUFF, 0);
        }

        public void Derage()
        {
            this.TakeBuff(-ATTACK_BUFF, 0);
        }
    }
}
