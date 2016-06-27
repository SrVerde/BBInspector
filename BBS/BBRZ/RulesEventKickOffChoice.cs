using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventKickOffChoice
	{
		public int ChosingTeam{ get; set; }
		public int KickOffTeam{get;set;}

		public RulesEventKickOffChoice ()
		{
		}
	}
}

