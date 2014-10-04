using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Imp Master
    /// 
    /// At the end of your turn, deal 1 damage to this minion and summon a 1/1 Imp.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class ImpMaster : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 5;

        public ImpMaster(int id = -1)
        {
            this.Id = id;
            this.Name = "Imp Master";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
