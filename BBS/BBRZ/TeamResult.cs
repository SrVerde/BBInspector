using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class TeamResult
	{
		public string LobbyId{get;set;}
		public int PopularityBeforeMatch{get;set;}
		public TeamData TeamData{get;set;}
		public int PopularityGain{ get; set; }
		public int NbSupporters{get;set;}
		public int CashBeforeMatch{ get; set; }
		public List<PlayerResult> PlayerResults{get;set;}
		public int CashEarnedBeforeConcession{get;set;}
		public int IdTeam{get;set;}
		public int WinningsDice{get;set;}
		public int CashEarned{ get; set; }

		public TeamResult ()
		{
		}
	}
}

