using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Big Game Hunter
    /// 
    /// <b>Battlecry:</b> Destroy a minion with an Attack of 7 or more.
    /// </summary>
    public class BigGameHunter : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;
        private const int BATTLECRY_POWER = 7;

        public BigGameHunter(int id = -1)
        {
            this.Id = id;
            this.Name = "Big Game Hunter";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            if (subTarget != null &&
                !GameEngine.GameState.CurrentPlayerPlayZone.Any(card => card != null && card.CurrentAttackPower >= BATTLECRY_POWER && card != this) &&
                !GameEngine.GameState.WaitingPlayerPlayZone.Any(card => card != null && card.CurrentAttackPower >= BATTLECRY_POWER))
            {
                throw new InvalidOperationException("No valid targets!");
            }

            if (subTarget == null &&
                (GameEngine.GameState.CurrentPlayerPlayZone.Any(card => card != null && card.CurrentAttackPower >= BATTLECRY_POWER && card != this) ||
                GameEngine.GameState.WaitingPlayerPlayZone.Any(card => card != null && card.CurrentAttackPower >= BATTLECRY_POWER)))
            {
                throw new InvalidOperationException("There is a valid target, must select one!");
            }

            if (subTarget != null && !(subTarget is BaseMinion))
            {
                throw new InvalidOperationException("Must target minions!");
            }

            var targetMinion = subTarget as BaseMinion;
            if (targetMinion != null)
            {
                if (targetMinion.CurrentAttackPower < BATTLECRY_POWER)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Invalid minion {0}! It does not have enough attack power! Needs to be at least {1}",
                            targetMinion, BATTLECRY_POWER));
                }

                targetMinion.Die();
            }
        }
    }
}
