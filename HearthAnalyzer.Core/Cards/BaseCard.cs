using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthAnalyzer.Core.Cards
{
    /// <summary>
    /// Represents a card effect
    /// </summary>
    public enum CardEffect
    {
        NONE,
        FIRST,
        SECOND
    }

    /// <summary>
    /// Represents the base card type
    /// </summary>
    public abstract class BaseCard : IEquatable<BaseCard>
    {
        /// <summary>
        /// The unique id of this card for this game instance
        /// </summary>
        public int Id;

        /// <summary>
        /// Blizard's internal name of this card
        /// </summary>
        public string CardId;

        /// <summary>
        /// The name of this card
        /// </summary>
        public string Name;

        /// <summary>
        /// Who owns this card
        /// </summary>
        public BasePlayer Owner;

        /// <summary>
        /// The type of this card
        /// </summary>
        public CardType Type;

        /// <summary>
        /// The mana cost for this card
        /// </summary>
        public int OriginalManaCost;

        /// <summary>
        /// The current mana cost for this card
        /// </summary>
        public int CurrentManaCost;

        /// <summary>
        /// The original attack power for this card
        /// </summary>
        public int OriginalAttackPower;

        /// <summary>
        /// The current attack power for this card
        /// </summary>
        public int CurrentAttackPower
        {
            get { return this.OriginalAttackPower + this.PermanentAttackBuff + this.TemporaryAttackBuff; }
        }

        /// <summary>
        /// The amount of permanent attack buff
        /// </summary>
        public int PermanentAttackBuff;

        /// <summary>
        /// The amount of temporary attack buff this turn
        /// </summary>
        public int TemporaryAttackBuff;

        /// <summary>
        /// The time that the card was first played on the board
        /// </summary>
        public DateTime TimePlayed;

        #region IComparable

        public override int GetHashCode()
        {
            return this.Id ^ this.Name.GetHashCode();
        }

        public bool Equals(BaseCard other)
        {
            return this.Id == other.Id;    
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", this.Name, this.Id);
        }

        #endregion IComparable
    }

    /// <summary>
    /// Represents the various card types
    /// </summary>
    public enum CardType
    {
        BEAST,
        DEMON,
        DRAGON,
        MURLOC,
        TOTEM,
        PIRATE,
        NORMAL_MINION,
        SPELL,
        WEAPON,
        ACTIVE_WEAPON,
        ACTIVE_SECRET
    }
}
