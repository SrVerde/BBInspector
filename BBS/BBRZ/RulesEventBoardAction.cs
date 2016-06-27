using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventBoardAction
	{
		public int PlayerId { get; set; }
		public int RequestType{ get; set; }
		public int ActionType{get;set;}
		public Order Order {get;set;} 
		public Results Results { get; set; }

		public RulesEventBoardAction ()
		{
		}
	}
}

