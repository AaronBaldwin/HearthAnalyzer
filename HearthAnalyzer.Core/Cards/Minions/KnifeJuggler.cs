using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Knife Juggler
    /// 
    /// After you summon a minion, deal 1 damage to a random enemy.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class KnifeJuggler : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 2;

        public KnifeJuggler(int id = -1)
        {
            this.Id = id;
            this.Name = "Knife Juggler";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
