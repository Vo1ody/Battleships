using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Exam
{
    class Board
    {
        public List<Ship> Ships { get; set; }
        public int[,] Map { get; set; }
        public int[,] EnemyMap { get; set; }

        public Board()
        {
            Ships = new List<Ship>();
            Map = new int[10,10];
            EnemyMap = new int[10,10];
            Array.Clear(Map, 0, Map.Length);
            Array.Clear(EnemyMap, 0, EnemyMap.Length);
        }
    }
}
