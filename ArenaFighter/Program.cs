using ArenaFighter.Classes;
using Microsoft.SqlServer.Server;
using System;
using static System.Console;

namespace ArenaFighter
{
    class Program
    {
        static void Main(string[] args)
        {
            string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
            string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
            string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
            string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
            string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
            try
            {
                while (true)
                {

                    Random random = new Random();
                    int cPoints = 9;
                    string chStr;
                    int cStr;
                    int cHealth;
                    WriteLine("Arena Fighter\n");
                    WriteLine("Make a character:");         // Character creation   ///////////////////////////////////////////////
                    Write("Enter Character Name: ");
                    ForegroundColor = ConsoleColor.Yellow;
                    string cName = ReadLine();
                    ForegroundColor = ConsoleColor.White;
                    while (true)
                    {
                        WriteLine($"\n\nYou have 9 points to spend on {RED}Strength{NORMAL} and {GREEN}Health{NORMAL}");
                        Write($"How much {RED}Strength{NORMAL} do you want? {CYAN}(Max amount is 8){NORMAL}: ");
                        ForegroundColor = ConsoleColor.Red;
                        chStr = ReadLine();
                        ForegroundColor = ConsoleColor.White;

                        if (int.TryParse(chStr, out cStr))
                        {
                            cStr = int.Parse(chStr);
                            if (cStr < 0 || cStr >= cPoints)
                            {
                                WriteLine("Please enter a valid number!");
                                continue;
                            }
                        }

                        else
                        {
                            WriteLine("Please enter a number!");
                            continue;
                        }

                        if (cStr > 0 && cStr < 10)
                        {
                            cPoints -= cStr;
                            WriteLine($"\nAssigning left over points to {GREEN}health{NORMAL}");
                            cHealth = cPoints;
                            WriteLine("Your final stats are:");
                            WriteLine($"Name: {YELLOW}{cName}{NORMAL}");
                            WriteLine($"Strength: {RED}{cStr}{NORMAL}");
                            WriteLine($"Health: {GREEN}{cHealth}{NORMAL}");
                            WriteLine();
                            break;
                        }

                    }
                    Character player = new Character(cName, cStr, cHealth);
                    while (true)
                    {
                        if (player.PlayerScore <= 3)                  /////////////////// BATTLE vs LV 1 Enemy ////////////////////
                        {
                            Character npc = new Character("Enemy", random.Next(2, 7), random.Next(2, 7));
                            Battle battle = new Battle(player, npc);
                            WriteLine("Are you ready to fight?");
                            WriteLine($"{CYAN}Press any key to continue...{NORMAL}");
                            ReadKey();
                            Battle(battle, player, npc);
                        }

                        else if (player.PlayerScore > 3 && player.PlayerScore <= 7)    //////////// BATTLE vs LV 2 Enemy //////////////
                        {
                            Character npc = new Character("Enemy", random.Next(3, 9), random.Next(3, 9));
                            npc.Level = 2;
                            Battle battle = new Battle(player, npc);
                            WriteLine("Are you ready to fight?");
                            WriteLine($"{CYAN}Press any key to continue...{NORMAL}");
                            ReadKey();
                            Battle(battle, player, npc);
                        }

                        else if (player.PlayerScore > 7 && player.PlayerScore <= 10)
                        {
                            Character npc = new Character("Enemy", random.Next(4, 10), random.Next(4, 10)); //// BATTLE vs LV 3 Enemy ////
                            npc.Level = 3;
                            Battle battle = new Battle(player, npc);
                            WriteLine("Are you ready to fight?");
                            WriteLine($"{CYAN}Press any key to continue...{NORMAL}");
                            ReadKey();
                            Battle(battle, player, npc);
                        }
                        else
                        {
                            Character npc = new Character("Enemy", random.Next(5, 15), random.Next(5, 15)); //// BATTLE vs LV 4 Enemy ////
                            npc.Level = 4;
                            Battle battle = new Battle(player, npc);
                            WriteLine("Are you ready to fight?");
                            WriteLine($"{CYAN}Press any key to continue...{NORMAL}");
                            ReadKey();
                            Battle(battle, player, npc);
                                                    }
                        if (player.IsDead == true)            ///////////////////// GAME OVER /////////////////////////////
                        {
                            WriteLine($"{RED}Game over!\n\n{NORMAL}");
                            WriteLine($"Your final score is: {GREEN}{player.PlayerScore}{NORMAL}");
                            if (player.PlayerScore >= 10)
                            {
                                WriteLine($"{GREEN}Exellent! You got great score!{NORMAL}\n\n\n");
                                ReadKey();
                            }
                            else if (player.PlayerScore < 10 && player.PlayerScore >= 5)
                            {
                                WriteLine($"{YELLOW}Good job! You got a decent score.{NORMAL}\n\n\n");
                                ReadKey();
                            }
                            else if (player.PlayerScore < 5 && player.PlayerScore >= 3)
                            {
                                WriteLine($"{CYAN}Decent! You got some points atleast!{NORMAL}\n\n\n");
                                ReadKey();
                            }
                            else
                            {
                                WriteLine("Better luck next time!\n\n\n");
                                ReadKey();
                            }
                            break;
                        }
                        else
                            continue;
                    }
                    Write($"{YELLOW}Do you want to try again? ( {GREEN}y{NORMAL} /{RED} any key{YELLOW} ): {NORMAL}"); //// Try again ////
                    ForegroundColor = ConsoleColor.Cyan;
                    string tryAgain = ReadLine().ToLower();
                    ForegroundColor = ConsoleColor.White;
                    if (tryAgain == "y")
                        continue;
                    else
                        break;
                }
                WriteLine($"{GREEN}Thank you for playing!{NORMAL}\n\n\n");
                WriteLine($"{CYAN}Press any key to exit the game...{NORMAL}");
                ReadKey();
            }
            catch (Exception e) { WriteLine(e.Message); }
        }




        ///////////////////  Battle Checks ////////////////////
        static void Battle(Battle battle, Character player, Character npc)
        {
            string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
            string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
            string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
            string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
            while (true)
            {
                if (player.IsDead == false && npc.IsDead == false)
                {
                    Clear();
                    battle.FightRound();
                    WriteLine($"{CYAN}Press any key to continue...{NORMAL}");
                    ReadKey();
                }
                if (battle.LastRound.Winner == player)
                {
                    Clear();
                    WriteLine($"Your current score is {GREEN}{player.PlayerScore}{NORMAL}");
                    WriteLine($"Do you want to continue? ( {GREEN}y{NORMAL} /{RED} any key{NORMAL} )");
                    ForegroundColor = ConsoleColor.Green;
                    string cont = ReadLine().ToLower();
                    ForegroundColor = ConsoleColor.White;
                    if (cont == "y")
                    {
                        battle.FightRound(true);
                        break;
                    }

                    else
                    {
                        player.IsDead = true;
                        break;
                    }


                }
                if (player.IsDead == true)
                {
                    Clear();
                    WriteLine("You died");
                    break;
                }
            }
        }
    }
}
