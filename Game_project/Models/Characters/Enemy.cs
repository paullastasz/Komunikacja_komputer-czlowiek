using GameProject.Models.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Models.Characters
{
    internal class Enemy : Character
    {
        public Enemy()
        {
            Position = new Position(55, 7);
        }

        public void PrintIconEnemy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(@"■■■■■");
        }

        public void ClearIconEnemy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("     ");
        }
    }
}
