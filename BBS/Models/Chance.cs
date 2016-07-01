namespace BBS.Models
{
    /// <summary>
    /// Basic chance object
    /// </summary>
    public class Chance
    {
        public Chance()
        {
        }

        public Chance(double successChance, double failureChance) : this()
        {
            Success = successChance;
            Failure = failureChance;
        }

        /// <summary>
        /// Probability of succeding in an action. It is not complementary with the failure
        /// </summary>
        public double Success { get; set; }
        /// <summary>
        /// Probability of failing an action. It is not complementary with the success
        /// </summary>
        public double Failure { get; set; }
    }

    /// <summary>
    /// Single roll chance
    /// Please note that success and failure are not complementary
    /// </summary>
    public class SingleRollChance : Chance
    {
        public SingleRollChance() : base()
        { }

        public SingleRollChance(double successChance, double failureChance) : base(successChance, failureChance)
        {
        }
    }

    /// <summary>
    /// Chance in a roll that can be rerolled somehow
    /// </summary>
    public class CompleteRollChance : Chance
    {
        public CompleteRollChance() : base()
        {
            ProbabilityUsingReroll = 0.0;
            ProbabilityUsingSkill = 0.0;
        }

        public CompleteRollChance(double successChance, double failureChance) : base(successChance, failureChance)
        {
            ProbabilityUsingReroll = 0.0;
            ProbabilityUsingSkill = 0.0;
        }

        public CompleteRollChance(SingleRollChance chance) : this(chance.Success, chance.Failure)
        { }

        /// <summary>
        /// Chances of succeding in the first attept (no reroll or skill used)
        /// </summary>
        public double SuccessAtFirstAttempt { get; set; }
        /// <summary>
        /// Chances of needing a reroll to succeed
        /// </summary>
        public double ProbabilityUsingReroll { get; set; }
        /// <summary>
        /// Chances of needing a skill to succeed
        /// </summary>
        public double ProbabilityUsingSkill { get; set; }
    }

    /// <summary>
    /// Chance of completing a subaction
    /// </summary>
    public class SubActionChance : Chance
    {
        public SubActionChance() : base()
        { }

        public double AttackerDown { get; set; }
        public double AttackerStunned { get; set; }
        public double AttackerOutOfThePitch { get; set; }
        public double DefenderDown { get; set; }
        public double DefenderStunned { get; set; }
        public double DefenderOutOfThePitch { get; set; }
    }

    /// <summary>
    /// Chance of completing an action
    /// </summary>
    public class ActionChance : SubActionChance
    {
        public ActionChance() : base()
        { }
    }
}
