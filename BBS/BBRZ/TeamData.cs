using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class TeamData
	{
		public string LobbyId{ get; set; }
		public int Value{ get; set; }
		public int Cheerleaders{get;set;}
		public int TreasuryBeforeInducements{get;set;}
		public string Name{get;set;}
		public int Popularity{ get; set; }
		public int Apothecary{get;set;}
		public int IdRace{get;set;}
		public int Treasury{get;set;}
		public int Reroll{get;set;}
		public string Logo{ get; set; }
		public string TeamColor { get; set; }

		public TeamData ()
		{
		}
	}
}

