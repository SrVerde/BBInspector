using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBS.BBRZ;


namespace BBS.Models
{
    public class Action: ActionBase
    {
        /// <summary>
        /// Action constructor
        /// </summary>
        /// <param name="player">The player performing the action</param>
        public Action(Player player)
            :base(player)
        {
            Player = player;
            SubActions = new List<SubAction>();
        }

       
        /// <summary>
        /// The action being performed
        /// </summary>
        public override  ActionType Type
        {
            get { return CalculateActionType(); }
        }

        private ActionType CalculateActionType()
        {
            ActionType current = ActionType.Unknown;

            var sb = SubActions.Find(sa => sa.Type == ActionType.Blitz);

            if (sb != null)
                current = sb.Type;
            else
            {
                //sacamos la mas importante en principio si es blitz nos quedamos con ella ignorando el block y el move.
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

        public override RollResult RollResult
        {
            get
            {
                return new RollResult();
            }
        }

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
        
       

    }

    public enum ActionType
    {
        Unknown = -1,
        Move = 0,
        /// <summary>
        /// The following two actions seems to be different. I think it has to do with animations
        /// </summary>          
        Block = 1,
        Blitz = 2,
        /// <summary>
        /// Seems to be throwing the ball
        /// </summary>
        ThrowBall = 3,
        /// <summary>
        /// Seems to be passing the ball from a player to an adjacent palyer
        /// </summary>       
        HandOff = 4,
        /// <summary>
        /// Seems to be fouling a prone player
        /// </summary>
        Foul = 5, 
        /// <summary>
        /// Seems to be putting the player prone
        /// </summary>
        Prone = 6,
        /// <summary>
        /// Seems to be selecting the kicking player
        /// </summary>
        SelectForKickOff = 7,
        /// <summary>
        /// Seems to be bouncing or scattering the ball
        /// </summary>       
        MoveBall = 8,
        /// <summary>
        /// Seems to be attaching the ball to a player when bounces
        /// </summary>    
        Catch = 9,
        /// <summary>
        /// Seems to be scoring a touchdown (standing up in TD zone with ball)
        /// </summary>  
        TouchDown = 10,
        /// <summary>
        /// Seems to be turning face down players to face up
        /// </summary> 
        TurnProneFaceUp = 11,
        /// <summary>
        /// Seems to be moving KO's to reserves
        /// </summary>
        PlayerInBench = 12,
        unknown13 = 13, 
        /// <summary>
        /// Seems to be attaching the ball to a player when it is on the ground (the ball)
        /// </summary>        
        PickUp = 14,
        /// <summary>
        /// Seems to be trying to act (really stupid, wild animal, bone head...)
        /// </summary>
        TryingToAct = 15, 
        unknown16 = 16,
        unknown17 = 17,
        /// <summary>
        /// Seems to be pursuing a player with shadow
        /// </summary>
        Shadow = 18, 
        /// <summary>
        /// Seems to be stabbing with a knife
        /// </summary>
        Stab = 19, 
        unknown20 = 20,
        unknown21 = 21,
        unknown22 = 22,
        unknown23 = 23,
        unknown24 = 24,
        unknown25 = 25,
        unknown26 = 26,
        unknown27 = 27,
        unknown28 = 28,
        unknown29 = 29,
        unknown30 = 30,
        unknown31 = 31,
        unknown32 = 32,
        unknown33 = 33,
        unknown34 = 34,
        unknown35 = 35,
        unknown36 = 36,
        unknown37 = 37,
        unknown38 = 38,
        unknown39 = 39,
        unknown40 = 40,
        unknown41 = 41,
        /// <summary>
        /// Seems to be the staring point of each action. That is, selecting the player (it waves his hand in the animation)
        /// </summary>
        SelectPlayer = 42, 
    }
}
