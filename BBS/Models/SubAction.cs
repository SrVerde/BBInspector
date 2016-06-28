using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class SubAction : ActionBase
    {
        ActionType _actionType;

        public SubAction(ActionType subActionType, Player player, Player targetPlayer = null)
            : base(player)
        {
            _actionType = subActionType;
            TargetPlayer = targetPlayer;
            Rolls = new List<Roll>();
        }

        public override ActionType Type
        {
            get { return _actionType; }

        }


        /// <summary>
        /// The player target of the action 
        /// </summary>
        public Player TargetPlayer { get; protected set; }

        public List<Roll> Rolls
        {
            get; private set;
        }

        public override RollResult RollResult
        {
            get
            {
                return Rolls[0].GetChances(Player,TargetPlayer);
            }
          
        }

       

        internal void AddRoll(Roll roll)
        {
            Rolls.Add(roll);
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Type.ToString(), GetRollString());
        }

        private object GetRollString()
        {
            return String.Join(",", Rolls);
        }
    }
}
