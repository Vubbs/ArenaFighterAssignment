using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter.Classes
{
    public class Round
    {
        public int PlayerRoll { get; set; }
        public int OpponentRoll { get; set; }
        public Character Winner { get; set; }
        public bool IsFinal { get; set; }
        public int RoundNumber { get; set; }
        public Round()
        {
            Random diceRoll = new Random();
            PlayerRoll = diceRoll.Next(1,7);
            OpponentRoll = diceRoll.Next(1,7);
            Winner = null;
        }

    }
}
