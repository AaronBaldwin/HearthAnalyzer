using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the The Beast
    /// 
    /// <b>Deathrattle:</b> Summon a 3/3 Finkle Einhorn for your opponent.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class TheBeast : BaseMinion
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 9;
        private const int HEALTH = 7;

        public TheBeast(int id = -1)
        {
            this.Id = id;
            this.Name = "The Beast";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }
    }
}
