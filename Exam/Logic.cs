using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    abstract class Logic
    {
        public Computer c_player;
        public Logic()
        {
            c_player = new Computer();
        }
        public bool Hunting { get; set; }
        /// This method handles the computer's turn.
        public abstract int TakeShot(Human player, Rendering rendering);
        /// This method works out the number of times a tile could possibly be occupied by a ship.
        public abstract int[,] CalculatePossiblePlacements();
        /// This method is invoked when the AI knows the general location of a ship, to whittle down the remaining squares it occupies.
        public abstract int[,] CalculateTargetedPlacements();
        /// This method checks to see if a hypothetical ship would collide with anything excpet unknown tiles.
        public abstract bool UnknownSpaceCollision(int shipX, int shipY, int shipLength, ShipOrientation so);
        /// This method checks to see if a hypothetical ship placement in the player grid would collide with a known ship. It returns the number of ship tiles the placement collides with.
        public abstract int PlayerShipCollision(int shipX, int shipY, int shipLength, ShipOrientation so);
    }
}
