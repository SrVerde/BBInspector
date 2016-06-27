using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventAddInducement
	{
		public int InducementsCash{ get; set; }
		public int CoachSlot{get;set;}
		public int InducementCategory{get;set;}
		public int Treasury{ get; set; }

		public RulesEventAddInducement ()
		{
		}
	}
}

