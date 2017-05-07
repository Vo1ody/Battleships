using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    abstract class Player : Board
    {
        public bool AllShipsDestroyed()
        {
            for (int shipNumber = 0; shipNumber < 10; shipNumber++)
            {
                if (Ships[shipNumber].IsDrowned == false)
                {
                    return false;
                }
            }
            return true;
        }
        public int RemainingShips()
        {
            int totalShips = 0;

            for (int currentShip = 0; currentShip < 10; currentShip++)
            {
                if (!Ships[currentShip].IsDrowned)
                {
                    totalShips++;
                }
            }
            return totalShips;
        }
    }
}
