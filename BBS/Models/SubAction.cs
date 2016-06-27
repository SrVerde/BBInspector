using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class SubAction
    {
        public SubAction(ActionType subActionType, Player player, Player targetPlayer = null)
        {
            Type = subActionType;
            Player = player;
            TargetPlayer = targetPlayer;
            Rolls = new List<Roll>();
        }

        public SubAction() : base()
        {
        }

        public ActionType Type
        {
            get; private set;

        }

        /// <summary>
        /// The player performing the action
        /// </summary>
        public Player Player { get; protected set; }
        /// <summary>
        /// The player target of the action 
        /// </summary>
        public Player TargetPlayer { get; protected set; }

        public List<Roll> Rolls
        {
            get; private set;
        }

        public RollResult Rollresult { get; protected set; }

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
