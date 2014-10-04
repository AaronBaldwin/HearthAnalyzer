using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Abusive Sergeant
    /// 
    /// <b>Battlecry:</b> Give a minion +2 Attack this turn.
    /// </summary>
    public class AbusiveSergeant : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 1;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 1;
        private const int BATTLECRY_POWER = 2;

        public AbusiveSergeant(int id = -1)
        {
            this.Id = id;
            this.Name = "Abusive Sergeant";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            if (subTarget is BasePlayer)
            {
                throw new InvalidOperationException("Can't buff players!");
            }

            subTarget.TakeTemporaryBuff(BATTLECRY_POWER);
        }
    }
}
