using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventGameFinished
	{
		public MatchResult MatchResult{ get; set; }

		public RulesEventGameFinished ()
		{
		}
	}
}

