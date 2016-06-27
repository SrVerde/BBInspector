using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class Luck
    {
        public Luck()
        { }

        private static int[] _roll2D6 = new int[] { 36, 36, 36, 35, 33, 30, 26, 21, 15, 10, 6, 3, 1, 0, 0 };

        /*
        /// <summary>
        /// Get the chance to succeed in a single action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="currentPlayer"></param>
        /// <param name="surroundingEnemyPlayers"></param>
        /// <param name="useReroll"></param>
        /// <returns></returns>
        public static void GetSingleActionChance(Action action, Player currentPlayer, List<Player> surroundingEnemyPlayers, bool useReroll = false)
        {
            double chance = 0.0;
            bool isSuccess = false;
            bool isFailure = false;

            switch (action.Type)
            {
                //  Block is a singular case:
                //  Failure is not the complementary of success
                //  Success depends in the intention (knocking down or stripping the ball), there is not target number
                //  The first player in the surrounding enemy player list is the one being blocked
                //
                case (ActionType.Block):
                    chance = GetBlockChance(action.NumDices, currentPlayer.Skills, surroundingEnemyPlayers[0].Skills, surroundingEnemyPlayers[0].HasTheBall);
                    isSuccess = IsBlockSuccessful((BlockDice)action.FinalResult, currentPlayer.Skills, surroundingEnemyPlayers[0].Skills, surroundingEnemyPlayers[0].HasTheBall);
                    isFailure = IsBlockFailure((BlockDice)action.FinalResult, currentPlayer.Skills, currentPlayer.HasTheBall);
                    break;

                //  The required target number must account for:
                //     ·) enemy tackle zones
                //     ·) prehensile tails
                //     ·) diving tackles
                //     ·) nerves of steel
                //
                case (ActionType.Dodge):
                    bool hasDodge = (currentPlayer.Skills != null) && currentPlayer.Skills.Contains(SkillEnum.Dodge.ToString());
                    bool anEnemyHasTackle = false;
                    foreach (Player player in surroundingEnemyPlayers)
                    {
                        if ((player.Skills != null) && player.Skills.Contains(SkillEnum.Dodge.ToString()))
                            anEnemyHasTackle = true;
                    }
                    chance = GetRoll1DChance(action.RequiredResult, hasDodge && !anEnemyHasTackle);
                    isSuccess = IsRoll1DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number must account for:
                //     ·) blizzards
                //
                case (ActionType.GoForIt):
                    bool hasSureFeet = (currentPlayer.Skills != null) && currentPlayer.Skills.Contains(SkillEnum.SureFeet.ToString());
                    chance = GetRoll1DChance(action.RequiredResult, hasSureFeet);
                    isSuccess = IsRoll1DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number must account for:
                //     ·) rain
                //     ·) enemy tackle zones
                //     ·) big hand
                //     ·) nerves of steel
                //
                case (ActionType.PickUp):
                    bool hasSurehands = (currentPlayer.Skills != null) && currentPlayer.Skills.Contains(SkillEnum.SureHands.ToString());
                    chance = GetRoll1DChance(action.RequiredResult, hasSurehands);
                    isSuccess = IsRoll1DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number must account for:
                //     ·) rain
                //     ·) enemy tackle zones
                //     ·) nerves of steel
                //     ·) strong arm
                //
                case (ActionType.Pass):
                    bool hasPass = (currentPlayer.Skills != null) && currentPlayer.Skills.Contains(SkillEnum.Pass.ToString());
                    chance = GetRoll1DChance(action.RequiredResult, hasPass);
                    isSuccess = IsRoll1DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number must account for:
                //     ·) rain
                //     ·) enemy tackle zones
                //     ·) big hand
                //     ·) nerves of steel
                //     ·) accurate pass
                //
                case (ActionType.Catch):
                    bool hasCatch = (currentPlayer.Skills != null) && currentPlayer.Skills.Contains(SkillEnum.Catch.ToString());
                    chance = GetRoll1DChance(action.RequiredResult, hasCatch);
                    isSuccess = IsRoll1DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number must account for:
                //     ·) enemy armour value
                //     ·) mighty blow
                //     ·) fouling assistances
                //
                case (ActionType.Foul):
                    chance = GetRoll2DChance(action.RequiredResult);
                    isSuccess = IsRoll2DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number is KO or CAS and must account for:
                //     ·) mighty blow
                //     ·) stunty
                //     ·) thick skull
                //
                case (ActionType.Injury):
                    chance = GetRoll2DChance(action.RequiredResult);
                    isSuccess = IsRoll2DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;

                //  The required target number must account for:
                //     ·) enemy armour value
                //     ·) mighty blow
                //     ·) other
                //
                case (ActionType.Armour):
                    chance = GetRoll2DChance(action.RequiredResult);
                    isSuccess = IsRoll2DSuccessful(action.FinalResult, action.RequiredResult);
                    isFailure = !isSuccess;
                    break;
            }
        }

        /// <summary>
        /// Get the probability of succeding in a 2D6 roll (rolling equal or higher than desired)
        /// </summary>
        /// <param name="required"></param>
        /// <returns>Probability in succeding in a 2D6 roll from 0.0 to 1.0</returns>
        public static double GetRoll2DChance(int required)
        {
            if (required < 0)
                required = 0;
            else if (required > _roll2D6.Length - 1)
                required = _roll2D6.Length - 1;

            return _roll2D6[required] / 36.0;
        }

        /// <summary>
        /// Get the probability of succeding in a D6 roll (rolling equal or higher than desired)
        /// </summary>
        /// <param name="required">Minimum target number required to succed</param>
        /// <param name="canSpareReRoll">TRUE if a skill reroll is available</param>
        /// <returns>Probability in succeding in a D6 roll from 0.0 to 1.0</returns>
        public static double GetRoll1DChance(int required, bool hasSkillReRoll = true)
        {
            //  The probability of success is the complementary of the probability of failure
            //
            return 1.0 - Risk.GetRoll1DRisk(required, hasSkillReRoll);
        }

        /// <summary>
        /// Chances in succedding in a block action from 0.0 to 100.0 [%]
        /// </summary>
        /// <param name="dices">Number of dices rolled. Negative if attacker is weaker than defender</param>
        /// <param name="blockerSkills">Blocking player skill list</param>
        /// <param name="targetSkills">Target player skill list</param>
        /// <param name="targetHasBall">TRUE if target is the ball carrier</param>
        /// <returns>Success probability from 0.0 to 1.0</returns>
        public static double GetBlockChance(int dices, List<string> blockerSkills, List<string> targetSkills, bool targetHasBall = false)
        {
            bool blockerHasSkills = blockerSkills != null;
            bool targetHasSkills = targetSkills != null;
            bool bothDownWorks = false;
            bool stumbleWorks = true;
            bool pushWorks = false;

            //  We have block!
            //
            if (blockerHasSkills && blockerSkills.Contains(SkillEnum.Block.ToString()))
                bothDownWorks = true;

            //  But the defender also has block (or wrestle)
            //
            if (targetHasSkills && (targetSkills.Contains(SkillEnum.Block.ToString()) ||
                                    targetSkills.Contains(SkillEnum.Wrestle.ToString())))
                bothDownWorks = false;

            //  But we want to get down the ball carrier!
            //
            if (targetHasBall && blockerSkills.Contains(SkillEnum.Wrestle.ToString()))
                bothDownWorks = true;

            //  The defender has dodge
            //
            if (targetHasSkills && targetSkills.Contains(SkillEnum.Dodge.ToString()))
            {
                stumbleWorks = false;

                //  But we have tackle!
                //
                if (blockerHasSkills && blockerSkills.Contains(SkillEnum.Tackle.ToString()))
                    stumbleWorks = true;
            }

            //  We want to strip ball
            //
            if (targetHasBall && blockerHasSkills && blockerSkills.Contains(SkillEnum.StripBall.ToString()))
            {
                pushWorks = true;
                stumbleWorks = true;

                //  But the defender has sure hands
                //
                if (targetHasSkills && targetSkills.Contains(SkillEnum.SureHands.ToString()))
                {
                    pushWorks = false;
                    if (targetHasSkills && targetSkills.Contains(SkillEnum.Dodge.ToString()))
                    {
                        stumbleWorks = false;
                        if (blockerHasSkills && blockerSkills.Contains(SkillEnum.Tackle.ToString()))
                            stumbleWorks = true;
                    }
                }

                //  We also have juggernaut (if relevant)
                //
                if (pushWorks && blockerSkills.Contains(SkillEnum.Juggernaut.ToString()))
                    bothDownWorks = true;
            }

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
            double numTotalCombinations = Math.Pow(6, Math.Abs(dices));

            //  Chances are valid combinations divided by total combinations
            //  As a curiosity, if 0 dice are rolled, chances to succeed are 100.0 %
            //
            return (dices <= 0) ?
                   Math.Pow(numValidResults, -dices) / numTotalCombinations :
                  (1.0 - Math.Pow(6 - numValidResults, dices) / numTotalCombinations);
        }

        /// <summary>
        /// Get if the rolled block was a success or not
        /// </summary>
        /// <param name="roll">Actually rolled dice</param>
        /// <param name="blockerSkills">Attacking player skill list</param>
        /// <param name="targetSkills">Defending player skill list</param>
        /// <param name="targetHasBall">TRUE if the defendes ir the ball carrier</param>
        /// <returns>TRUE if the action was a success</returns>
        public static bool IsBlockSuccessful(BlockDice roll, List<string> blockerSkills, List<string> targetSkills, bool targetHasBall = false)
        {
            bool blockerHasSkills = blockerSkills != null;
            bool targetHasSkills = targetSkills != null;
            bool bothDownWorks = false;
            bool stumbleWorks = true;
            bool pushWorks = false;
            bool isBlockSuccessful = false;

            //  We have block!
            //
            if (blockerHasSkills && blockerSkills.Contains(SkillEnum.Block.ToString()))
                bothDownWorks = true;

            //  But the defender also has block (or wrestle)
            //
            if (targetHasSkills && (targetSkills.Contains(SkillEnum.Block.ToString()) ||
                                    targetSkills.Contains(SkillEnum.Wrestle.ToString())))
                bothDownWorks = false;

            //  But we want to get down the ball carrier!
            //
            if (targetHasBall && blockerSkills.Contains(SkillEnum.Wrestle.ToString()))
                bothDownWorks = true;

            //  The defender has dodge
            //
            if (targetHasSkills && targetSkills.Contains(SkillEnum.Dodge.ToString()))
            {
                stumbleWorks = false;

                //  But we have tackle!
                //
                if (blockerHasSkills && blockerSkills.Contains(SkillEnum.Tackle.ToString()))
                    stumbleWorks = true;
            }

            //  We want to strip ball
            //
            if (targetHasBall && blockerHasSkills && blockerSkills.Contains(SkillEnum.StripBall.ToString()))
            {
                pushWorks = true;
                stumbleWorks = true;

                //  But the defender has sure hands
                //
                if (targetHasSkills && targetSkills.Contains(SkillEnum.SureHands.ToString()))
                {
                    pushWorks = false;
                    if (targetHasSkills && targetSkills.Contains(SkillEnum.Dodge.ToString()))
                    {
                        stumbleWorks = false;
                        if (blockerHasSkills && blockerSkills.Contains(SkillEnum.Tackle.ToString()))
                            stumbleWorks = true;
                    }
                }

                //  We also have juggernaut (if relevant)
                //
                if (pushWorks && blockerSkills.Contains(SkillEnum.Juggernaut.ToString()))
                    bothDownWorks = true;
            }

            //  View the outcome of the dice
            //
            switch (roll)
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
        /// Get if the rolled block was a failure or not (most probably a turnover)
        /// </summary>
        /// <param name="roll">Actually rolled dice</param>
        /// <param name="blockerSkills">Attacking player skill list</param>
        /// <param name="blockerHasBall">TRUE if the attacker is also the ball carrier</param>
        /// <returns>TRUE if the blocking action ended in a failure</returns>
        public static bool IsBlockFailure(BlockDice roll, List<string> blockerSkills, bool blockerHasBall = false)
        {
            bool blockerHasSkills = blockerSkills != null;
            bool blockWorks = false;
            bool isBlockFailure = false;

            //  We have block! (or wrestle and not carrying the ball) (or juggernaut)
            //
            if (blockerHasSkills && (blockerSkills.Contains(SkillEnum.Block.ToString()) ||
                                    (blockerSkills.Contains(SkillEnum.Wrestle.ToString()) && !blockerHasBall) ||
                                     blockerSkills.Contains(SkillEnum.Juggernaut.ToString())))
                blockWorks = true;

            //  View the outcome of the dice
            //
            switch (roll)
            {
                case (BlockDice.Skull):
                    isBlockFailure = true;
                    break;
                case (BlockDice.BothDown):
                    isBlockFailure = !blockWorks;
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
        /// <param name="roll">Actualy rolled dice result</param>
        /// <param name="target">Target value to succed</param>
        /// <returns>TRUE if roll was succesful</returns>
        public static bool IsRoll1DSuccessful(int roll, int target)
        {
            return roll == 6 || (roll != 1 && roll >= target);
        }

        /// <summary>
        /// Get if rolled 2D6 was successful
        /// </summary>
        /// <param name="roll">Actualy rolled dice result</param>
        /// <param name="target">Target value to succed</param>
        /// <returns>TRUE if roll was succesful</returns>
        public static bool IsRoll2DSuccessful(int roll, int target)
        {
            return (roll >= target);
        }
        */
    }
}
