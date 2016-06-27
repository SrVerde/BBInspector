using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventAddMercenary
	{
		public int InducementsCash{get;set;}
		public int MercenaryId { get; set; }
		public int MercenaryType{ get; set; }
		public int InducementCategory{get;set;}
		public int Treasury{get;set;}

		public RulesEventAddMercenary ()
		{
		}
	}
}

