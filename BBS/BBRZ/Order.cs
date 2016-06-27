using System;

namespace BBS.BBRZ
{
	[Serializable]
	public class Order
	{
		public CellTo CellTo {get;set;}
        public Location CellFrom { get; set; }

		public Order ()
		{
		}
	}
}

