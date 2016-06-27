using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BBS.BBRZ
{
	[Serializable]
	public class ReplayStep
	{
		public GameInfos GameInfos { get; set; }
		public RulesEventInducementsInfos RulesEventInducementsInfos {get;set;}
		public RulesEventAddInducement RulesEventAddInducement { get; set; }
		public RulesEventApplyInducements RulesEventApplyInducements{ get; set; }
		public RulesEventKickOffChoice RulesEventKickOffChoice {get;set;}
		public RulesEventKickOffTable RulesEventKickOffTable{get;set;}
		public RulesEventEndTurn RulesEventEndTurn{ get; set; }
		public RulesEventSetUpAction RulesEventSetUpAction{ get; set; }
		public RulesEventSetUpConfiguration RulesEventSetUpConfiguration{ get; set; }		
        [XmlElement]
        public List<RulesEventBoardAction> RulesEventBoardAction{get;set;}		
        public RulesEventCoachChoice RulesEventCoachChoice {get;set;}
		public RulesEventAddMercenary RulesEventAddMercenary{get;set;}
		public RulesEventWaitingRequest RulesEventWaitingRequest { get; set; }
		public RulesEventGameFinished RulesEventGameFinished{ get; set; }
		public BoardState BoardState { get; set; }

		public ReplayStep ()
		{
		}
	}
}

