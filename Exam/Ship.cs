using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Exam
{
    public enum ShipOrientation
    {
        Horizontal,
        Vertical
    };
    class Ship
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public ShipOrientation Orientation { get; set; }
        public int[,] Coords { get; set; }
        public int HitCount { get; set; }
        public bool IsDrowned { get { return HitCount == Length; } }

        public Ship(string name, int length, ShipOrientation so)
        {
            Name = name;
            Coords = new int[length,2];
            Length = length;
            Orientation = so;
            HitCount = 0;
        }
        public void PlaceShip(int x, int y, ShipOrientation so)
        {
            for (int i = 0; i < Length; i++)
            {
                if (so == ShipOrientation.Vertical)
                {
                    Coords[i, 0] = x;
                    Coords[i, 1] = y + i;
                }
                else
                {
                    Coords[i, 0] = x + i;
                    Coords[i, 1] = y;
                }
            }
        }
        public void Rotate()
        {
            Orientation = (Orientation == ShipOrientation.Horizontal)
                ? ShipOrientation.Vertical
                : ShipOrientation.Horizontal;
        }
        public int ShipHit()
        {
            HitCount++;
            if (IsDrowned) return 1;
            return 0;
        }
    }
}
