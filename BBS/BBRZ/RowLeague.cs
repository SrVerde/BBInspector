using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RowLeague
	{
		public int Edition {get;set;}
		public int IdOwner { get; set; }
		public string Logo { get; set; }
		public Id Id {get;set;}
		public string Description {get;set;}
		public int Phase {get;set;}
		public string Name { get; set; }
		public int NbRegisteredTeams {get;set;}
	
		public RowLeague ()
		{
		}
	}
}

