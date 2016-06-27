using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BBS.BBRZ
{
	[Serializable]
	public class Results
	{
        [XmlElement]
		public List<BoardActionResult> BoardActionResult{get;set;}

		public Results ()
		{
		}
	}
}

