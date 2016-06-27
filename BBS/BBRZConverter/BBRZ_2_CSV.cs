using BBS.BBRZ;
using BBS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BBS.Converter
{
    public static class BBRZ_2_CSV
    {
        public static void Transform()
        {
            //  Open a standard file dialog 
            //
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "BB2 replay files (*.bbrz)|*.bbrz";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Title = "Please select replay files to analyze";

            //  Go on only with "OK" button
            //
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //  Process each selected file
                //
                foreach (string file in dialog.FileNames)
                {
                    //  Unzip
                    //
                    DateTime preZip = DateTime.Now;
                    string replayTextfile = Zipper.UnzipReplay(file);

                    //  Deserialize
                    //
                    DateTime preDeserialize = DateTime.Now;

                    //  If file already exists, go to the next one
                    //
                    if (File.Exists(replayTextfile) == false)
                    {
                        continue;
                    }

                    //  Read and interpret the file
                    //
                    string data = File.ReadAllText(replayTextfile);
                    Replay replay = Replay.CreateFromString(data);

                    //  Generate the CSV file
                    //
                    TextWriter tw = File.CreateText(file + ".rolls.csv");
                    tw.Write("ActionType;");
                    tw.Write("Player;");
                    tw.Write("RollType;");
                    tw.Write("Requirement;");
                    tw.Write("Modifiers;");
                    tw.Write("Team;");
                    tw.Write("SkillsUsed;");
                    tw.Write("DiceResults;");
                    tw.Write("IsCompleted;");
                    tw.WriteLine();
                    Dictionary<int, string> idToNameMap = new Dictionary<int, string>();
                    ActionType thisAction;
                    RollType thisRoll;
                    string thisPlayerName = "";

                    //  Scan all the replay steps
                    //
                    foreach (ReplayStep rep in replay.ReplayStep)
                    {
                        //  Skip null replay steps
                        //
                        if (rep.RulesEventBoardAction == null)
                            continue;

                        //  Only interested in the in-game replay steps (not pre and post game steps)
                        //  Each replay step may contain several sub steps
                        //  Each sub step is stored in a rules event board action
                        //
                        foreach (RulesEventBoardAction ba in rep.RulesEventBoardAction)
                        {
                            //  Get action type
                            //
                            string start = "";
                            thisAction = (ActionType)ba.ActionType;
                            start += (thisAction + ";");

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
                            
                            //  Each rules eboard action may have several results
                            //  We need a line for each one
                            //  
                            foreach (BoardActionResult bar in ba.Results.BoardActionResult)
                            {
                                //  Write the action type and the name of the involved player
                                //
                                tw.Write(start);

                                //  Write the roll type
                                //
                                thisRoll = (RollType)bar.RollType;
                                if (thisRoll == RollType.BounceBallRoll)
                                    thisRoll = Roll.GetMovingBallRollType(bar.CoachChoices.ListDices);
                                tw.Write(thisRoll + ";");

                                //  Calculate roll modifier
                                // 
                                int modifier = 0;
                                bool hasModifier = false;
                                string modifierText = "";
                                foreach (DiceModifier dm in bar.ListModifiers)
                                {
                                    modifierText += (Modifier.GetModifierResult(dm.Skill, dm.Type, dm.Value) + ",");
                                    modifier += dm.Value;
                                    hasModifier = true;
                                }

                                //  Write the roll requirements
                                //
                                if (bar.Requirement > 1 && (thisRoll != RollType.BlockRoll))
                                    if (hasModifier)
                                        tw.Write(Roll.GetRollRequirement(bar.Requirement, modifier, thisRoll) + ";");
                                    else
                                        tw.Write(bar.Requirement + "+;");
                                else
                                    tw.Write(";");

                                //  Write the modifiers
                                //
                                if (hasModifier)
                                    tw.Write(String.Format("({0})", modifierText));
                                tw.Write(";");

                                //  Write concerned team's action (choice, result or whatever)  
                                //
                                tw.Write(bar.CoachChoices.ConcernedTeam + ";");

                                //  Write used skills in the roll
                                //
                                bool hasUsedSkills = (bar.CoachChoices.ListSkills.Count > 0);

                                if (hasUsedSkills)
                                    tw.Write("(");
                                
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

                                    tw.Write(thisPlayerName + ":" + (SkillEnum)ski.SkillId + ",");
                                }

                                if (hasUsedSkills)
                                    tw.Write(")");

                                tw.Write(";");

                                //  Write rolled dices
                                //
                                string diceRolls = Dice.GetDiceResults(bar.CoachChoices.ListDices, thisRoll);
                                tw.Write(diceRolls + ";");

                                //  Write if the order is finished
                                //
                                tw.Write(bar.IsOrderCompleted + ";");

                                //  Write to the file
                                //
                                tw.WriteLine();
                            }
                        }
                    }

                    tw.Close();
                }
            }
        }
    }
}
