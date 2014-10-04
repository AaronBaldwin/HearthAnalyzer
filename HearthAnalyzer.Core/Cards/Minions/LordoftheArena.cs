using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Lord of the Arena
    /// 
    /// <b>Taunt</b>
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class LordoftheArena : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 6;
        private const int HEALTH = 5;

        public LordoftheArena(int id = -1)
        {
            this.Id = id;
            this.Name = "Lord of the Arena";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
