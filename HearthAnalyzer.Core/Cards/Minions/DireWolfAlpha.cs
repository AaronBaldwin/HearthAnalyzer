using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Dire Wolf Alpha
    /// 
    /// Adjacent minions have +1 Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class DireWolfAlpha : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 2;

        public DireWolfAlpha(int id = -1)
        {
            this.Id = id;
            this.Name = "Dire Wolf Alpha";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }
    }
}
