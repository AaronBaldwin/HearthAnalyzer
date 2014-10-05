using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ancient Mage
    /// 
    /// <b>Battlecry:</b> Give adjacent minions <b>Spell Damage +1</b>.
    /// </summary>
    public class AncientMage : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 5;
        private const int BATTLE_CRY_POWER = 1;

        public AncientMage(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancient Mage";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            var playZone = GameEngine.GameState.CurrentPlayerPlayZone;
            int indexOfMage = playZone.FindIndex(card => card == this);
            if (indexOfMage - 1 >= 0)
            {
                var leftMinion = playZone[indexOfMage - 1] as BaseMinion;
                leftMinion.BonusSpellPower += BATTLE_CRY_POWER;
            }

            if (indexOfMage + 1 < Constants.MAX_CARDS_ON_BOARD)
            {
                var rightMinion = playZone[indexOfMage + 1] as BaseMinion;
                rightMinion.BonusSpellPower += BATTLE_CRY_POWER;
            }
        }
    }
}
