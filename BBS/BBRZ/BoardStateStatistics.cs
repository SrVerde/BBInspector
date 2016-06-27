using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class BoardStateStatistics
	{
		public int IdCoachHome { get; set; }	
		public object ShardedIdTeamListingAway{get;set;}
		public int IdCoachAway{get;set;}
		public object ShardedIdTeamListingHome{ get; set; }

		public BoardStateStatistics ()
		{
		}
	}
}

