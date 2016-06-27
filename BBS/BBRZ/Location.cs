using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class Location
	{
		public int y { get; set; }
		public int x {get;set;}

		public Location ()
		{
		}

        public override string ToString()
        {
            return x + "," + y;
        }
	}

	[Serializable]
	public class Cell
	{
		public int y { get; set; }
		public int x {get;set;}

		public Cell ()
		{
		}
	}
}

