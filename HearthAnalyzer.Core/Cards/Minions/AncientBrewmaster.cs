using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Implements the Ancient Brewmaster
    /// 
    /// <b>Battlecry:</b> Return a friendly minion from the battlefield to your hand.
    /// </summary>
    public class AncientBrewmaster : BaseMinion, IBattlecry
    {
        private const int MANA_COST = 4;
        private const int ATTACK_POWER = 5;
        private const int HEALTH = 4;

        public AncientBrewmaster(int id = -1)
        {
            this.Id = id;
            this.Name = "Ancient Brewmaster";
            this.CurrentManaCost = MANA_COST;
            this.OriginalManaCost = MANA_COST;
            this.OriginalAttackPower = ATTACK_POWER;
            this.MaxHealth = HEALTH;
            this.CurrentHealth = HEALTH;
			this.Type = CardType.NORMAL_MINION;
        }

        public void Battlecry(IDamageableEntity subTarget)
        {
            if (GameEngine.GameState.CurrentPlayerPlayZone.Any(card => (card != null && card != this)) && subTarget == null)
            {
                throw new InvalidOperationException("Must have a target if there are minions on the board!");
            }
            
            if (GameEngine.GameState.CurrentPlayerPlayZone.All(card => card == null || card == this) && subTarget == null)
            {
                // No battlecry if there are no friendly minions on the board
                return;
            }

            var targetMinion = subTarget as BaseMinion;
            if (targetMinion == null || !GameEngine.GameState.CurrentPlayerPlayZone.Contains(targetMinion))
            {
                throw new InvalidOperationException("Must target friendly minions!");
            }

            GameEngine.GameState.Board.RemoveCard(targetMinion);
            this.Owner.Hand.Add(targetMinion);
        }
    }
}
