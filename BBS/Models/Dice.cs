using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BBS.Models
{
    public class Dice
    {
        public Dice()
        { }

        public Dice(DiceType type, int result) :this()
        {
            Type = type;
            Result = result;
        }

        public DiceType Type { get; protected set; }
        public int Result { get; protected set; }
    }
    
    [Serializable]
    public enum DiceType
    {
        Unknown = -1,
        NoDice = 0,
        BlockDice = 1,
        CoinToss = 2,
        SixSidedDice = 6,
        EightSidedDice = 8,
    }

    [Serializable]
    public enum BlockDice
    {
        Unknown = -1,
        Skull = 0,
        BothDown = 1,
        Push = 2,
        Stumble = 3,
        Pow = 4,
        unknown5 = 5,
        unknown6 = 6 
    }
}
