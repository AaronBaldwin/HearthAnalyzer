using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Cards;

namespace HearthAnalyzer.Core.Deathrattles
{
    /// <summary>
    /// Returns a random friendly minion to the hand
    /// </summary>
    public class DeathrattleReturnFriendlyMinion : BaseDeathrattle
    {
        private BasePlayer _owner;

        public DeathrattleReturnFriendlyMinion(BasePlayer owner)
        {
            this._owner = owner;
        }

        public override void Deathrattle()
        {
            List<BaseCard> playZone;

            if (this._owner == GameEngine.GameState.Player)
            {
                playZone = GameEngine.GameState.Board.PlayerPlayZone;
            }
            else
            {
                playZone = GameEngine.GameState.Board.OpponentPlayZone;
            }

            int minionCount = playZone.Count(card => card != null);
            if (minionCount != 0)
            {
                int randomMinionIndex = GameEngine.Random.Next(minionCount);
                var randomMinion = playZone[randomMinionIndex];

                if (randomMinion == null)
                {
                    throw new InvalidOperationException("What? This shouldn't ever happen");
                }

                GameEngine.GameState.Board.RemoveCard(randomMinion);
                this._owner.Hand.Add(randomMinion);
            }
        }
    }
}
