﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    class EasyLevel:Logic
    {
        public override int TakeShot(Human player, Rendering rendering)
        {
            int[,] possibilityMap;
            int currentHighestX = 0;
            int currentHighestY = 0;
            int currentHighestScore = 0;
            int tempScore = 0;
            int turn;

            possibilityMap = CalculatePossiblePlacements();

            //clear all previously fired on tiles just to be safe
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (c_player.EnemyMap[x, y] != 0)
                    {
                        possibilityMap[x, y] = 0;
                    }
                }
            }

            //choose most likely location of the ship
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tempScore = possibilityMap[x, y];
                    if (tempScore > currentHighestScore)
                    {
                        currentHighestX = x;
                        currentHighestY = y;
                        currentHighestScore = tempScore;
                    }
                }
            }
            turn = player.SquareHit(currentHighestX, currentHighestY, c_player, rendering);
            return turn;
        }
        public override int[,] CalculatePossiblePlacements()
        {
            int[,] possibilityMap = new int[10, 10];
            Array.Clear(possibilityMap, 0, possibilityMap.Length);

            //Vertical placement possibilities
            for (int currentShipSize = 0; currentShipSize < 4; currentShipSize++)
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10 - currentShipSize; y++)
                    {
                        for (int c = 0; c < currentShipSize; c++)
                        {
                            if (UnknownSpaceCollision(x, y, currentShipSize, ShipOrientation.Vertical) == false)
                            {
                                possibilityMap[x, y + c]++; //increment the value in the possibility map, indicating a ship could be here.
                            }
                        }

                    }
                }

                //Horizontal placement possibilities
                for (int x = 0; x < 10 - currentShipSize; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {

                        for (int c = 0; c < currentShipSize; c++)
                        {
                            if (UnknownSpaceCollision(x, y, currentShipSize, ShipOrientation.Horizontal) == false)
                            {
                                possibilityMap[x + c, y]++; //increment the value in the possibility map, indicating a ship could be here.
                            }
                        }

                    }
                }
            }
            return possibilityMap;
        }
        public override bool UnknownSpaceCollision(int shipX, int shipY, int shipLength, ShipOrientation so)
        {
            bool collision = false;

            for (int c = 0; c < shipLength; c++)
            {
                if (so == ShipOrientation.Vertical)
                {
                    if (c_player.EnemyMap[shipX, shipY + c] != 0) //if the map square is not an unknown tile
                    {
                        collision = true;
                    }
                }
                else
                {
                    if (c_player.EnemyMap[shipX + c, shipY] != 0) //if the map square is not an unknown tile
                    {
                        collision = true;
                    }
                }
            }
            return collision;
        }

        public override int[,] CalculateTargetedPlacements()
        {
            throw new NotImplementedException();
        }

        public override int PlayerShipCollision(int shipX, int shipY, int shipLength, ShipOrientation so)
        {
            throw new NotImplementedException();
        }
    }
}
