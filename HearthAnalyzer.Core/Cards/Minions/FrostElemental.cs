using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Frost Elemental
    /// Basic Minion
    /// </summary>
    public class FrostElemental : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 6;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 5;

        public FrostElemental(int id = -1)
        {
            this.Id = id;
            this.Name = "Frost Elemental";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.CurrentAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            var targetMinion = subTarget as BaseMinion;
            var targetPlayer = subTarget as BasePlayer;

            if (targetMinion != null)
            {
                targetMinion.ApplyStatusEffects(MinionStatusEffects.FROZEN);
            }
            else if (targetPlayer != null)
            {
                targetPlayer.ApplyStatusEffects(BasePlayer.PlayerStatusEffects.FROZEN);
            }
        }
    }
}
