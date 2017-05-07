using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    class Human:Player
    {
        /// This method handles hits, determining which ships, if any, were hit and updating the instances accordingly. It will return a 0 if no ships were destroyed and a 1 if some were.
        public int SquareHit(int posX, int posY, Computer computer, Rendering rendering)
        {
            int hitShipID;
            if (Map[posX, posY] != 0 && Map[posX, posY] < 11) //if the map square is a ship.
            {
                hitShipID = Map[posX, posY] - 1;
                if (Ships[hitShipID].ShipHit()==0)
                {
                    computer.EnemyMap[posX, posY] = 2;
                    Map[posX, posY] = 12;
                    rendering.DrawGameScreens(this);
                    rendering.UpdateLog("The enemy shot hits!");
                }
                else
                {
                    for (int count = 0; count < Ships[hitShipID].Length; count++)
                    {
                        computer.EnemyMap[Ships[hitShipID].Coords[count, 0], Ships[hitShipID].Coords[count, 1]] = 3;
                        //Make it known to the computer that a ship has been destroyed.
                        Map[Ships[hitShipID].Coords[count, 0], Ships[hitShipID].Coords[count, 1]] = 13;
                    }
                    //Map[posX, posY] = 13;
                    rendering.DrawGameScreens(this);
                    rendering.UpdateLog(Ships[hitShipID].Name + " destroyed!");
                }
                return 0;
            }
            else
            {
                computer.EnemyMap[posX, posY] = 1;
                Map[posX, posY] = 11;
                rendering.DrawGameScreens(this);
                rendering.UpdateLog("The enemy shot misses!");
                return -1;
            }
        }
        /// This method handles the player making a shot on the enemy ships.
        public int TakeShot(Computer computer, Rendering rendering)
        {
            int xSelection = 0;
            int ySelection = 0;
            bool shotFired = false;
            int userInput;
            int turn = -1;

            while (shotFired == false)
            {
                rendering.DrawGameScreens(this);
                rendering.UpdateLog("Select Target");

                bool innerLoop = true;

                while (innerLoop)
                {
                    userInput = (int)Console.ReadKey(true).Key;
                    if (userInput < 75 && userInput > 64) //if the key pressed is a to j
                    {
                        xSelection = userInput - 65; //converts the keycode to an x co-ordinate;
                        innerLoop = false;
                    }
                }

                rendering.DrawTarget(this, xSelection);
                innerLoop = true;

                while (innerLoop)
                {
                    userInput = (int)Console.ReadKey(true).Key;
                    if (userInput < 58 && userInput > 47) //if the key pressed is 0 to 9
                    {
                        ySelection = userInput - 48;
                        innerLoop = false;
                    }
                }

                rendering.DrawTarget(this, xSelection, ySelection);
                rendering.UpdateLog("Ready to Fire");
                innerLoop = true;

                while (innerLoop)
                {
                    userInput = (int)Console.ReadKey(true).Key;

                    if (userInput == 32 || userInput == 13) //spacebar or enter
                    {
                        if (EnemyMap[xSelection, ySelection] != 0)
                        {
                            rendering.UpdateLog("Error: You've already fired at that square!");
                        }
                        else
                        {
                            turn = computer.SquareHit(xSelection, ySelection, this, rendering);
                            shotFired = true;
                        }
                        innerLoop = false;
                    }
                    else if (userInput == 8) //backspace
                    {
                        rendering.UpdateLog("Shot cancelled");
                        innerLoop = false;
                    }
                }
            }
            return turn;
        }
    }
}
