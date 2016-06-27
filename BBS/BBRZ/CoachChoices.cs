using System;
using System.Collections.Generic;

namespace BBS.BBRZ
{
	[Serializable]
	public class CoachChoices
	{
		public List<Cell> ListCells { get; set; }
		public List<SkillInfo> ListSkills { get; set; }
		public string ListDices{get;set;}
		public int ConcernedTeam{ get; set; }
		public CoachChoices ()
		{
		}
	}
}

