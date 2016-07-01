using System.Collections.Generic;

namespace BBS.Models
{
    public interface IRollInfo
    {
        List<Dice> Dices { get; }
        int FinalTargetNumber { get; }
        int Result { get; }
        RollType Type { get; }
    }
}