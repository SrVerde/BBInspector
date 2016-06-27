using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBS.BBRZ;

namespace BBS.Models
{
    public class PlayerStats
    {
        private PlayerData data;

        public PlayerStats()
        { }

        public PlayerStats(PlayerData data)
        {
            this.data = data;
        }

        /// <summary>
        /// Movement Allowance
        /// </summary>
        public int Ma { get; protected set; }
        /// <summary>
        /// Strenght
        /// </summary>
        public int St { get; protected set; }
        /// <summary>
        /// Agility
        /// </summary>
        public int Ag { get; protected set; }
        /// <summary>
        /// Armour value
        /// </summary>
        public int Av { get; protected set; }
    }
}
