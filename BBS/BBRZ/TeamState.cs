using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class TeamState
	{
		public int PassAvailable { get; set; }
		public int KickOffTurn{get;set;}
		public int FoulAvailable{get;set;}
		public int BlitzerId{get;set;}
		public int GameTurn {get;set;}
		public int WizardEndsTurn{get;set;}
		public int Babes { get; set; }
		public int Wizard {get;set;}
		public int InducementsTurn{get;set;}
		public int TeamRerollAvailable{get;set;}
		public int RerollNumber{get;set;}
		public int Bribes {get;set;}
		public List<PlayerState> ListPitchPlayers{get;set;}
		public TeamData Data{get;set;}
		public int NbSupporters { get; set; }
		public int ApothecaryNumber { get; set; }
		public int ApothecaryAvailable{get;set;}
		public int HandOffAvailable{ get; set; }

		public TeamState ()
		{
		}
	}
}

