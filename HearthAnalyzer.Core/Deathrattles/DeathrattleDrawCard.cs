using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Deathrattles
{
    /// <summary>
    /// Implements a deathrattle that draws cards
    /// </summary>
    public class DeathrattleDrawCard : BaseDeathrattle
    {
        private BasePlayer _player;
        private int _cardsToDraw;

        /// <summary>
        /// Creates an instance of DeathrattleDrawCard
        /// </summary>
        /// <param name="player">The player to draw cards for</param>
        /// <param name="cardsToDraw">The number of cards to draw</param>
        public DeathrattleDrawCard(BasePlayer player, int cardsToDraw)
        {
            this._player = player;
            this._cardsToDraw = cardsToDraw;
        }

        public override void Deathrattle()
        {
            this._player.DrawCards(this._cardsToDraw);
        }
    }
}
