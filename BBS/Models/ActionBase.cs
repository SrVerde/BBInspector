using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public abstract class ActionBase
    {
        public ActionBase(Player player)
        {
            this.Player = player;
        }


        /// <summary>
        /// The player performing the action
        /// </summary>
        public Player Player { get; protected set; }
        /// <summary>
        /// The action being performed
        /// </summary>
        public abstract ActionType Type
        {
            get;
        }


        /// <summary>
        /// Probabilities of the action 
        /// </summary>
        public abstract RollResult RollResult
        {
            get;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
