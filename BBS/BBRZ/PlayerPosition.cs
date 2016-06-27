using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class PlayerPosition
	{
		public int PlayerId{get;set;}
		public Location Position{get;set;}

		public PlayerPosition ()
		{
		}
	}
}

