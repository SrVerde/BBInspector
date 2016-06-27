using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class PlayerState : ActorMove
	{
		public PlayerStateStatistics Statistics { get; set; }
		public int CanBlock { get; set; }
		public int TackleZone { get; set; }
		public int CanAct { get; set; }
		public int Id {get;set;}
		public int CanDeclareBlitz {get;set;}
		public int Gfi {get;set;}
		public int MovePoint {get;set;}
		public PlayerData Data {get;set;}

		public PlayerState ()
		{
		}
	}
}

