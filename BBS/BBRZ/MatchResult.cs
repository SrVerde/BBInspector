using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class MatchResult
	{
		public string IdMatchForSelfValidation {get;set;}
		public int UseLobbyTeams{get;set;}
		public string Session{get;set;}
		public List<CoachResult> CoachResults { get; set; }
		public MatchResultsRow Row {get;set;}
		public MatchResult ()
		{
		}
	}
}

