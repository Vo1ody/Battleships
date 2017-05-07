using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    class Computer:Player
    {
        /// This method handles hits, determining which ships, if any, were hit and updating the instances accordingly.
        public int SquareHit(int posX, int posY, Human player, Rendering rendering)
        {
            int hitShipID;
            if (Map[posX, posY] != 0) //if the map square is a ship.
            {
                hitShipID = Map[posX, posY] - 1;
                if (Ships[hitShipID].ShipHit() == 0)
                {
                    rendering.UpdateLog("Your shot hits!");
                    player.EnemyMap[posX, posY] = 2;
                }
                else
                {
                    for (int count = 0; count < Ships[hitShipID].Length; count++)
                    {
                        player.EnemyMap[Ships[hitShipID].Coords[count, 0], Ships[hitShipID].Coords[count, 1]] = 3;
                    }
                    rendering.UpdateLog(Ships[hitShipID].Name + " destroyed!");
                    //player.EnemyMap[posX, posY] = 2;
                }
                return 0;
            }
            else
            {
                rendering.UpdateLog("Your shot misses!");
                player.EnemyMap[posX, posY] = 1;
                return -1;
            }
        }
        
    }
}
