using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class CampaignSpecifics
	{
		public List<string> ListDangerousCells { get; set; } 
		public List<string> ListPoisonnedPlayers { get; set; } 

		public CampaignSpecifics ()
		{
		}
	}
}

