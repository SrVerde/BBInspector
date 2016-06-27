using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class GameInfos
	{
		public int AnswerClock { get; set; }
		public int ServerReconnectionAllowedTime {get;set;}
		public int ClientDelayDetectionPingIntervall{ get; set; }
		public int TurnClockDuration { get; set; }
		public List<CoachInfos> CoachesInfos { get; set; }
		public string Id { get; set; }
		public int LevelCabalVision {get;set;}
		public int Clock {get;set;}
		public int LocalCoachSlot {get;set;}
		public int State { get; set; }
		public string Stadium { get; set; }
		public int TurnClock {get;set;}
		public int ServerDelayDetectionThreshold {get;set;}
		public int ClientReconnectionIntervall {get;set;}
		public string NameStadium { get; set; }
		public int LevelStadium { get; set; }
		public RowLeague RowLeague { get; set; }
		public RowCompetition RowCompetition { get; set; }

		public GameInfos ()
		{
		}
	}
}

