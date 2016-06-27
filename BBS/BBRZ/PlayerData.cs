using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class PlayerData
	{
		public string LobbyId { get; set; }
		public int Ma { get; set; }
		public string Name {get;set;}
		public int Ag {get;set;}
		public int Level {get;set;}
		public int Number {get;set;}
		public int Experience{get;set;}
		public int IdHead{ get; set; }
		public int Av{ get; set; }
		public int St{get;set;}
		public string ListSkills { get; set; }
		public int Id{get;set;}
		public int IdPlayerTypes{get;set;}
		public int Contract{ get; set; }

		public PlayerData ()
		{
            Level = 1;
		}
	}
}

