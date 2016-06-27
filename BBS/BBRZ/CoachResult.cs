using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class CoachResult
	{
		public TeamResult TeamResult{ get; set; }
		public int IdCoach {get;set;}
		public int TotalTimeDelayed{get;set;}
		public int NbDelayTriggered{ get; set; }
		public int TotalTimeDisconnected{get;set;}
		public string IpAddress{ get; set; }
		public int NbDisconnects{get;set;}

		public CoachResult ()
		{
		}
	}
}

