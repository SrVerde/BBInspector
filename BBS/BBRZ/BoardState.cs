using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class BoardState
	{
		public BallData Ball { get; set; }
		public BoardStateStatistics Statistics{ get; set; }
		public List<TeamState> ListTeams { get; set; }
		public BoardState ()
		{
		}
	}
}

