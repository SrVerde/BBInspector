using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class RulesEventSetUpConfiguration
	{
		public SetupState SetupState{ get; set; }
		public List<PlayerPosition> ListPlayersPositions { get; set; }

		public RulesEventSetUpConfiguration ()
		{
		}
	}
}

