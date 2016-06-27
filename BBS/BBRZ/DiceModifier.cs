using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class DiceModifier
	{
		Location Cell {get;set;}
		public int Skill {get;set;}
		public int Type { get; set; }
		public int Value { get; set; }

		public DiceModifier ()
		{
		}
	}
}

