using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    static class ManuallyArragement
    {
        static public void Arrange(ref Human p, Rendering r)
        {
            ShipAdd(ref p);
            PlaceShips(p, r);
        }
        static private void ShipAdd(ref Human p)
        {
            p.Ships.Add(new Ship("Four-decker",4, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Three-decker", 3, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Three-decker", 3, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Two-decker", 2, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Two-decker", 2, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Two-decker", 2, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Single-decker", 1, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Single-decker", 1, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Single-decker", 1, ShipOrientation.Vertical));
            p.Ships.Add(new Ship("Single-decker", 1, ShipOrientation.Vertical));
        }
        static private void PlaceShips(Human p, Rendering r)
        {
            int shipLength;
            int shipX, shipY;
            int userInput;
            bool shipPlaced;
            ShipOrientation so;

            for (int shipNumber = 0; shipNumber < 10; shipNumber++)
            {
                shipLength = p.Ships[shipNumber].Length;
                so = p.Ships[shipNumber].Orientation;
                shipX = 0;
                shipY = 0;
                
                shipPlaced = false;

                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(25, 4);
                Console.Write("(use arrows + space)");
                Console.SetCursorPosition(25, 5);
                Console.Write("(r - rotate)");

                r.UpdateLog("Place " + p.Ships[shipNumber].Name);

                while (!shipPlaced)
                {
                    r.DrawGameScreens(p);
                    r.DrawShipPlacement(shipX, shipY, shipLength, so);
                    userInput = (int)Console.ReadKey(true).Key;
                    switch (userInput)
                    {
                        case 82: //'r' - rotate
                            if(so == ShipOrientation.Vertical)
                            {
                                if (shipX + shipLength > 10)
                                {
                                    shipX = 10 - shipLength;
                                }
                            }
                            else
                            {
                                if (shipY + shipLength > 10)
                                {
                                    shipY = 10 - shipLength;
                                }
                            }
                            p.Ships[shipNumber].Rotate();
                            so = p.Ships[shipNumber].Orientation;
                            break;
                        case 37: //left arrow
                            if (shipX - 1 >= 0)
                            {
                                shipX--;
                            }
                            break;
                        case 38: //up arrow
                            if (shipY - 1 >= 0)
                            {
                                shipY--;
                            }
                            break;
                        case 39: //right arrow
                            if(so == ShipOrientation.Vertical)
                            {
                                if (shipX + 1 < 10)
                                {
                                    shipX++;
                                }
                            }
                            else
                            {
                                if (shipX + shipLength < 10)
                                {
                                    shipX++;
                                }
                            }
                            break;
                        case 40: //down arrow
                            if (so == ShipOrientation.Vertical)
                            {
                                if (shipY + shipLength < 10)
                                {
                                    shipY++;
                                }
                            }
                            else
                            {
                                if (shipY + 1 < 10)
                                {
                                    shipY++;
                                }
                            }
                            break;

                        case 32: //Space bar
                            if (ShipCollision(shipX, shipY, shipLength, so, p) == false)
                            {
                                for (int i = 0; i < shipLength; i++)
                                {
                                    if (so == ShipOrientation.Vertical)
                                    {
                                        p.Map[shipX, shipY + i] = shipNumber + 1; 
                                        //this will be a unique identifier in order to allow quick lookups of hit ships.
                                    }
                                    else
                                    {
                                        p.Map[shipX + i, shipY] = shipNumber + 1; 
                                        //this will be a unique identifier in order to allow quick lookups of hit ships.
                                    }
                                }
                                p.Ships[shipNumber].PlaceShip(shipX, shipY, so);
                                shipPlaced = true;
                            }
                            break;
                    }
                }
            }
            r.UpdateLog("All ships placed!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(25, 5);
            Console.Write("                    ");
            Console.SetCursorPosition(25, 4);
            Console.Write("                    ");
        }
        /// This method returns true if the ships current placement would result in a collision with an existing ship.
        static private bool ShipCollision(int shipX, int shipY, int shipLength, ShipOrientation so, Human p)
        {
            bool collision = false;
            if (shipLength == 1)
            {
                if (shipX != 0 && shipY != 0)
                {
                    if (p.Map[shipX - 1, shipY - 1] != 0 || p.Map[shipX, shipY - 1] != 0 || p.Map[shipX + 1, shipY - 1] != 0 ||
                        p.Map[shipX + 1, shipY] != 0 || p.Map[shipX + 1, shipY + 1] != 0 || p.Map[shipX, shipY + 1] != 0 ||
                        p.Map[shipX - 1, shipY + 1] != 0 || p.Map[shipX - 1, shipY] != 0) collision = true;
                }
                if (shipX == 0 && shipY == 0)
                    if (p.Map[shipX + 1, shipY] != 0 || p.Map[shipX + 1, shipY + 1] != 0 || p.Map[shipX, shipY + 1] != 0) collision = true;
                if (shipX == 9 && shipY == 9)
                    if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX - 1, shipY - 1] != 0 || p.Map[shipX - 1, shipY] != 0) collision = true;
                if (shipX == 9 && shipY == 0)
                    if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX - 1, shipY + 1] != 0 || p.Map[shipX, shipY + 1] != 0) collision = true;
                if (shipX == 0 && shipY == 9)
                    if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX + 1, shipY - 1] != 0 || p.Map[shipX + 1, shipY] != 0) collision = true;

                if (shipX == 0 && shipY != 0 && shipY != 9)
                    if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX + 1, shipY - 1] != 0 || p.Map[shipX + 1, shipY] != 0 ||
                        p.Map[shipX + 1, shipY + 1] != 0 || p.Map[shipX, shipY + 1] != 0) collision = true;
                if (shipX == 9 && shipY != 0 && shipY != 9)
                    if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX - 1, shipY - 1] != 0 || p.Map[shipX - 1, shipY] != 0 ||
                        p.Map[shipX - 1, shipY + 1] != 0 || p.Map[shipX, shipY + 1] != 0) collision = true;
                if (shipY == 9 && shipX != 0 && shipX != 9)
                    if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX - 1, shipY - 1] != 0 || p.Map[shipX, shipY - 1] != 0 ||
                        p.Map[shipX + 1, shipY - 1] != 0 || p.Map[shipX + 1, shipY] != 0) collision = true;
                if (shipY == 0 && shipX != 0 && shipX != 9)
                    if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX - 1, shipY + 1] != 0 || p.Map[shipX, shipY + 1] != 0 ||
                        p.Map[shipX + 1, shipY + 1] != 0 || p.Map[shipX + 1, shipY] != 0) collision = true;
            }
            else
            {
                if (so == ShipOrientation.Vertical)
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (p.Map[shipX, shipY + i] != 0) collision = true; //if the map square contains a ship
                    }
                    if (shipX != 0 && shipX != 9 && shipY != 0 && shipY < 10 - shipLength)
                    {
                        if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX + 1, shipY] != 0 || p.Map[shipX, shipY - 1] != 0 ||
                            p.Map[shipX - 1, shipY - 1] != 0 || p.Map[shipX + 1, shipY - 1] != 0 || p.Map[shipX, shipY + shipLength] != 0 || 
                            p.Map[shipX + 1, shipY + shipLength - 1] != 0 || p.Map[shipX - 1, shipY + shipLength - 1] != 0 ||
                            p.Map[shipX + 1, shipY + shipLength] != 0 || p.Map[shipX - 1, shipY + shipLength] != 0) collision = true;
                    }
                    if (shipX == 0 && shipY == 0)
                        if (p.Map[shipX + 1, shipY] != 0 || p.Map[shipX, shipY + shipLength] != 0 ||
                            p.Map[shipX + 1, shipY + shipLength] != 0 || p.Map[shipX + 1, shipY + shipLength - 1] != 0) collision = true;
                    if (shipY == 0 && shipX != 0 && shipX != 9)
                        if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX + 1, shipY] != 0 || p.Map[shipX, shipY + shipLength] != 0 ||
                            p.Map[shipX + 1, shipY + shipLength] != 0 || p.Map[shipX - 1, shipY + shipLength] != 0 ||
                            p.Map[shipX + 1, shipY + shipLength - 1] != 0 || p.Map[shipX - 1, shipY + shipLength - 1] != 0) collision = true;
                    if (shipX == 0 && shipY != 0 && shipY < 10 - shipLength)
                    {
                        if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX + 1, shipY - 1] != 0 ||
                            p.Map[shipX + 1, shipY] != 0 ||
                            p.Map[shipX, shipY + shipLength] != 0 || p.Map[shipX + 1, shipY + shipLength] != 0 ||
                            p.Map[shipX + 1, shipY + shipLength - 1] != 0) collision = true;
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (p.Map[shipX + 1, shipY - 1 + i] != 0 || p.Map[shipX + 1, shipY + i] != 0
                                || p.Map[shipX + 1, shipY + i + 1] != 0) collision = true;
                        }
                    }
                    if (shipX == 9 && shipY != 0 && shipY < 10 - shipLength)
                    {
                        if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX - 1, shipY - 1] != 0 ||
                            p.Map[shipX - 1, shipY] != 0 ||
                            p.Map[shipX, shipY + shipLength] != 0 || p.Map[shipX - 1, shipY + shipLength] != 0 ||
                            p.Map[shipX - 1, shipY + shipLength - 1] != 0) collision = true;
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (p.Map[shipX - 1, shipY - 1 + i] != 0 || p.Map[shipX - 1, shipY + i] != 0
                                || p.Map[shipX - 1, shipY + i + 1] != 0) collision = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (p.Map[shipX + i, shipY] != 0) collision = true; //if the map square contains a ship
                    }
                    if (shipX != 0 && shipY != 0 && shipY != 9 && shipX < 10 - shipLength)
                    {
                        if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX, shipY + 1] != 0 || p.Map[shipX - 1, shipY] != 0 ||
                            p.Map[shipX - 1, shipY - 1] != 0 || p.Map[shipX - 1, shipY + 1] != 0 || p.Map[shipX + shipLength, shipY] != 0 ||
                            p.Map[shipX + shipLength, shipY + 1] != 0 || p.Map[shipX + shipLength, shipY - 1] != 0 ||
                            p.Map[shipX + shipLength - 1, shipY + 1] != 0 || p.Map[shipX + shipLength - 1, shipY - 1] != 0) collision = true;
                    }
                    if (shipX == 0 && shipY == 0)
                        if (p.Map[shipX, shipY + 1] != 0 || p.Map[shipX + shipLength, shipY] != 0 ||
                            p.Map[shipX + shipLength, shipY + 1] != 0 || p.Map[shipX + shipLength - 1, shipY + 1] != 0) collision = true;
                   if (shipX == 0 && shipY != 0 && shipY != 9)
                        if (p.Map[shipX, shipY - 1] != 0 || p.Map[shipX, shipY + 1] != 0 || p.Map[shipX + shipLength, shipY] != 0 ||
                            p.Map[shipX + shipLength, shipY + 1] != 0 || p.Map[shipX + shipLength, shipY - 1] != 0 ||
                            p.Map[shipX + shipLength - 1, shipY + 1] != 0 || p.Map[shipX + shipLength - 1, shipY - 1] != 0) collision = true;
                    if (shipY == 0 && shipX != 0 && shipX < 10 - shipLength)
                    {
                        if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX - 1, shipY + 1] != 0 ||
                            p.Map[shipX, shipY + 1] != 0 ||
                            p.Map[shipX + shipLength, shipY] != 0 || p.Map[shipX + shipLength, shipY + 1] != 0 ||
                            p.Map[shipX + shipLength - 1, shipY + 1] != 0) collision = true;
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (p.Map[shipX + i - 1, shipY + 1] != 0 || p.Map[shipX + i, shipY + 1] != 0
                                || p.Map[shipX + i + 1, shipY + 1] != 0) collision = true;
                        }
                    }
                    if (shipY == 9 && shipX != 0 && shipX < 10 - shipLength)
                    {
                        if (p.Map[shipX - 1, shipY] != 0 || p.Map[shipX - 1, shipY - 1] != 0 ||
                            p.Map[shipX, shipY - 1] != 0 ||
                            p.Map[shipX + shipLength, shipY] != 0 || p.Map[shipX + shipLength, shipY - 1] != 0 ||
                            p.Map[shipX + shipLength - 1, shipY - 1] != 0) collision = true;
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (p.Map[shipX + i - 1, shipY - 1] != 0 || p.Map[shipX + i, shipY - 1] != 0
                                || p.Map[shipX + i + 1, shipY - 1] != 0) collision = true;
                        }
                    }
                }
            }
            return collision;
        }
    }
}
