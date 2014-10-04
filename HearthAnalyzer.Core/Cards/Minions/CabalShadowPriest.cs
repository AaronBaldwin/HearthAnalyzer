using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Cabal Shadow Priest
    /// 
    /// <b>Battlecry:</b> Take control of an enemy minion that has 2 or less Attack.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class CabalShadowPriest : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 5;

        public CabalShadowPriest(int id = -1)
        {
            this.Id = id;
            this.Name = "Cabal Shadow Priest";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
