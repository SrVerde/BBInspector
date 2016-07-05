using System;
using System.Collections.Generic;
using System.Linq;
using BBS.BBRZ;


namespace BBS.Models
{
    /// <summary>
    /// An action is a goup of subactions performed by a player
    /// The action is defined in the BBRZ file since the player is selected until another player is selected
    /// </summary>
    public class Action : ActionBase<ActionChance>
    {
        /// <summary>
        /// Action constructor
        /// </summary>
        /// <param name="player">The player performing the action</param>
        public Action(Player player) : base(player)
        {
            Player = player;
            SubActions = new List<SubAction>();
        }

        /// <summary>
        /// The action being performed, in an action, will be the most important subaction
        /// </summary>
        public override ActionType Type { get { return CalculateActionType(); } }

        private ActionType CalculateActionType()
        {
            ClearSubActions();

            ActionType current = ActionType.Unknown;

            var sb = SubActions.Find(sa => sa.Type == ActionType.Blitz);

            if (sb != null)
                current = sb.Type;
            else
            {
              
                foreach (var sa in SubActions)
                {
                    if ((sa.Type != ActionType.Prone) && (sa.Type != ActionType.SelectPlayer))
                    {
                        current = sa.Type;
                    }
                }

                if (current == ActionType.Unknown)
                    current = SubActions.First().Type;
            }

            return current;
        }

        public override ActionChance Chances { get; }

        public List<SubAction> SubActions
        {
            get; private set;
        }
        
        public override string ToString()
        {
            
            return String.Format("{0} - {1}", Player.Name, Type);
        }

        internal void AddSubActions(ActionType subActionType, List<BoardActionResult> boardActionResult, Player targetPlayer = null)
        {
            var sa = new SubAction(subActionType, Player, targetPlayer);

            foreach (BoardActionResult bar in boardActionResult)
            {
                List<Modifier> thisRollModifiers = new List<Modifier>();
                foreach (DiceModifier dm in bar.ListModifiers)
                {
                    Modifier thisModifier = new Modifier(dm);
                    thisRollModifiers.Add(thisModifier);

                }
                Roll roll = new Roll(bar.CoachChoices.ListDices, bar.Requirement, bar.RollType, thisRollModifiers);

                sa.AddRoll(roll);
            }            
            this.SubActions.Add(sa);
        }


        private void ClearSubActions()
        {
            
            for(int i =SubActions.Count-1; i >= 0; i--)
            {
                var sa = SubActions[i];
                if (sa.Rolls.Count == 1 && sa.Rolls[0].isNoRoll)
                {
                    if (SubActions.Count > 1)
                        SubActions.RemoveAt(i);
                }
            }
        }


        public IEnumerable<IRollInfo> GetAllRolls()
        {
            foreach (var sa in SubActions)
                foreach (var r in sa.Rolls)
                    yield return r;
        }
    }

      
}
