using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Succubus
    /// 
    /// <b>Battlecry:</b> Discard a random card.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Succubus : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 3;

        public Succubus(int id = -1)
        {
            this.Id = id;
            this.Name = "Succubus";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
