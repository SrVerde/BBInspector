using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BBS.Models
{
    /// <summary>
    /// A single roll in the game
    /// Basic object for statistical luck / chance calculations
    /// </summary>
    public class Roll
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Roll()
        {
            Dices = new List<Dice>();
            Modifiers = new List<Modifier>();
            Type = RollType.Unknown;
        }

        /// <summary>
        /// Constructor of choice
        /// </summary>
        /// <param name="diceRolls">Text with rolled dice from the BBRZ file</param>
        /// <param name="requirement">Requirement from the BBRZ file</param
        /// <param name="rollType">Roll type ID from the BBRZ file</param>
        /// <param name="modifiers">Already created modifiers from the BBRZ file</param>
        public Roll(string diceRolls, int requirement, int rollType, List<Modifier> modifiers = null) : this()
        {
            //  Set type of roll
            //
            RollType type = (RollType)rollType;

            if (type == RollType.BounceBallRoll)
            {
                string[] numbers = Regex.Split(diceRolls, @"\D+");

                switch (numbers.Length - 2)
                {
                    case (2):
                        type = RollType.ScatterBallRoll;
                        break;
                    case (3):
                        type = RollType.InaccuratePassRoll;
                        break;
                    default:
                        type = RollType.BounceBallRoll;
                        break;
                }
            }

            Type = type;

            //  If the rolled dice text is null, we can end now
            //
            if (diceRolls == null)
                return;

            Regex expression;
            Match match;
            int dice1;
            int dice2;
            int dice3;
            int modifier = 0;

            //  Get the target number and the modifiers
            //
            InitialTargetNumber = requirement;
            if (modifiers != null)
            {
                foreach (Modifier m in modifiers)
                {
                    Modifiers.Add(m);
                    modifier += m.Value;
                }
            }

            //  Interpret the rolled dice string
            //
            switch (Type)
            {
                case (RollType.FollowUp):
                case (RollType.Move):
                case (RollType.PilingOn):
                case (RollType.Push):
                case (RollType.TouchBack):
                    isNoRoll = true;
                    break;
                case (RollType.ArmourRoll):
                case (RollType.InjuryRoll):
                case (RollType.ShadowRoll):
                case (RollType.StabRoll):
                    expression = new Regex(@"((?<FirstDice>[0-9]+)," + @"(?<SecondDice>[0-9]+))");
                    match = expression.Match(diceRolls);
                    int.TryParse(match.Groups["FirstDice"].ToString(), out dice1);
                    int.TryParse(match.Groups["SecondDice"].ToString(), out dice2);
                    Dices.Add(new Dice(DiceType.SixSidedDice, dice1));
                    Dices.Add(new Dice(DiceType.SixSidedDice, dice2));
                    Result = dice1 + dice2;
                    FinalTargetNumber = InitialTargetNumber - modifier;
                    break;
                case (RollType.BoneheadRoll):
                case (RollType.CatchRoll):
                case (RollType.DodgeRoll):
                case (RollType.GoForItRoll):
                case (RollType.InterceptionRoll):
                case (RollType.LeapRoll):
                case (RollType.LonerRoll):
                case (RollType.PassRoll):
                case (RollType.PickUpRoll):
                case (RollType.ReallyStupidRoll):
                case (RollType.WakeUpFromKORoll):
                case (RollType.WildAnimalRoll):
                    expression = new Regex(@"(?<FirstDice>[0-9]+)");
                    match = expression.Match(diceRolls);
                    int.TryParse(match.Groups["FirstDice"].ToString(), out dice1);
                    Dices.Add(new Dice(DiceType.SixSidedDice, dice1));
                    Result = dice1;
                    FinalTargetNumber = Math.Min(Math.Max(InitialTargetNumber - modifier, 2), 6);
                    break;
                case (RollType.BlockRoll):
                    string[] numbers = Regex.Split(diceRolls, @"\D+");
                    int numDices = (numbers.Length - 2) / 2; //  I do not know why the block dice are rolled twice as needed
                    for (int i = 1; i <= numDices; i++)
                    {
                        int thisNumber;
                        int.TryParse(numbers[i], out thisNumber);
                        Dices.Add(new Dice(DiceType.BlockDice, thisNumber));
                    }
                    if (numDices > 1)
                        Result = (int)BlockDice.Unknown;
                    else
                        Result = Dices[0].Result;
                    break;
                case (RollType.BounceBallRoll):
                    expression = new Regex(@"(?<FirstDice>[0-9]+)");
                    match = expression.Match(diceRolls);
                    int.TryParse(match.Groups["FirstDice"].ToString(), out dice1);
                    Dices.Add(new Dice(DiceType.EightSidedDice, dice1));
                    break;
                case (RollType.ScatterBallRoll):
                    expression = new Regex(@"((?<FirstDice>[0-9]+)," + @"(?<SecondDice>[0-9]+))");
                    match = expression.Match(diceRolls);
                    int.TryParse(match.Groups["FirstDice"].ToString(), out dice1);
                    int.TryParse(match.Groups["SecondDice"].ToString(), out dice2);
                    Dices.Add(new Dice(DiceType.EightSidedDice, dice1));
                    Dices.Add(new Dice(DiceType.SixSidedDice, dice2));
                    break;
                case (RollType.InaccuratePassRoll):
                    expression = new Regex(@"((?<FirstDice>[0-9]+)," + @"(?<SecondDice>[0-9]+)," + @"(?<ThirdDice>[0-9]+))");
                    match = expression.Match(diceRolls);
                    int.TryParse(match.Groups["FirstDice"].ToString(), out dice1);
                    int.TryParse(match.Groups["SecondDice"].ToString(), out dice2);
                    int.TryParse(match.Groups["ThirdDice"].ToString(), out dice3);
                    Dices.Add(new Dice(DiceType.EightSidedDice, dice1));
                    Dices.Add(new Dice(DiceType.EightSidedDice, dice2));
                    Dices.Add(new Dice(DiceType.EightSidedDice, dice3));
                    break;
                case (RollType.CasualtyRoll):
                    expression = new Regex(@"((?<FirstDice>[0-9]+)," + @"(?<SecondDice>[0-9]+))");
                    match = expression.Match(diceRolls);
                    int.TryParse(match.Groups["FirstDice"].ToString(), out dice1);
                    int.TryParse(match.Groups["SecondDice"].ToString(), out dice2);
                    Dices.Add(new Dice(DiceType.SixSidedDice, dice1 / 10));
                    Dices.Add(new Dice(DiceType.EightSidedDice, dice1 - (dice1 / 10) * 10));
                    Result = dice2;
                    break;
                default:
                    isNoRoll = true;
                    break;
            }
        }

        /// <summary>
        /// Block dice can be negative (red" dice)
        /// </summary>
        private bool isRedDice = false;
        /// <summary>
        /// Some roll types are lited as rolls, but they are not
        /// </summary>
        private bool isNoRoll = false;
        /// <summary>
        /// Combinations in 2D6 to roll equal or higher than the array index
        /// </summary>
        private static int[] roll2D6 = new int[] { 36, 36, 36, 35, 33, 30, 26, 21, 15, 10, 6, 3, 1, 0, 0 };
        /// <summary>
        /// Number of doubles results in 2D6 when rolling equal or higher than the array index
        /// </summary>
        private static int[] numDoubles2D6 = new int[] { 6, 6, 6, 5, 5, 4, 4, 3, 3, 2, 2, 1, 1, 0, 0 };

        /// <summary>
        /// Initial target number for success
        /// </summary>
        public int InitialTargetNumber { get; protected set; }
        /// <summary>
        /// Final target number for success (after modifiers)
        /// </summary>
        public int FinalTargetNumber { get; protected set; }
        /// <summary>
        /// Final rolled and chosen result
        /// </summary>
        public int Result { get; protected set; }
        /// <summary>
        /// Modifiers to the dice
        /// </summary>
        public List<Modifier> Modifiers { get; protected set; }
        /// <summary>
        /// Actually rolled dice
        /// </summary>
        public List<Dice> Dices { get; protected set; }
        /// <summary>
        /// Roll type
        /// </summary>
        public RollType Type { get; protected set; }

        /// <summary>
        /// Select a single roll from the rolled dice (usualyy block dice)
        /// </summary>
        /// <param name="index">Which dice is chosen</param>
        /// <returns>Chose's dice value</returns>
        public void SelectDiceResult(int index)
        {
            Result = Dices[index].Result;
            return;
        }
        
        /// <summary>
        /// Set the block dice as negative block dice (defenders choice, "red" dices)
        /// </summary>
        /// <param name="isNegative">TRUE to set block dice as negatives</param>
        public void SetNegativeBlockDice(bool isNegative)
        {
            isRedDice = isNegative;
        }

        public RollResult GetChances(Player currentPlayer, Player targetPlayer, bool canUseReroll = false, bool isFirstFrenzyHit = true)
        {
            RollResult result = new RollResult();

            try
            {
                switch (Type)
                {
                    case (RollType.FollowUp):
                    case (RollType.Move):
                    case (RollType.PilingOn):
                    case (RollType.Push):
                    case (RollType.TouchBack):
                        result.Succees = 1.0;
                        break;
                    case (RollType.ArmourRoll):
                        if (targetPlayer.Id == currentPlayer.Id)
                        {
                            result.Succees = GetRoll2DRisk();
                            result.Failure = GetRoll2DChance();
                            result.AttackerStunned = result.Failure;
                        }
                        else
                        {
                            result.Succees = GetRoll2DChance();
                            result.Failure = GetRoll2DRisk();
                            result.DefenderStunned = result.Succees;
                        }
                        break;
                    case (RollType.InjuryRoll):
                    case (RollType.ShadowRoll):
                    case (RollType.StabRoll):
                        result.Succees = GetRoll2DChance();
                        break;
                    case (RollType.BoneheadRoll):
                    case (RollType.LonerRoll):
                    case (RollType.ReallyStupidRoll):
                    case (RollType.WakeUpFromKORoll):
                    case (RollType.WildAnimalRoll):
                        result.Succees = GetRoll1DChance();
                        break;
                    case (RollType.CatchRoll):
                    case (RollType.DodgeRoll):
                    case (RollType.GoForItRoll):
                    case (RollType.InterceptionRoll):
                    case (RollType.LeapRoll):
                    case (RollType.PassRoll):
                    case (RollType.PickUpRoll):
                        result.Succees = GetRoll1DChance();
                        result.Failure = GetRoll1DRisk();
                        break;
                    case (RollType.BlockRoll):
                        result.Succees = GetBlockChance(currentPlayer, targetPlayer);
                        result.Failure = GetBlockRisk(currentPlayer);
                        break;
                    case (RollType.BounceBallRoll):
                    case (RollType.ScatterBallRoll):
                    case (RollType.InaccuratePassRoll):
                    case (RollType.CasualtyRoll):
                        break;
                    default:
                        break;
                }
            }
            catch 
            {
                // empty result on error
            }

            return result;
        }

        /// <summary>
        /// Get the probability of succeding in a 2D6 roll (rolling equal or higher than desired)
        /// </summary>
        /// <returns>Probability in succeding in a 2D6 roll from 0.0 to 1.0</returns>
        private double GetRoll2DChance()
        {
            int required = FinalTargetNumber;
            
            if (FinalTargetNumber < 0)
                required = 0;
            else if (required > roll2D6.Length - 1)
                required = roll2D6.Length - 1;

            return roll2D6[required] / 36.0;
        }

        /// <summary>
        /// Get the probability of succeding in a D6 roll (rolling equal or higher than desired)
        /// </summary>
        /// <param name="canSpareReRoll">TRUE if a skill reroll is available</param>
        /// <returns>Probability in succeding in a D6 roll from 0.0 to 1.0</returns>
        private double GetRoll1DChance(bool hasSkillReRoll = true)
        {
            //  The probability of success is the complementary of the probability of failure
            //
            return 1.0 - GetRoll1DRisk(hasSkillReRoll);
        }

        /// <summary>
        /// Calculate the chance to succeed in a block roll
        /// </summary>
        /// <param name="blocker">Attacking player</param>
        /// <param name="defender">Defending player</param>
        /// <param name="canUseReroll">TRUE if a re roll can be used</param>
        /// <param name="isFirstFrenzyHit">TRUE if this is the first attack of a frenzy player</param>
        /// <returns>Probability in succeeding</returns>
        private double GetBlockChance(Player blocker, Player defender, bool canUseReroll = false, bool isFirstFrenzyHit = true)
        {
            bool bothDownWorks = false;
            bool stumbleWorks = true;
            bool pushWorks = false;

            //  Determine which results are good
            //
            GetValidBlockResults(blocker, defender, isFirstFrenzyHit, out bothDownWorks, out stumbleWorks, out pushWorks);

            //  Number of valid results in a single block dice to succedd
            //  (starts with the POW and adds depending in skills)
            //
            int numValidResults = 1;
            if (bothDownWorks)
                numValidResults++;
            if (pushWorks)
                numValidResults += 2;
            if (stumbleWorks)
                numValidResults++;

            //  Total combinations depending on number of dice rolled
            //
            double numTotalCombinations = Math.Pow(6, Dices.Count);

            //  Chances are valid combinations divided by total combinations
            //  As a curiosity, if 0 dice are rolled, chances to succeed are 100.0 %
            //
            double chance = (isRedDice) ?
                             Math.Pow(numValidResults, Dices.Count) / numTotalCombinations :
                            (1.0 - Math.Pow(6 - numValidResults, Dices.Count) / numTotalCombinations);

            //  Increase probability if a reroll can be spared in this
            //
            if (canUseReroll)
                chance += (1.0 - chance) * chance;

            return chance;
        }

        /// <summary>
        /// Get the risk of rolling two D6 and adding up (rolling lower than desired)
        /// </summary>
        /// <param name="canSpareReRoll">TRUE if a reroll (either team or skill) can be spared</param>
        /// <returns>Probability of failing the roll from 0.0 to 1.0</returns>        
        private double GetRoll2DRisk(bool canSpareReRoll = false)
        {
            double chances = 1.0 - GetRoll2DChance();

            return ((canSpareReRoll) ? chances * chances : chances);
        }

        /// <summary>
        /// Get the risk of fouling (failure is a turnover)
        /// </summary>
        /// <param name="fouler">Player fouling</param>
        /// <returns>Probability of player being seen by the referee</returns>
        private double GetFoulRisk(Player fouler)
        {
            if (!fouler.Skills.Contains(SkillEnum.DirtyPlayer)) return 6.0 / 36.0;

            int required = FinalTargetNumber;

            if (required < 0)
                required = 0;
            else if (required >= numDoubles2D6.Length - 1)
                required = numDoubles2D6.Length - 1;

            return numDoubles2D6[required] / 36.0;
        }

        /// <summary>
        /// Get the risk of rolling one D6 (rolling lower than desired) and producing a turnover
        /// </summary>
        /// <param name="canSpareReRoll">TRUE if a reroll (either team or skill) can be spared</param>
        /// <returns>Probability of failing the roll (most probably causing a turnover) from 0.0 to 1.0</returns>
        private double GetRoll1DRisk(bool canSpareReRoll = true)
        {
            double chances = FinalTargetNumber / 6.0;

            return ((canSpareReRoll) ? chances * chances : chances);
        }

        /// <summary>
        /// Calculate the chance to cause a turnover in a block roll
        /// </summary>
        /// <param name="blocker">Attacking player</param>
        /// <param name="canUseReroll">TRUE if a re roll can be used</param>
        /// <returns>Probability in failing</returns>
        private double GetBlockRisk(Player blocker, bool canUseReroll = false)
        {
            bool bothDownkWorks = IsBothDownValid(blocker);

            //  Number of results in a single block dice to produce a turnover
            //  (starts with the SKULL and BOTH DOWN and removes depending in skills)
            //
            int numInvalidResults = 2;
            if (bothDownkWorks)
                numInvalidResults--;

            //  Total combinations depending on number of dice rolled
            //
            double numTotalCombinations = Math.Pow(6, Dices.Count);

            //  Chances are valid combinations divided by total combinations
            //  As a curiosity, if 0 dice are rolled, chances to get a turnover are 0.0 %
            //
            double chances = !(isRedDice) ?
                             Math.Pow(numInvalidResults, Dices.Count) / numTotalCombinations :
                             (1.0 - Math.Pow(6 - numInvalidResults, Dices.Count) / numTotalCombinations);

            //  If a re-roll can be spared, then the risk is calculated by failing two consecutive times
            //
            return (canUseReroll) ? chances * chances : chances;
        }

        /// <summary>
        /// Get which results in the block dice are valid
        /// </summary>
        /// <param name="blocker">Attacking player</param>
        /// <param name="defender">Defending player</param>
        /// <param name="isFirstFrenzyHit">TRUE if it is the fist attack in a frenzy player</param>
        /// <param name="bothDownWorks">TRUE if a "both down" result is considered a success</param>
        /// <param name="stumbleWorks">TRUE if a "stumble" result is considered a success</param>
        /// <param name="pushWorks">TRUE if a "push" result is considered a success</param>
        private void GetValidBlockResults(Player blocker, Player defender, bool isFirstFrenzyHit, out bool bothDownWorks, out bool stumbleWorks, out bool pushWorks)
        {
            bothDownWorks = false;
            stumbleWorks = true;
            pushWorks = false;

            //  We have block!
            //
            bothDownWorks = blocker.Skills.Contains(SkillEnum.Block);

            //  But the defender also has block (or wrestle)
            //
            bothDownWorks = !defender.Skills.Contains(SkillEnum.Block)
                         && !defender.Skills.Contains(SkillEnum.Wrestle);

            //  But we want to get down the ball carrier!
            //
            bothDownWorks = defender.HasTheBall
                         && blocker.Skills.Contains(SkillEnum.Wrestle);

            //  The defender has dodge
            //
            if (defender.Skills.Contains(SkillEnum.Dodge))
            {
                stumbleWorks = false;

                //  But we have tackle!
                //
                stumbleWorks = blocker.Skills.Contains(SkillEnum.Tackle);
            }

            //  We want to strip ball
            //
            if (defender.HasTheBall && blocker.Skills.Contains(SkillEnum.StripBall))
            {
                //  But the defender has sure hands or stand firm
                //
                pushWorks = !defender.Skills.Contains(SkillEnum.SureHands)
                         && !defender.Skills.Contains(SkillEnum.StandFirm);
                stumbleWorks = stumbleWorks || pushWorks;
            }

            //  This is a frenzy player with the first hit
            //
            pushWorks = pushWorks
                    || (blocker.Skills.Contains(SkillEnum.Frenzy)
                     && isFirstFrenzyHit
                     && !defender.Skills.Contains(SkillEnum.StandFirm));
            stumbleWorks = stumbleWorks || pushWorks;

            //  Check if we are hitting a player close to the border of the pitch
            //
            if (defender.IsBetweenPublicAndPlayer(blocker))
            {
                pushWorks = pushWorks
                        || !defender.Skills.Contains(SkillEnum.StandFirm);
                stumbleWorks = stumbleWorks || pushWorks;
            }

            //  We also have juggernaut (if relevant)
            //
            bothDownWorks = bothDownWorks 
                        || (pushWorks 
                         && blocker.Skills.Contains(SkillEnum.Juggernaut));
        }

        /// <summary>
        /// Get if a result of "both down" could cause a turnover or not
        /// </summary>
        /// <param name="blocker">Attacking player</param>
        /// <returns>FALSE if a "both down" result causes turnover</returns>
        private bool IsBothDownValid(Player blocker)
        {
            //  We have block! (or wrestle and not carrying the ball) (or juggernaut)
            //
            if (blocker.Skills.Contains(SkillEnum.Block) ||
               (blocker.Skills.Contains(SkillEnum.Wrestle) && !blocker.HasTheBall) ||
                blocker.Skills.Contains(SkillEnum.Juggernaut))
                return true;
            else
                return false;
        } 

        /// <summary>
        /// Get if the result of the bloick can be considered as a success
        /// </summary>
        /// <param name="blocker">Attacking player</param>
        /// <param name="defender">Defending player</param>
        /// <param name="isFirstFrenzyHit">TRUE if if it the forst attack of a frenzy player</param>
        /// <returns>TRUE if block was a success</returns>
        private bool IsBlockSuccessful(Player blocker, Player defender, bool isFirstFrenzyHit = true)
        {
            bool bothDownWorks = false;
            bool stumbleWorks = true;
            bool pushWorks = false;
            bool isBlockSuccessful = false;

            //  Determine which results are good
            //
            GetValidBlockResults(blocker, defender, isFirstFrenzyHit, out bothDownWorks, out stumbleWorks, out pushWorks);

            //  View the outcome of the dice
            //
            switch ((BlockDice)Result)
            {
                case (BlockDice.Skull):
                    isBlockSuccessful = false;
                    break;
                case (BlockDice.BothDown):
                    isBlockSuccessful = bothDownWorks;
                    break;
                case (BlockDice.Push):
                    isBlockSuccessful = pushWorks;
                    break;
                case (BlockDice.Stumble):
                    isBlockSuccessful = stumbleWorks;
                    break;
                case (BlockDice.Pow):
                    isBlockSuccessful = true;
                    break;
            }

            return isBlockSuccessful;
        }

        /// <summary>
        /// Get if the result of the block was a failure, causing a turnover
        /// </summary>
        /// <param name="blocker">Attacking player</param>
        /// <returns>TRUE if the block was a fumble</returns>
        private bool IsBlockFailure(Player blocker)
        {
            bool bothDownWorks = IsBothDownValid(blocker);
            bool isBlockFailure = false;

            //  View the outcome of the dice
            //
            switch ((BlockDice)Result)
            {
                case (BlockDice.Skull):
                    isBlockFailure = true;
                    break;
                case (BlockDice.BothDown):
                    isBlockFailure = !bothDownWorks;
                    break;
                case (BlockDice.Push):
                    isBlockFailure = false;
                    break;
                case (BlockDice.Stumble):
                    isBlockFailure = false;
                    break;
                case (BlockDice.Pow):
                    isBlockFailure = false;
                    break;
            }

            return isBlockFailure;
        }

        /// <summary>
        /// Get if rolled 1D6 was successful
        /// </summary>
        /// <returns>TRUE if roll was succesful</returns>
        private bool IsRoll1DSuccessful()
        {
            return Result == 6 || (Result != 1 && Result >= FinalTargetNumber);
        }

        /// <summary>
        /// Get if rolled 2D6 was successful
        /// </summary>
        /// <returns>TRUE if roll was succesful</returns>
        private bool IsRoll2DSuccessful(int roll, int target)
        {
            return (Result >= FinalTargetNumber);
        }

        /// <summary>
        /// Deafult ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (isNoRoll)
                return Type.ToString();

            string type = Type.ToString();
            string targetNumber = "";
            string result = "";
            bool hasTargetNumber = (InitialTargetNumber > 1);
            
            if (Type == RollType.BlockRoll)
            {
                hasTargetNumber = false;

                foreach (Dice roll in Dices)
                {
                    result += ((BlockDice)(roll.Result) + ",");
                }

                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1) + String.Format(" ({0})", (BlockDice)Result);
            }
            else if (Type == RollType.CasualtyRoll)
            {
                hasTargetNumber = false;

                result += (Dices[0].Result + ",");
                result += (Dices[1].Result);
                result += (" (" + (CasualtyEffect)Result +")");
            }
            else
            {
                foreach (Dice roll in Dices)
                {
                    result += (roll.Result.ToString() + ",");
                }

                if (result.Length > 0)
                    result = (Dices.Count > 1) ?
                              result.Substring(0, result.Length - 1) + String.Format(" ({0})", Result) :
                              result.Substring(0, result.Length - 1);
            }

            targetNumber = (InitialTargetNumber == FinalTargetNumber) ?
                            InitialTargetNumber.ToString() + "+" :
                            String.Format("{0}+({1}+)", InitialTargetNumber, FinalTargetNumber);

            return (hasTargetNumber) ?
                    String.Format("{0} {1} : {2}", type, targetNumber, result) :
                    String.Format("{0} : {1}", type, result);
        }
    }
    
    /// <summary>
    /// There are many roll types in Blood bowl!
    /// </summary>
    public enum RollType
    {
        Unknown = -1,
        /// <summary>
        /// This is no roll, but it is identified as so in the replay
        /// Move one square
        /// </summary>
        Move = 0,
        /// <summary>
        /// Roll to stand up in the just sprinted square
        /// </summary>
        GoForItRoll = 1,
        /// <summary>
        /// Roll to move out / between tackle zones
        /// </summary>
        DodgeRoll = 2,
        /// <summary>
        /// Roll to surpass armour value
        /// </summary>
        ArmourRoll = 3,
        /// <summary>
        /// Roll to see if player is stunned, KO, injured
        /// </summary>
        InjuryRoll = 4,
        /// <summary>
        /// Roll block dice
        /// </summary>
        BlockRoll = 5,
        /// <summary>
        /// Roll to pick the ball from the ground
        /// </summary>
        PickUpRoll = 7,
        /// <summary>
        /// Roll for what kind of casualty the injured player gets
        /// </summary>
        CasualtyRoll = 8,
        /// <summary>
        /// Roll to cath the ball (a pass or a bouncing ball)
        /// </summary>
        CatchRoll = 9,
        /// <summary>
        /// Roll for where the ball goes
        /// </summary>        
        BounceBallRoll = 10,
        unknown11 = 11, 
        /// <summary>
        /// Roll to throw a passing ball
        /// </summary>
        PassRoll = 12,
        /// <summary>
        /// This is no roll, but it is identified as so in the replay
        /// Push the defender one square
        /// </summary>
        Push = 13,
        /// <summary>
        /// This is no roll, but it is identified as so in the replay
        /// Follow the pushed defender (or not)
        /// </summary>
        FollowUp = 14,
        unknown15 = 15,
        /// <summary>
        /// Roll to intercept an ongoing pass
        /// </summary>
        InterceptionRoll = 16, // ??
        /// <summary>
        /// Roll to wake up from KO
        /// </summary>           
        WakeUpFromKORoll = 17,
        unknown18 = 18,
        TouchBack = 19, // ??
        /// <summary>
        /// Roll to see if player obeys command (bone head, wild animal or really stupid)
        /// </summary>
        BoneheadRoll = 20, 
        ReallyStupidRoll = 21,
        WildAnimalRoll = 22, 
        /// <summary>
        /// Roll to see if the just spent reroll is applicable to the loner guy
        /// </summary>
        LonerRoll = 23,
        unknown24 = 24,
        unknown25 = 25,
        unknown26 = 26,
        unknown27 = 27,
        unknown28 = 28,
        DauntlessRoll = 29, // ??
        unknown30 = 30,
        unknown31 = 31,
        /// <summary>
        /// Roll to scape from a shadowing enemy
        /// </summary>
        ShadowRoll = 32, 
        unknown33 = 33,
        /// <summary>
        /// Roll to stab an anemy (similar to blitz / block, but roll for armour instead)
        /// </summary>
        StabRoll = 34,
        unknown35 = 35,
        /// <summary>
        /// Roll to jump 2 squares
        /// </summary>
        LeapRoll = 36,
        unknown37 = 37,
        unknown38 = 38,
        unknown39 = 39,
        unknown40 = 40,
        unknown41 = 41,
        unknown42 = 42,
        unknown43 = 43,
        unknown44 = 44,
        unknown45 = 45,
        unknown46 = 46,
        unknown47 = 47,
        unknown48 = 48,
        unknown49 = 49,
        unknown50 = 50,
        unknown51 = 51,
        unknown52 = 52,
        unknown53 = 53,
        unknown54 = 54,
        unknown55 = 55,
        unknown56 = 56,
        unknown57 = 57,
        unknown58 = 58,
        /// <summary>
        /// This is not a roll, the roll is made later as an armour roll
        /// </summary>
        PilingOn = 59,
        unknown60 = 60,
        unknown61 = 61,
        /// <summary>
        /// Do not know what is this
        /// It appears between rolling one block dice and selecting the "stumble" result when the defeder has "dodge"
        /// It has a result (from 1D6 or 1D8) and a requirement of 0, so it seems scattering the ball
        /// </summary>
        unknown62 = 62, // ??
        unknown63 = 63,
        unknown64 = 64,
        /// <summary>
        /// Bouncing the ball and scattering the ball seems to be the same in the BBRZ files
        /// Distinction between these three is made through the rolled dice
        /// Bouncing ball should be 1D8
        /// Scattered ball is 1D8 & 1D6
        /// Inaccurate pass is 3D8
        /// </summary>
        ScatterBallRoll = 110,
        InaccuratePassRoll = 210,
        /// <summary>
        /// Casualty rolls and apotecray rolls seems to be the same in the BBRZ files
        /// Distinction between these two is made through the rolled dice
        /// </summary>
        ApothecaryRoll = 104,
    }

    public enum CasualtyRoll
    {
        BadlyHurt = 38,

        unknown39 = 39,
        unknown40 = 40,

        BrokenRibs = 41,
        GrolnStrain = 42,
        GougedEye = 43,
        BrokenJaw = 44,
        FracturedArm = 45,
        FracturedLeg = 46,
        SmashedHand = 47,
        PinchedNerve = 48,

        unknown49 = 49,
        unknown50 = 50,

        DamagedBack = 51,
        SmashedKnee = 52,
        SmashedHip = 53,
        SmashedAnkle = 54,
        SeriousConcussion = 55,
        FracturedSkull = 56,
        BrokenNeck = 57,
        SmashedCollarBone = 58,

        unknown59 = 59,
        unknown60 = 60,

        Dead = 61
    }

    public enum CasualtyEffect
    {
        Unknown = -1,
        ApothecaryUsed = 0,
        NoLongTerm = 1,
        MissNextGameRibs = 2,
        MissNextGameStrain = 3,
        MissNextGameEye = 4,
        MissNextGameJaw = 5,
        MissNextGameArm = 6,
        MissNextGameLeg = 7,
        MissNextGameHand = 8,
        MissNextGameNerve = 9,
        NigglingInjuryNerve = 10,
        NigglingInjuryBack = 11,
        LessMovementHip = 12,
        LessMovementAnkle = 13,
        LessArmourConcussion = 14,
        LessArmourSkull = 15, 
        LessAgility = 16, 
        LessStrength = 17, 
        Dead = 18
    }
}
