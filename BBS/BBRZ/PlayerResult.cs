using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class PlayerResult
	{
		public PlayerStateStatistics Statistics {get;set;}
		public int Xp{ get; set; }
		public PlayerData PlayerData { get; set; }

		public PlayerResult ()
		{
		}
	}
}

