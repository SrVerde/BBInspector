using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventEndTurn
	{
		public int PlayingTeam {get;set;}
		public int TouchdownScorer{get;set;}
		public int NewDrive{get;set;}
		public List<TeamInfo > ListTeamInfos { get; set; }

		public RulesEventEndTurn ()
		{
		}
	}
}

