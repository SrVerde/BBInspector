using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public abstract class ActionBase<T> where T: Chance
    {
        public ActionBase(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// The player performing the action
        /// </summary>
        public Player Player { get; protected set; }
        /// <summary>
        /// The action being performed
        /// </summary>
        public abstract ActionType Type { get; }

        /// <summary>
        /// Probabilities of the action 
        /// </summary>
        public abstract T Chances { get; }

     

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
