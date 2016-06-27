using BBS.BBRZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
    public class Player
    {
        public Player(PlayerState ps)
        {
            this.Id = ps.Id;
            this.Name = ps.Data.Name;

            this.Stats = new PlayerStats(ps.Data);
            //this.HasTheBall = ps.
            this.Position = new BoardLocation(ps.Cell.x, ps.Cell.y);

            Skills = new List<SkillEnum>();

            if (ps.Data.ListSkills != null)
            {
                string skills = ps.Data.ListSkills.Substring(1, ps.Data.ListSkills.Length - 2);
                foreach (string s in skills.Split(','))
                    Skills.Add((SkillEnum)int.Parse(s));
            }
        }

        public int Id
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public bool HasTheBall { get; protected set; }
        public List<SkillEnum> Skills { get; protected set; }
        public PlayerStats Stats { get; protected set; }
        public BoardLocation Position { get; protected set; }
        public bool IsBetweenPublicAndPlayer(Player otherPlayer)
        {
            if (Position.IsPitchBorder)
            {                
                return (int)Position.Type == (int)(otherPlayer.Position - this.Position);
            }
            else
            {
                return false;
            }
        }
    }
}
