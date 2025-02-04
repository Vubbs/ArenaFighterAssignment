using System;
using static System.Console;
namespace ArenaFighter.Classes
{
    public class Battle
    {
        public Character Player { get; set; }
        public Character Opponent { get; set; }
        public Round LastRound { get; set; }
        public bool IsFinished { get; set; }
        public int RoundNumber { get; set; }
        public Battle(Character player, Character npc)
        {
            Player = player;
            Opponent = npc;
            RoundNumber = 1;
        }

        public void FightRound(bool isFinished = false)
        {
            string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
            string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
            string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
            string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
            string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
            int playerStr;
            int enemyStr;
            LastRound = new Round();
            if (isFinished == false)
            {
                WriteLine($"{YELLOW}---||| ROUND {RoundNumber} |||---\n\n\n{NORMAL}");

                WriteLine($"{GREEN}{Player.Name}{NORMAL} Lv.{GREEN}{Player.Level}{NORMAL} is facing against {RED}{Opponent.Name}{NORMAL} Lv.{RED}{Opponent.Level}{NORMAL}!");
                WriteLine($"{RED}{Opponent.Name}{NORMAL} has {RED}{Opponent.Health}{NORMAL} Health and {RED}{Opponent.Strength}{NORMAL} Strength!");
                WriteLine();

                playerStr = Player.Strength + LastRound.PlayerRoll;
                WriteLine($"{GREEN}{Player.Name}{NORMAL} tossed the dice and rolled a {YELLOW}{LastRound.PlayerRoll}{NORMAL} and gets a total strength roll of {CYAN}{playerStr}{NORMAL}!");

                enemyStr = Opponent.Strength + LastRound.OpponentRoll;
                WriteLine($"{RED}{Opponent.Name}{NORMAL} tossed the dice and rolled a {YELLOW}{LastRound.OpponentRoll}{NORMAL} and gets a total strength roll of {CYAN}{enemyStr}{NORMAL}!");
                WriteLine();
                if (playerStr > enemyStr)
                {
                    Opponent.Health -= Player.Damage;
                    WriteLine($"{GREEN}{Player.Name}{NORMAL} won the Strength roll and does {YELLOW}{Player.Damage}{NORMAL} damage to {RED}{Opponent.Name}{NORMAL}");
                    WriteLine($"{RED}{Opponent.Name}{NORMAL} has {RED}{Opponent.Health}{NORMAL} Health left!");
                    WriteLine("\n\n");
                    RoundNumber++;
                }
                else if (playerStr == enemyStr)
                {
                    Opponent.Health -= Player.Damage / 2;
                    Player.Health -= Opponent.Damage / 2;
                    WriteLine($"Both {GREEN}{Player.Name}{NORMAL} and {RED}{Opponent.Name}{NORMAL} strikes at eachother at the same time, both taking reduced damage.");
                    WriteLine();
                    WriteLine($"{GREEN}{Player.Name}{NORMAL} takes {YELLOW}{Opponent.Damage / 2}{NORMAL} damage and {RED}{Opponent.Name}{NORMAL} takes {YELLOW}{Player.Damage / 2}{NORMAL} damage!");
                    WriteLine();
                    WriteLine($"{GREEN}{Player.Name}{NORMAL} has {GREEN}{Player.Health}{NORMAL} Health left!");
                    WriteLine();
                    WriteLine($"{RED}{Opponent.Name}{NORMAL} has {RED}{Opponent.Health}{NORMAL} Health left!");
                    WriteLine();
                    RoundNumber++;
                }
                else
                {
                    Player.Health -= Opponent.Damage;
                    WriteLine($"{RED}{Opponent.Name}{NORMAL} won the Strength roll and does {YELLOW}{Opponent.Damage}{NORMAL} damage to {GREEN}{Player.Name}{NORMAL}");
                    WriteLine();
                    WriteLine($"{GREEN}{Player.Name}{NORMAL} has {GREEN}{Player.Health}{NORMAL} Health left!");
                    WriteLine();
                    RoundNumber++;
                }
                if (Player.Health <= 0 && Opponent.Health <= 0)
                {
                    Opponent.IsDead = true;
                    Player.IsDead = true;
                    IsFinished = true;
                    WriteLine("You both died at the same time, how unfortunate!");
                }
                if (Opponent.Health <= 0)
                {
                    Opponent.IsDead = true;
                    Player.PlayerScore++;
                    Player.Experience += 5;
                    if (Player.Experience >= 10)
                    {
                        WriteLine($"{YELLOW}You leveled up!{NORMAL}");
                        Player.Level += 1;
                        Player.Experience = 0;
                        WriteLine($"You are now Level {GREEN}{Player.Level}{NORMAL}!");
                        Random random = new Random();
                        int statUp = random.Next(1, 11);
                        if (statUp < 5)
                        {
                            Player.Health += 1;
                            WriteLine($"Your {GREEN}Health{NORMAL} went up by one!");
                            ReadKey();
                        }
                        if (statUp >= 5 && statUp < 10)
                        {
                            Player.Strength += 1;
                            WriteLine($"Your {RED}Strength{NORMAL} went up by one!");
                            ReadKey();
                        }
                        if (statUp == 10)
                        {
                            Player.Health += 1;
                            Player.Strength += 1;
                            WriteLine($"Lucky! Both your {RED}Strength{NORMAL} and {GREEN}Health{NORMAL} went up by one!");
                            ReadKey();
                        }
                    }
                    LastRound.IsFinal = true;
                    IsFinished = true;
                    if (IsFinished == true && LastRound.IsFinal == true)
                    {
                        LastRound.Winner = Player;
                    }
                }
                if (Player.Health <= 0)
                {
                    Player.IsDead = true;
                    LastRound.Winner = Opponent;
                    IsFinished = true;
                }
            }
            else
            {
                IsFinished = false;
            }


        }



        // Once battle starts, roll a dice by generating a random number between 1 and 6, and adding it to the strength value of the character.
        // Then compare the total strength values of both characters, and have the character with the highest value deal damage to the other.
    }
}
