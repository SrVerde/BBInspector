using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventKickOffTable
	{
		public string ListDice{get;set;}
		public int Event{get;set;}
		
        /*
        I do not know how to handle this:
        public StringMessage StringMessage{get;set;}
        */

		public RulesEventKickOffTable ()
		{
		}
	}
}

