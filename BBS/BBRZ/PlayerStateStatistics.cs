using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class PlayerStateStatistics
	{
		public int InflictedPasses{ get; set; }
		public int InflictedMetersPassing {get;set;}
		public int InflictedTackles{get;set;}
		public int SustainedTackles{ get; set; }
		public int SustainedInjuries{get;set;}
		public int InflictedMetersRunning{ get; set; }
		public int MatchPlayed{get;set;}
		public int MVP{get;set;}
		public int IdPlayerListing{get;set;}
		public int InflictedInjuries{get;set;}
		public int InflictedCatches {get;set;}
		public int InflictedInterceptions { get; set; }
		public int InflictedTouchdowns{ get; set; }
		public int SustainedKO{get;set;}
		public int InflictedKO{ get; set; }
		public int InflictedCasualties{get;set;}
		public int SustainedCasualties{get;set;}
		public int SustainedDead{ get; set;}
		public int InflictedDead { get; set; }
		public int SustainedStun{ get; set;}
		public int InflictedStun { get; set; }

		public PlayerStateStatistics ()
		{
		}
	}
}

