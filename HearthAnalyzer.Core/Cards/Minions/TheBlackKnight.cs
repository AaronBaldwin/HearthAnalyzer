using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the The Black Knight
    /// 
    /// <b>Battlecry:</b> Destroy an enemy minion with <b>Taunt</b>.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class TheBlackKnight : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 5;

        public TheBlackKnight(int id = -1)
        {
            this.Id = id;
            this.Name = "The Black Knight";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
