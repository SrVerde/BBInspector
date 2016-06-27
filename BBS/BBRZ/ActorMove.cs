using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class ActorMove
	{
		public Location Cell {get;set;}
		public Location Destination {get;set;}
		public ActorMove ()
		{
		}
	}
}

