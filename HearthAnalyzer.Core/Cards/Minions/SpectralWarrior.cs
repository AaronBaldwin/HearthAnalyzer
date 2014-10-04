using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Spectral Warrior
    /// 
    /// At the start of your turn, deal 1 damage to your hero.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class SpectralWarrior : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 0;
        private const int HEALTH = 4;

        public SpectralWarrior(int id = -1)
        {
            this.Id = id;
            this.Name = "Spectral Warrior";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }
    }
}
