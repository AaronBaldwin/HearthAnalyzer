using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Blood Knight
    /// 
    /// <b>Battlecry:</b> All minions lose <b>Divine Shield</b>. Gain +3/+3 for each Shield lost.
    /// </summary>
    public class BloodKnight : BaseMinion, IBattlecry
    {
        internal const int MANA_COST = 3;
        internal const int ATTACK_POWER = 3;
        internal const int HEALTH = 3;
        internal const int BATTLECRY_POWER = 3;

        public BloodKnight(int id = -1)
        {
            this.Id = id;
            this.Name = "Blood Knight";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            GameEngine.GameState.CurrentPlayerPlayZone.ForEach(card => this.RemoveDivineShieldAndBuffBloodKnight((BaseMinion)card));
            GameEngine.GameState.WaitingPlayerPlayZone.ForEach(card => this.RemoveDivineShieldAndBuffBloodKnight((BaseMinion)card));
        }

        /// <summary>
        /// Removes divine shield from the minion if it has it and buff's the blood knight
        /// </summary>
        /// <param name="minion">The minion to remove divine shield from</param>
        private void RemoveDivineShieldAndBuffBloodKnight(BaseMinion minion)
        {
            if (minion == null) return;
            if (!minion.HasDivineShield) return;

            minion.RemoveStatusEffects(MinionStatusEffects.DIVINE_SHIELD);
            this.TakeBuff(BATTLECRY_POWER, BATTLECRY_POWER);
        }
    }
}
