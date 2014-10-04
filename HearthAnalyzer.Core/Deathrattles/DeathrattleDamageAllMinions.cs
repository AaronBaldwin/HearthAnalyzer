using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core.Deathrattles
{
    /// <summary>
    /// Deals X amount of damage to ALL minions
    /// </summary>
    public class DeathrattleDamageAllMinions : BaseDeathrattle
    {
        private int _damage;

        public DeathrattleDamageAllMinions(int damage)
        {
            this._damage = damage;
        }

        public override void Deathrattle()
        {
            var gameState = GameEngine.GameState;

            foreach (var card in gameState.CurrentPlayerPlayZone)
            {
                if (card != null)
                {
                    var minion = card as BaseMinion;
                    minion.TakeDamage(this._damage);
                }
            }

            foreach (var card in gameState.WaitingPlayerPlayZone)
            {
                if (card != null)
                {
                    var minion = card as BaseMinion;
                    minion.TakeDamage(this._damage);
                }
            }
        }
    }
}
