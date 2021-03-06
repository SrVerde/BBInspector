﻿using System;
using System.Collections.Generic;
using BBS.BBRZ;

namespace BBS.Models
{
    public class Turn
    {
        public Turn(int turn, Team team)
        {

            this.Number = turn;
            this.Team = team;
            Actions = new List<Action>();
        }

        public Team Team
        {
            get; private set;
        }


        public List<Action> Actions
        {
            get; private set;
        }


        public int Number
        {
            get; private set;
        }

        internal void AddActions(ReplayStep step)
        {
            int targetPlayerId = -1;
            Dictionary<string, int> positionsMap = CreatePositionsMap(step);
            Action action = null;
            foreach (RulesEventBoardAction ba in step.RulesEventBoardAction)
            {
                ActionType ac = (ActionType)ba.ActionType;
              
                Player player = SearchPlayer(step.BoardState, ba.PlayerId);

                if (ac == ActionType.SelectPlayer)
                {
                    action = new Action(player);
                    this.Actions.Add(action);
                }
                else
                {
                    //there is an action that involves a player.
                    if (action != null)
                    {
                        positionsMap.TryGetValue(ba.Order.CellTo.Cell.ToString(), out targetPlayerId);

                        Player targetPlayer = SearchPlayer(step.BoardState, targetPlayerId);


                        action.AddSubActions((ActionType)ba.ActionType, ba.Results.BoardActionResult, targetPlayer);
                    }
                }
               
            }
        }

        private Player SearchPlayer(BoardState boardState, int playerId)
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (PlayerState ps in boardState.ListTeams[i].ListPitchPlayers)
                {
                    if (ps.Data.Id == playerId)
                    {
                        return new Player(ps);
                    }
                }
            }

            return null;
        }

        private Dictionary<string, int> CreatePositionsMap(ReplayStep step)
        {
            Dictionary<string, int> positionsMap = new Dictionary<string, int>();

            if (step.BoardState != null && step.BoardState.ListTeams != null)
            {
                foreach (TeamState teamState in step.BoardState.ListTeams)
                {
                    if (teamState.ListPitchPlayers != null)
                    {
                        foreach (PlayerState playerState in teamState.ListPitchPlayers)
                        {
                            if (!positionsMap.ContainsKey(playerState.Cell.ToString()))
                                positionsMap.Add(playerState.Cell.ToString(), playerState.Id);
                        }
                    }
                }
            }

            return positionsMap;
        }



        public override string ToString()
        {
            return String.Format(" Turn: {0}", Number);
        }
    }
}
