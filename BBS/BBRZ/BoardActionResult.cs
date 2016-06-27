using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class BoardActionResult
	{
		public int Requirement{get;set;}
		public List<DiceModifier> ListModifiers{get;set;}
		public int IsOrderCompleted { get; set; }
		public CoachChoices CoachChoices{get;set;}
		public int RollType{get;set;}
        public int RequestType { get; set; }
        public int ResultType { get; set; }

		public BoardActionResult ()
		{
		}
	}
}

