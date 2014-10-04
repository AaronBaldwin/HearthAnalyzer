using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Faceless Manipulator
    /// 
    /// <b>Battlecry:</b> Choose a minion and become a copy of it.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class FacelessManipulator : BaseMinion
    {
        private const int MANA_COST = 5;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 3;

        public FacelessManipulator(int id = -1)
        {
            this.Id = id;
            this.Name = "Faceless Manipulator";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
