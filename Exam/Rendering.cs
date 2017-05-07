using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exam
{
    class Rendering
    {
        private string[] consoleLog;
        private int[] consoleLogColour;
        const string statistic = "score.txt";
        public FileStream fs;
        private StreamReader sr;
        public Rendering()
        {
            consoleLogColour = new int[17];
            consoleLog = new string[17];
            fs = new FileStream(statistic, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            sr = new StreamReader(fs, Encoding.Unicode);
        }
        /// This method draws the main game menu to the screen.
        public void DrawMenu()
        {
            Console.Clear();
            DrawTitle();
            Console.SetCursorPosition(6, 10);
            Console.Write("A - Start game with manually ship placement");
            Console.SetCursorPosition(6, 12);
            Console.Write("B - Start game with automatically ship placement");
            Console.SetCursorPosition(6, 14);
            Console.Write("C - Exit");
            DrawStatistic();
        }
        /// This method draws a fancy title for the menu
        private void DrawTitle()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(3, 2);
            Console.Write("░██  ░█ ░███░███░█  ░███ ░██░█░█░█░██");
            Console.SetCursorPosition(3, 3);
            Console.Write("░█░█░█░█ ░█  ░█ ░█  ░█  ░█  ░█░█░█░█░█");
            Console.SetCursorPosition(3, 4);
            Console.Write("░██ ░███ ░█  ░█ ░█  ░██  ░█ ░███░█░██");
            Console.SetCursorPosition(3, 5);
            Console.Write("░█░█░█░█ ░█  ░█ ░█  ░█    ░█░█░█░█░█");
            Console.SetCursorPosition(3, 6);
            Console.Write("░██ ░█░█ ░█  ░█ ░███░███░██ ░█░█░█░█");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
        }
        /// This method draws the computer and player grids to the screen.
        public void DrawGameScreens(Human player)
        {
            DrawComputerShips(player);
            DrawPlayerShips(player);
        }
        /// This method draws the window for the CUI to the screen. This includes the status box, grids and instruction box.
        public void DrawGameWindow()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("╔═════════════════════╤════════════════════════╗");
            for (int c = 0; c < 11; c++)
            {
                Console.SetCursorPosition(0, c + 1);
                Console.Write("║");
                Console.SetCursorPosition(22, c + 1);
                Console.Write("│");
                Console.SetCursorPosition(47, c + 1);
                Console.WriteLine("║");
            }
            Console.WriteLine("╟─────────────────────┤                        ║");
            for (int c = 0; c < 11; c++)
            {
                Console.SetCursorPosition(0, c + 13);
                Console.Write("║");
                Console.SetCursorPosition(22, c + 13);
                Console.Write("│");
                Console.SetCursorPosition(47, c + 13);
                Console.WriteLine("║");
            }
            Console.WriteLine("╚═════════════════════╧════════════════════════╝");
            Console.SetCursorPosition(22, 6);
            Console.WriteLine("├────────────────────────╢");
            //write grid assists to the screen
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            for (int x = 1; x < 11; x += 2)
            {
                Console.SetCursorPosition(x * 2, 1);
                Console.Write((char)(64 + x) + " ");
                Console.SetCursorPosition(x * 2, 13);
                Console.Write((char)(64 + x) + " ");
                if (x % 2 == 1)
                {
                    Console.SetCursorPosition(1, x + 1);
                    Console.Write(x - 1);
                    Console.SetCursorPosition(1, x + 13);
                    Console.Write(x - 1);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            for (int x = 2; x < 11; x += 2)
            {
                Console.SetCursorPosition(x * 2, 1);
                Console.Write((char)(64 + x) + " ");
                Console.SetCursorPosition(x * 2, 13);
                Console.Write((char)(64 + x) + " ");
                if (x % 2 == 0)
                {
                    Console.SetCursorPosition(1, x + 1);
                    Console.Write(x - 1);
                    Console.SetCursorPosition(1, x + 13);
                    Console.Write(x - 1);
                }
            }
            Console.SetCursorPosition(0, 0);
        }
        /// This method draws the player's grid to the CUI.
        private void DrawPlayerShips(Human player)
        {
            int[,] renderMap = player.Map;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.SetCursorPosition((x * 2) + 2, y + 14);
                    switch (renderMap[x, y])
                    {
                        case 0:
                            //render in a checkerboard pattern
                            if (y % 2 == 1)
                            {
                                if (x % 2 == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    Console.Write("▒▒");
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.Write("  ");
                                }
                            }
                            else
                            {
                                if (x % 2 == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    Console.Write("▒▒");
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.Write("  ");
                                }
                            }
                            break;
                        case 11: //Previously hit location
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("▒▒");
                            break;
                        case 12: //Hit Ship
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("▒▒");
                            break;
                        case 13: //Destroyed Ship
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("▒▒");
                            break;
                        default: //Ship
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write("  ");
                            break;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        /// This method draws the enemy player grid to the CUI.
        private void DrawComputerShips(Human player)
        {
            int[,] renderMap = player.EnemyMap;
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.SetCursorPosition((x * 2) + 2, y + 2);
                    switch (renderMap[x, y])
                    {
                        case 0:
                            //Draw in a checkerboard pattern
                            if (y % 2 == 1)
                            {
                                if (x % 2 == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.Write("??");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.Write("??");
                                }
                            }
                            else
                            {
                                if (x % 2 == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.Write("??");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.Write("??");
                                }
                            }
                            break;
                        case 1: //Empty
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("▒▒");
                            break;
                        case 2: //Ship
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("▒▒");
                            break;
                        case 3: //Ship Destroyed
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("▒▒");
                            break;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        //public void DrawShipPlacement(int x, int y, uint length, bool vertical)
        public void DrawShipPlacement(int x, int y, int length, ShipOrientation so)
        {
            if(so == ShipOrientation.Vertical)
            {
                for (int i = 14; i < length + 14; i++)
                {
                    Console.SetCursorPosition((x * 2) + 2, y + i);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("  ");
                }
            }
            else
            {
                for (int i = 2; i < length + 2; i++)
                {
                    Console.SetCursorPosition((x + (i - 1)) * 2, y + 14);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("  ");
                }
            }

        }
        /// This method updates the event log in the console.
        public void UpdateLog(string textToWrite)
        {
            int numberOfLines = 0;
            int stringLength = 0;
            string[] newConsoleLog = new string[17];
            string remainingString;

            stringLength = textToWrite.Length;
            numberOfLines = (int)Math.Ceiling(stringLength / (double)23);
            remainingString = textToWrite;

            //A tad annoying.
            for (int c = 0; c < numberOfLines; c++)
            {
                if (remainingString.Length > 23)
                {
                    newConsoleLog[c] = textToWrite.Substring((c * 23), 23); //take off a 20 character chunk and add it to the log.
                    remainingString = textToWrite.Substring(stringLength - (stringLength - ((c + 1) * 23)), stringLength - ((c + 1) * 23)); //calculate the remaining string to write.
                }
                else
                {
                    newConsoleLog[c] = textToWrite.Substring((c * 23), remainingString.Length); //add the remaining string to the log.
                    for (int count = remainingString.Length; count < 23; count++)
                    {
                        newConsoleLog[c] += " "; //pad the remaining space with spaces (heh) to 'erase' the previous log beneath.
                    }
                }
                consoleLogColour[c] = 1; //white
            }

            int originalCount = 0; //the index in the old log.

            for (int c = numberOfLines; c < 17; c++)
            {
                newConsoleLog[c] = consoleLog[originalCount];
                consoleLogColour[c] = 0; //grey
                originalCount++;
            }
            for (int c = 0; c < 17; c++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(23, 7 + c);
                if (consoleLogColour[c] == 1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    if (c < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.Write(newConsoleLog[c]);
            }
            Array.Copy(newConsoleLog, consoleLog, newConsoleLog.Length);
            Console.SetCursorPosition(0, 0);
        }
        /// Draws a line to show the X column selected.
        public void DrawTarget(Human player, int x)
        {
            DrawComputerShips(player);
            Console.BackgroundColor = ConsoleColor.Yellow;
            for (int c = 2; c < 12; c++)
            {
                Console.SetCursorPosition((x + 1) * 2, c);
                Console.Write("  ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        /// Draws a square over the sqaure that has been targeted.
        public void DrawTarget(Human player, int x, int y)
        {
            DrawComputerShips(player);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((x + 1) * 2, 2 + y);
            Console.Write("  ");
        }
        /// This method draws a victory screen for the player (0) or the computer (1)
        public void DrawVictoryScreen(int winner)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(0, 23);
            Console.WriteLine("                                            ");
            Console.SetCursorPosition(0, 24);
            Console.WriteLine("                                            ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(17, 20);
            Console.Write(" _▄ _▄   █████ ▄  ▄_  ");
            Console.SetCursorPosition(17, 21);
            Console.Write("▄▄█▄▄█▄█████████▄▄█▄▄▄");
            Console.SetCursorPosition(18, 22);
            Console.Write("▀██████████████████▀");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 2);

            if (winner == 0) //Player victory
            {
                Console.Write("You are victorious!");
            }
            else
            {
                Console.Write("Your fleet has been wiped out...");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
        }
        /// This method draws information such as the remaining ships and the hit chance to the CUI.
        public void DrawInfoBox(Human player, Computer computer)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(24, 1);
            Console.Write("Remaining Ships:");
            Console.SetCursorPosition(24, 2);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("You: {0:D2}", player.RemainingShips());
            Console.SetCursorPosition(24, 3);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("CPU: {0:D2}", computer.RemainingShips());
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void DrawStatistic()
        {
            Console.SetCursorPosition(75, 0);
            Console.Write("Score board");
            Console.SetCursorPosition(60, 1);
            Console.WriteLine("╔═════════════════════╤═══════════════╗");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine("║        Date         │     Score     ║");
            Console.SetCursorPosition(60, 3);
            Console.WriteLine("╚═════════════════════╧═══════════════╝");
            for (int c = 0; sr.EndOfStream!=true; c++)
            {
                Console.SetCursorPosition(60, c + 4);
                Console.Write("║");
                Console.SetCursorPosition(62, c + 4);
                Console.Write(sr.ReadLine());
                Console.SetCursorPosition(83, c + 4);
                Console.Write("│");
                Console.SetCursorPosition(85, c + 4);
                Console.Write(sr.ReadLine());
                Console.SetCursorPosition(89, c + 4);
                Console.WriteLine("║");
                if(sr.EndOfStream) Console.WriteLine("╚═════════════════════╧═══════════════╝");
            }
            sr.Dispose();
        }
    }
}
