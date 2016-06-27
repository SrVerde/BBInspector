using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RowCompetition
	{
		public Id IdPreviousEdition { get; set; }
		public int IdOwner { get; set; }
		public Id Id {get;set;}
		public int LeaguePhase {get;set;}
		public Id IdLeague  {get;set;}
		public int PrematchEvents { get; set; }
		public int IdCompetitionTypes { get; set; }
		public int RegistrationMode{get;set;}
		public int NbRounds{get;set;}
		public int NbTeamsMax{ get; set; }
		public int CompetitionStatus{get;set;}
		public int CurrentRound{get;set;}
		public string Name { get; set; }
		public int LeagueEdition{get;set;}
		public int NbRegisteredTeams{get;set;}
		public int AcceptTicketRequest{get;set;}
		public int TurnDuration{get;set;}

		public RowCompetition ()
		{
		}
	}
}

