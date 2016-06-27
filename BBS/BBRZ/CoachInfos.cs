using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class CoachInfos
	{
		public string Login { get; set; }
		public string UserId { get; set; }
		public int? Slot { get; set; }

		public CoachInfos ()
		{
		}
	}
}

