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
            var gameBoard = GameEngine.GameState.Board;

            foreach (var card in gameBoard.PlayerZone)
            {
                var minion = card as BaseMinion;
                minion.TakeDamage(this._damage);
            }

            foreach (var card in gameBoard.OpponentZone)
            {
                var minion = card as BaseMinion;
                minion.TakeDamage(this._damage);
            }
        }
    }
}
