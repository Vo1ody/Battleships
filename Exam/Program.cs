using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random rnd = new Random();
            //Game game = new Game(rnd.Next(1,3));
            Game game = new Game(3);

            game.Run();
        }
    }
}
