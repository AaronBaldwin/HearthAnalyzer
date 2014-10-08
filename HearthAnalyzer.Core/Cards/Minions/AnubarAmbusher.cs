using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Deathrattles;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Anub'ar Ambusher
    /// 
    /// <b>Deathrattle:</b> Return a random friendly minion to your hand.
    /// </summary>
    public class AnubarAmbusher : BaseMinion, IDeathrattler
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;

        public AnubarAmbusher(int id = -1)
        {
            this.Id = id;
            this.Name = "Anub'ar Ambusher";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void RegisterDeathrattle()
        {
            GameEngine.RegisterDeathrattle(this, new DeathrattleReturnFriendlyMinion(this.Owner));
        }
    }
}
