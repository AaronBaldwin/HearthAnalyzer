using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Arcane Golem
    /// 
    /// <b>Charge</b>. <b>Battlecry:</b> Give your opponent a Mana Crystal.
    /// </summary>
    public class ArcaneGolem : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 3;
        private const int ATTACK_POWER = 4;
        private const int HEALTH = 2;

        public ArcaneGolem(int id = -1)
        {
            this.Id = id;
            this.Name = "Arcane Golem";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;

            this.ApplyStatusEffects(MinionStatusEffects.CHARGE);
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            var waitingPlayer = GameEngine.GameState.WaitingPlayer;
            waitingPlayer.AddManaCrystal();
        }
    }
}
