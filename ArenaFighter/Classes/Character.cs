using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
[assembly: InternalsVisibleTo("UnitTestProject1")]

namespace ArenaFighter.Classes
{
    public class Character
    {
        private string _name;
        public string Name { get { return _name; } set { _name = Name; } }
        public int Strength { get; set; }
        public int Health { get; set; }
        public int Damage {  get; set; }
        public bool IsDead { get; set; }
        public int PlayerScore { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        public Character(string name, int strength, int health)
        {
            _name = name;
            Strength = strength;
            Health = health;
            Damage = Strength / 2;
            PlayerScore = 0;
            IsDead = false;
            Experience = 0;
            Level = 1;
        }
        public Character()      // if no parameters are used
        {
            _name = "Name";
            Strength = 1;
            Health = 1;
        }
    }
}
