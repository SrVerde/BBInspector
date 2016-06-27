using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class RollResult
    {
        public RollResult()
        { }

        public double Succees { get; set; }
        public double Failure { get; set; }
        public double AttackerDown { get; set; }
        public double AttackerStunned { get; set; }
        public double AttackerOutOfThePitch { get; set; }
        public double DefenderDown { get; set; }
        public double DefenderStunned { get; set; }
        public double DefenderOutOfThePitch { get; set; }
    }
}
