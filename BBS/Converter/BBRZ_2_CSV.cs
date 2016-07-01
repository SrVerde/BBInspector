using System;
using System.Collections.Generic;
using System.IO;
using BBS.BBRZ;
using BBS.Models;

namespace BBS.Converter
{
    public static class BBRZ_2_CSV
    {
        /// <summary>
        /// Writes a BBRZ replay into a CSV for easyness of comprehension
        /// </summary>
        /// <param name="replay">Replay to read</param>
        /// <param name="bbrzFileName">BBRZ file name</param>
        public static void Write2Csv(Replay replay, string bbrzFileName)
        {
            //  Generate the CSV file
            //
            TextWriter tw = File.CreateText(bbrzFileName + ".rolls.csv");
            tw.Write("ClockTime;");
            tw.Write("Turn;");
            tw.Write("Team;");
            tw.Write("ReplayStepID;");
            tw.Write("ActionID;");
            tw.Write("ActionType;");
            tw.Write("Player;");
            tw.Write("StepID;");
            tw.Write("Modifiers;");
            tw.Write("SkillsUsed;");
            tw.Write("Roll;");
            tw.Write("bar.IsOrderCompleted;");
            tw.Write("bar.RequestType;");
            tw.Write("bar.ResultType;");
            tw.Write("ba.Order;");

            tw.WriteLine();
            Dictionary<int, string> idToNameMap = new Dictionary<int, string>();
            string thisPlayerName = "";
            int replayStepID = 0;
            int clockTime = 0;
            int currentTurn = 0;
            int currentTeam = 0;

            //  Scan all the replay steps
            //
            foreach (ReplayStep rep in replay.ReplayStep)
            {
                //  Only interested in the in-game replay steps (not pre and post game steps)
                //  Each replay step may contain several sub steps
                //  Each sub step is stored in a rules event board action
                //
                if (rep.RulesEventBoardAction == null)
                    continue;

                if (rep.RulesEventEndTurn != null)
                {
                    currentTeam = rep.RulesEventEndTurn.PlayingTeam;
                    currentTurn = rep.BoardState.ListTeams[currentTeam].GameTurn;
                }

                if (rep.GameInfos != null)
                    clockTime = rep.GameInfos.Clock;

                replayStepID++;
                int actionID = 0;

                foreach (RulesEventBoardAction ba in rep.RulesEventBoardAction)
                {
                    //  Start the string with values that do not change
                    //
                    string start = String.Format("{0};{1};{2};{3};{4};{5};", clockTime, currentTurn, rep.BoardState.ListTeams[currentTeam].Data.Name, replayStepID, ++actionID, (ActionType)ba.ActionType);

                    //  Search the player's name
                    //
                    if (idToNameMap.ContainsKey(ba.PlayerId))
                    {
                        //  We already have it in the map:
                        //
                        start += (idToNameMap[ba.PlayerId] + ";");
                    }
                    else
                    {
                        //  We have to look in the board state, inside the list of players in the pitch
                        //  Do not forget to scan both teams
                        //
                        for (int i = 0; i < 2; i++)
                        {
                            foreach (PlayerState ps in rep.BoardState.ListTeams[i].ListPitchPlayers)
                            {
                                //  Found the player!
                                //  Update the output and the map
                                //
                                if (ps.Data.Id == ba.PlayerId)
                                {
                                    start += (ps.Data.Name + ";");
                                    i = 2;

                                    if (!idToNameMap.ContainsKey(ps.Data.Id))
                                        idToNameMap.Add(ps.Data.Id, ps.Data.Name);

                                    break;
                                }
                            }
                        }
                    }

                    int stepID = 0;

                    //  Each rules board action may have several results
                    //  We need a line for each one
                    //  
                    foreach (BoardActionResult bar in ba.Results.BoardActionResult)
                    {
                        //  Write the action type and the name of the involved player
                        //
                        tw.Write(start + (++stepID).ToString() + ";");

                        //  Get roll modifiers
                        // 
                        bool hasModifier = false;
                        string modifierText = "";
                        List<Modifier> thisRollModifiers = new List<Modifier>();
                        foreach (DiceModifier dm in bar.ListModifiers)
                        {
                            Modifier thisModifier = new Modifier(dm);
                            thisRollModifiers.Add(thisModifier);
                            modifierText += (thisModifier.ToString() + ",");
                            hasModifier = true;
                        }

                        //  Write the modifiers
                        //
                        if (hasModifier)
                            tw.Write(String.Format("({0})", modifierText.Substring(0, modifierText.Length - 1)));
                        tw.Write(";");

                        //  Write used skills in the roll
                        //
                        bool hasUsedSkills = (bar.CoachChoices.ListSkills.Count > 0);
                        string skillsText = "";
                        foreach (SkillInfo ski in bar.CoachChoices.ListSkills)
                        {
                            //  Search the player's name
                            //
                            if (idToNameMap.ContainsKey(ski.PlayerId))
                            {
                                //  We already have it in the map:
                                //
                                thisPlayerName = idToNameMap[ski.PlayerId];
                            }
                            else
                            {
                                //  We have to look in the board state, inside the list of players in the pitch
                                //  Do not forget to scan both teams
                                //
                                for (int i = 0; i < 2; i++)
                                {
                                    foreach (PlayerState ps in rep.BoardState.ListTeams[i].ListPitchPlayers)
                                    {
                                        //  Found the player!
                                        //  Update the output and the map
                                        //
                                        if (ps.Data.Id == ba.PlayerId)
                                        {
                                            thisPlayerName = ps.Data.Name;
                                            i = 2;

                                            if (!idToNameMap.ContainsKey(ps.Data.Id))
                                                idToNameMap.Add(ps.Data.Id, ps.Data.Name);

                                            break;
                                        }
                                    }
                                }
                            }

                            skillsText += (thisPlayerName + ":" + (SkillEnum)ski.SkillId + ",");
                        }

                        if (hasUsedSkills)
                            tw.Write(String.Format("({0})", skillsText.Substring(0, skillsText.Length - 1)));

                        tw.Write(";");

                        //  Write roll
                        //
                        Roll roll = new Roll(bar.CoachChoices.ListDices, bar.Requirement, bar.RollType, thisRollModifiers);
                        tw.Write(roll.ToString() + ";");

                        //  Write the resut of things still without understanding
                        //
                        tw.Write(bar.IsOrderCompleted + ";");
                        tw.Write(bar.RequestType + ";");
                        tw.Write(bar.ResultType + ";");
                        tw.Write(String.Format("(from {0} to {1});", ba.Order.CellFrom.ToString(), ba.Order.CellTo.ToString()));

                        //  Write to the file
                        //
                        tw.WriteLine();
                    }
                }
            }

            tw.Close();
        }

        /// <summary>
        /// Writes a BBRZ replay into a CSV for easyness of comprehension
        /// </summary>
        /// <param name="bbrzFileName">BBRZ file name</param>       
        public static void Write2Csv(string file)
        {
            //  Unzip
            //
            DateTime preZip = DateTime.Now;
            string replayTextfile = Zipper.UnzipReplay(file);

            //  Deserialize
            //
            DateTime preDeserialize = DateTime.Now;

            //  If file already exists, go out
            //
            if (File.Exists(replayTextfile) == false)
            {
                return;
            }

            //  Read and interpret the file
            //
            string data = File.ReadAllText(replayTextfile);
            Replay replay = Replay.CreateFromString(data);

            //  Write the CSV
            // 
            Write2Csv(replay, file);
        }
    }
}