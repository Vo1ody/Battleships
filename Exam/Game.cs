using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Exam
{
    class Game
    {
        private StreamWriter sw;
        public int playerTurns, computerTurns;
        private int choice, level;
        Rendering rendering;
        Human player;
        Logic computer;
        public Game(int level)
        {
            rendering = new Rendering();
            player = new Human();
            this.level = level;
            switch (level)
            {
                case 1: computer = new EasyLevel();
                    break;
                case 2: computer = new NormalLevel();
                    break;
                case 3: computer = new HardLevel();
                    break;
            }
            sw = new StreamWriter(rendering.fs, Encoding.Unicode);
        }
        public void Run()
        {
            bool programLoop = true;

            while (programLoop)
            {
                MainMenu();
                GameLoop();
            }
        }
        /// This method handles the main menu of the game.
        private void MainMenu()
        {
            int userInput;
            bool exitMenu = false;
            rendering.DrawMenu();
            Console.BackgroundColor = ConsoleColor.Black;
            while (!exitMenu)
            {
                userInput = (int)Console.ReadKey(true).Key; //returns the keycode for the key pressed by the user.
                switch (userInput)
                {
                    case (65): //a
                        exitMenu = true;
                        choice = 65;
                        break;
                    case (66): //b
                        exitMenu = true;
                        choice = 66;
                        break;
                    case (67): //c
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        /// This method handles the main game loop
        private void GameLoop()
        {
            bool exitGame = false;
            rendering.DrawGameWindow();
            rendering.DrawGameScreens(player);
            if (choice == 65)
            {
                ManuallyArragement.Arrange(ref player, rendering);
                AutoArragement.Arrange(ref computer.c_player);
            }
            if (choice == 66)
            {
                AutoArragement.Arrange(ref player);
                System.Threading.Thread.Sleep(500);
                AutoArragement.Arrange(ref computer.c_player);
                if(level == 3)(computer as HardLevel).GetArr(player);
                AutoArragement.ArrangeCompleted(rendering);

            }
            playerTurns = computerTurns = 0;
            while (!exitGame) //main game loop
            {
                int tmp;
                rendering.DrawGameScreens(player);
                rendering.DrawInfoBox(player, computer.c_player);
                do
                {
                    tmp = player.TakeShot(computer.c_player, rendering);
                    playerTurns++;
                    System.Threading.Thread.Sleep(1000);
                    rendering.DrawGameScreens(player);
                } while (tmp == 0);
                do
                {
                    tmp = computer.TakeShot(player, rendering);
                    computerTurns++;
                    System.Threading.Thread.Sleep(1000);
                } while (tmp == 0);
                if (player.AllShipsDestroyed())
                {
                    rendering.DrawVictoryScreen(1);
                    Console.ReadLine();
                    exitGame = true;
                }
                if (computer.c_player.AllShipsDestroyed())
                {
                    rendering.DrawVictoryScreen(0);
                    Console.ReadLine();
                    exitGame = true;
                }
            }
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine(playerTurns.ToString());
            sw.Dispose();
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}
