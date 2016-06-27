using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventSetUpAction
	{
		public Location PlayerPosition {get;set;}
		public Location NewPosition {get;set;}
		public int ValidAction { get; set; }
		public SetupState SetupState{get;set;}

		public RulesEventSetUpAction ()
		{
		}
	}
}

