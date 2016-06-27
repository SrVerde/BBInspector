using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventInducementsInfos
	{
		public List<TeamInducements> TeamInducements{ get; set; }
		public int InducementsCash { get; set; }

		public RulesEventInducementsInfos ()
		{
		}
	}
}

