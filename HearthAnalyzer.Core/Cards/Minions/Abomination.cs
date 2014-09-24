﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthAnalyzer.Core.Deathrattles;

namespace HearthAnalyzer.Core.Cards.Minions
{
    /// <summary>
    /// Represents the Abomination card
    /// Taunt. Deathrattle:
    /// Deal 2 damage to ALL characters.
    /// </summary>
    public class Abomination : BaseMinion
    {
        private static int MANA     = 5;
        private static int ATTACK   = 4;
        private static int HEALTH   = 4;
        private static int DEATHRATTLE_DAMAGE = 2;

        public Abomination(int id = -1)
        {
            this.Id = id;
            this.CardId = "FILLMEIN";
            this.Name = "Abomination";
            this.OriginalManaCost = MANA;
            this.CurrentManaCost = MANA;
            this.CurrentAttackPower = ATTACK;
            this.CurrentHealth = HEALTH;

            GameEngine.RegisterDeathrattle(this, new DeathrattleDamageAllMinions(DEATHRATTLE_DAMAGE));
        }
    }
}