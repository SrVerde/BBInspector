using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class SetupState
	{
		public int ScrimmageValid{get;set;}
		public int BotWideZoneValid{get;set;}
		public int PlayersNumberValid{get;set;}
		public int TopWideZoneValid{get;set;}

		public SetupState ()
		{
		}
	}
}

