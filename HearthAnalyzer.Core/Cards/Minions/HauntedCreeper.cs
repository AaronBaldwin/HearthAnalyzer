using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Haunted Creeper
    /// 
    /// <b>Deathrattle:</b> Summon two 1/1 Spectral Spiders.
    /// </summary>
    /// <remarks>
    /// TODO: NOT YET COMPLETELY IMPLEMENTED
    /// </remarks>
    public class HauntedCreeper : BaseMinion
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 1;
        private const int HEALTH = 2;

        public HauntedCreeper(int id = -1)
        {
            this.Id = id;
            this.Name = "Haunted Creeper";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.BEAST;
        }
    }
}
