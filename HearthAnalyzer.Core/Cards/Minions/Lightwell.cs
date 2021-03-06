using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Lightwell
    /// 
    /// At the start of your turn, restore 3 Health to a damaged friendly character.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Lightwell : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 5;

        public Lightwell(int id = -1)
        {
            this.Id = id;
            this.Name = "Lightwell";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
