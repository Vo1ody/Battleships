using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    static class AutoArragement
    {
        static private Random rand;

        static AutoArragement()
        {
            rand = new Random();
        }
        static public void ArrangeCompleted(Rendering r)
        {
            r.UpdateLog("All ships placed!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(25, 5);
            Console.Write("                    ");
            Console.SetCursorPosition(25, 4);
            Console.Write("                    ");
        }
        static public void Arrange(ref Computer p)
        {
            ShipAdd(ref p);
            PlaceShips(p);
        }
        static public void Arrange(ref Human p)
        {
            ShipAdd(ref p);
            PlaceShips(p);
        }
        static private void ShipAdd(ref Computer p)
        {
            p.Ships.Add(new Ship("Four-decker", 4, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Three-decker", 3, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Three-decker", 3, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Two-decker", 2, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Two-decker", 2, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Two-decker", 2, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
        }
        static private void ShipAdd(ref Human p)
        {
            p.Ships.Add(new Ship("Four-decker", 4, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Three-decker", 3, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Three-decker", 3, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Two-decker", 2, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Two-decker", 2, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Two-decker", 2, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
            p.Ships.Add(new Ship("Single-decker", 1, (ShipOrientation)rand.Next(2)));
        }
        static private void PlaceShips(Computer p)
        {
            int length;
            int shipX, shipY;
            ShipOrientation so;
            bool shipPlaced;

            for (int shipNumber = 0; shipNumber < 10; shipNumber++)
            {
                shipPlaced = false;
                length = p.Ships[shipNumber].Length;
                so = p.Ships[shipNumber].Orientation;

                while (shipPlaced == false)
                {
                    if (so == ShipOrientation.Vertical)
                    {
                        shipX = rand.Next(0, 10);
                        shipY = rand.Next(0, 10 - length);
                    }
                    else
                    {
                        shipX = rand.Next(0, 10 - length);
                        shipY = rand.Next(0, 10);
                    }

                    if (ShipCollision(shipX, shipY, length, so, p) == false)
                    {
                        for (int i = 0; i < length; i++)
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
                }
            }
        }
        static private void PlaceShips(Human p)
        {
            int length;
            int shipX, shipY;
            ShipOrientation so;
            bool shipPlaced;

            for (int shipNumber = 0; shipNumber < 10; shipNumber++)
            {
                shipPlaced = false;
                length = p.Ships[shipNumber].Length;
                so = p.Ships[shipNumber].Orientation;

                while (shipPlaced == false)
                {
                    if (so == ShipOrientation.Vertical)
                    {
                        shipX = rand.Next(0, 10);
                        shipY = rand.Next(0, 10 - length);
                    }
                    else
                    {
                        shipX = rand.Next(0, 10 - length);
                        shipY = rand.Next(0, 10);
                    }

                    if (ShipCollision(shipX, shipY, length, so, p) == false)
                    {
                        for (int i = 0; i < length; i++)
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
                }
            }
        }
        /// This method returns true if the ships current placement would result in a collision with an existing ship.
        static private bool ShipCollision(int shipX, int shipY, int shipLength, ShipOrientation so, Computer p)
        {
            bool collision = false;
            if (shipLength == 1)
            {
                if (shipX != 0 && shipY != 0 && shipX != 9 && shipY != 9)
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
        static private bool ShipCollision(int shipX, int shipY, int shipLength, ShipOrientation so, Human p)
        {
            bool collision = false;
            if (shipLength == 1)
            {
                if (shipX != 0 && shipY != 0 && shipX != 9 && shipY != 9)
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
