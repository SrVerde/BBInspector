using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBS.BBRZ;

namespace BBS.Models
{
    public class Team
    {
        public Team(int id, string name)
        {
            this.Name = name;
            this.TeamId = id;
            Turns = new List<Turn>();

        }

        public string Name
        {
            get; private set;
        }

        public int TeamId
        {
            get; private set;
        }

        public List<Turn> Turns
        {
            get; private set;
        }


        internal void AddReplay(ReplayStep rp)
        {
            if (rp.RulesEventBoardAction.Count > 0)
            {
                int turn = rp.BoardState.ListTeams[TeamId].GameTurn;

           

                Turn current = null;
                if (!Turns.Exists(t => t.Number == turn))
                {
                    current = new Turn(turn, this);
                    Turns.Add(current);
                }
                else
                    current = Turns.Find(t => t.Number == turn);



                current.AddActions(rp);
            }
        }

        /// <summary>
        /// Gets all rolls for a team.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRollInfo> GetAllRolls()
        {
            foreach (var t in Turns)
            {
                foreach (var ac in t.Actions)
                    foreach (var r in ac.GetAllRolls())
                        yield return r;
            }
        }
    }

}
