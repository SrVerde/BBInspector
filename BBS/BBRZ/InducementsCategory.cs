using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class InducementsCategory
	{
		public int Max{ get; set; }
		public int Cost {get;set;}
		public int Type { get; set; }
		public List<PlayerData> Players{ get; set; }

		public InducementsCategory ()
		{
		}
	}
}

