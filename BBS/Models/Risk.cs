using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class Risk
    {
        public Risk()
        { }

        private static int[] _numDoubles2D6 = new int[] { 6, 6, 6, 5, 5, 4, 4, 3, 3, 2, 2, 1, 1, 0, 0 };

        /*
        /// <summary>
        /// Chances in failing in a block action from 0.0 to 100.0 [%] and producing a turnover
        /// </summary>
        /// <param name="dices">Number of dices rolled. Negative if attacker is weaker than defender</param>
        /// <param name="blockerSkills">Blocking player skill list</param>
        /// <param name="canSpareReRoll">TRUE if the attacker can spare a reroll</param>
        /// <param name="blockerHasBall">TRUE if the attacker is also the ball carrier</param>
        /// <returns>Turnover probability from 0.0 to 1.0</returns>
        public static double GetBlockRisk(int dices, List<string> blockerSkills, bool canSpareReRoll = true, bool blockerHasBall = false)
        {
            bool blockerHasSkills = blockerSkills != null;
            bool blockWorks = false;

            //  We have block! (or wrestle and not carrying the ball) (or juggernaut)
            //
            if (blockerHasSkills && (blockerSkills.Contains(SkillEnum.Block.ToString()) ||
                                    (blockerSkills.Contains(SkillEnum.Wrestle.ToString()) && !blockerHasBall) ||
                                     blockerSkills.Contains(SkillEnum.Juggernaut.ToString())))
                blockWorks = true;

            //  Number of results in a single block dice to produce a turnover
            //  (starts with the SKULL and BOTH DOWN and removes depending in skills)
            //
            int numInvalidResults = 2;
            if (blockWorks)
                numInvalidResults--;

            //  Total combinations depending on number of dice rolled
            //
            double numTotalCombinations = Math.Pow(6, Math.Abs(dices));

            //  Chances are valid combinations divided by total combinations
            //  As a curiosity, if 0 dice are rolled, chances to get a turnover are 0.0 %
            //
            double chances = (dices > 0) ?
                             Math.Pow(numInvalidResults, dices) / numTotalCombinations :
                             (1.0 - Math.Pow(6 - numInvalidResults, -dices) / numTotalCombinations);

            //  If a re-roll can be spared, then the risk is calculated by failing two consecutive times
            //
            return ((canSpareReRoll) ? chances * chances : chances);
        }

        /// <summary>
        /// Get the risk of rolling one D6 (rolling lower than desired) and producing a turnover
        /// </summary>
        /// <param name="required">Minimum target number required to succed</param>
        /// <param name="canSpareReRoll">TRUE if a reroll (either team or skill) can be spared</param>
        /// <returns>Probability of failing the roll (most probably causing a turnover) from 0.0 to 1.0</returns>
        public static double GetRoll1DRisk(int required, bool canSpareReRoll = true)
        {
            //  Chances are valid combinations divided by total combinations
            //  A rolled 6 is allways a success
            //  A rolled 1 is allways a failure
            //
            double chances = (required <= 2) ? 1.0 / 6.0 : (required >= 6) ? 5.0 / 6.0 : (required - 1.0) / 6.0;

            //  If a re-roll can be spared, then the risk is calculated by failing two consecutive times
            //
            return ((canSpareReRoll) ? chances * chances : chances);
        }

        /// <summary>
        /// Get the risk of rolling 2D6 (rolling lower than desired). to my knowledge, these actions do not cause turnovers
        /// </summary>
        /// <param name="required">Minimum target number required to succed</param>
        /// <returns>Probability of failing the roll (most probably causing a turnover) from 0.0 to 1.0</returns>
        public static double GetRoll2DRisk(int required)
        {
            return 1.0 - Luck.GetRoll2DChance(required);
        }

        /// <summary>
        /// Get the risk of fouling (rolling doubles in an armour roll)
        /// </summary>
        /// <param name="required">Minimum target number required to succed</param>
        /// <returns>Probability of failing the roll (causing a turnover) from 0.0 to 1.0</returns>
        public static double GetFoulRisk(int required, bool isDirtyPlayer)
        {
            if (!isDirtyPlayer) return 6.0 / 36.0;

            if (required < 0)
                required = 0;
            else if (required >= _numDoubles2D6.Length - 1)
                required = _numDoubles2D6.Length - 1;

            return _numDoubles2D6[required] / 36.0;
        }
        */
    }
}
