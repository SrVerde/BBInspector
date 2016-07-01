using System;
using System.Collections.Generic;

namespace BBS.Models
{
    /// <summary>
    /// A subaction is a part of the action that may require one or more rolls
    /// </summary>
    public class SubAction : ActionBase<SubActionChance>
    {
        public SubAction(ActionType subActionType, Player player, Player targetPlayer = null) : base(player)
        {
            _actionType = subActionType;
            TargetPlayer = targetPlayer;
            Rolls = new List<Roll>();
        }

        ActionType _actionType;

        /// <summary>
        /// The sub-action type
        /// </summary>
        public override ActionType Type { get { return _actionType; } }
        /// <summary>
        /// The player target of the action 
        /// </summary>
        public Player TargetPlayer { get; protected set; }
        /// <summary>
        /// List of involved rolls
        /// </summary>
        public List<Roll> Rolls { get; protected set; }
        /// <summary>
        /// TRUE if a team reroll is available at this point of the action
        /// </summary>
        public bool IsTeamReRollAvailbale { get; protected set; }
        /// <summary>
        /// TRUE if apothecary is available at this point of the action
        /// </summary>
        public bool IsApothecaryAvailable { get; protected set; }
        /// <summary>
        /// TRUE if a bribe is available at this point of the action
        /// </summary>
        public bool IsBribeAvailable { get; protected set; }
        /// <summary>
        /// Chances of finishing this sub-action
        /// </summary>
        public override SubActionChance Chances { get; }

        private CompleteRollChance GetRollChance(Roll roll, bool canUseReRoll = false, bool canUseSkill = false)
        {
            bool hasTheSkill = roll.PlayerHasTheRightSkill(Player) && canUseSkill;
            bool isLoner = Player.Skills.Contains(SkillEnum.Loner);
            bool hasPro = Player.Skills.Contains(SkillEnum.Pro);

            CompleteRollChance rollChance;
            SingleRollChance singleRollChance = roll.GetChances(Player, TargetPlayer);

            double s = singleRollChance.Success;
            double f = singleRollChance.Failure;
            double l = (new Roll("4", 4, (int)RollType.LonerRoll, null)).GetChances(Player, TargetPlayer).Success;
            double p = (new Roll("4", 4, (int)RollType.ProRoll, null)).GetChances(Player, TargetPlayer).Success;

            if (!hasPro && !canUseReRoll && !hasTheSkill)
                rollChance = new CompleteRollChance(s, 
                                                    f);
            else if (!hasPro && canUseReRoll && !hasTheSkill && isLoner)
                rollChance = new CompleteRollChance(s + f * l * s,
                                                    f * (1.0 - l) + f * l * f);
            else if (hasPro && !canUseReRoll && !hasTheSkill)
                rollChance = new CompleteRollChance(s + f * p * s,
                                                    f * (1.0 - p) + f * p * f);
            else if (hasPro && canUseReRoll && !hasTheSkill && !isLoner)
                rollChance = new CompleteRollChance(s + f * p * s + f * (1.0 - p) * p * s,
                                                    f * (1.0 - p) * (1.0 - p) + f * p * f);
            else if (hasPro && canUseReRoll && !hasTheSkill && isLoner)
                rollChance = new CompleteRollChance(s + f * p * s + f * (1.0 - p) * l * p * s,
                                                    f * (1.0 - p) * (1.0 - l) + f * (1.0 - p) * l * (1.0 - p) + f * (1.0 - p) * l * p * f + f * p * f);
            else
                rollChance = new CompleteRollChance(s + f * s, 
                                                    f * f);

            if (hasTheSkill)
            {
                rollChance.ProbabilityUsingSkill = f;
                rollChance.ProbabilityUsingReroll = 0.0;
            }
            else if (hasPro && canUseReRoll)
            {
                rollChance.ProbabilityUsingSkill = 0.0;
                rollChance.ProbabilityUsingReroll = f * (1.0 - p);
            }
            else if (canUseReRoll)
            {
                rollChance.ProbabilityUsingSkill = 0.0;
                rollChance.ProbabilityUsingReroll = f;
            }

            if (hasPro && !hasTheSkill)
                rollChance.SuccessAtFirstAttempt = s + f * p * s;
            else
                rollChance.SuccessAtFirstAttempt = s;

            return rollChance;
        }

        internal void AddRoll(Roll roll)
        {
            Rolls.Add(roll);
        }

        private object GetRollString()
        {
            return String.Join(",", Rolls);
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Type.ToString(), GetRollString());
        }
    }
}
