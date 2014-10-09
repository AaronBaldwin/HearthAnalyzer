using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Argent Protector
    /// 
    /// <b>Battlecry:</b> Give a friendly minion <b>Divine Shield</b>.
    /// </summary>
    public class ArgentProtector : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 2;
        private const int ATTACK_POWER = 2;
        private const int HEALTH = 2;

        public ArgentProtector(int id = -1)
        {
            this.Id = id;
            this.Name = "Argent Protector";
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
            if (playZone.Any(card => card != null && card != this) && subTarget == null)
            {
                throw new InvalidOperationException("There are friendly minions on the board, you must target one!");
            }

            if (playZone.All(card => card == null) && subTarget != null)
            {
                throw new InvalidOperationException("There are no other friendly minions on the board so you can't target something!");
            }

            if (subTarget != null)
            {
                if (!(subTarget is BaseMinion))
                {
                    throw new InvalidOperationException("You must target a minion!");
                }

                ((BaseMinion)subTarget).ApplyStatusEffects(MinionStatusEffects.DIVINE_SHIELD);
            }
        }
    }
}
