using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Felguard
    /// 
    /// <b>Taunt</b>. <b>Battlecry:</b> Destroy one of your Mana Crystals.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class Felguard : BaseMinion
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 3;
        private const int HEALTH = 5;

        public Felguard(int id = -1)
        {
            this.Id = id;
            this.Name = "Felguard";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.DEMON;
        }
    }
}
