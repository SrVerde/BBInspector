using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class CellTo
	{
		public Location Cell {get;set;}

		public CellTo ()
		{
		}

        public override string ToString()
        {
            return Cell.ToString();
        }
    }
}

