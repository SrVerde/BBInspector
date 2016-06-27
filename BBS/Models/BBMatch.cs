using BBS.BBRZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    class BBMatch
    {
        private Replay _rep;
        public BBMatch(Replay rep)
        {
            this._rep = rep;
            BuildMatch();
        }

        public string Name
        {
            get { return String.Format("{0} vs {1}",TeamLocal.Name, TeamVisit.Name); }
        }

        public Team TeamLocal
        {
            get; private set;
        }

        public Team TeamVisit
        {
            get; set;
        }

        private void BuildMatch()
        {
            var team0 = _rep.FirstStep.BoardState.ListTeams[0];
            var team1 = _rep.FirstStep.BoardState.ListTeams[1];

            var replaySteps = _rep.ValidSteps();

            TeamLocal = new Team(0, team0.Data.Name);
            TeamVisit = new Team(1, team1.Data.Name);

            int currentTeam = 0;

            foreach (var rp in replaySteps)
            {
                if (rp.RulesEventEndTurn != null)
                    currentTeam = rp.RulesEventEndTurn.PlayingTeam;

                if (currentTeam == 0)
                    TeamLocal.AddReplay(rp);
                else
                    TeamVisit.AddReplay(rp);

            }
        }
    }
}
