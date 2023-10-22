using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Models.Movement;

namespace GameProject.Models.Characters
{
    internal class Player : Character
    {
        public string Nick { get; set; }
        public int CurrentPoints { get; set; }
        public int Level { get; set; }

        public Player(string Nick)
        {
            Position = new Position((Console.WindowWidth / 2) - 4, 20);
            this.Nick = Nick;
            CurrentPoints = 0;
            Level = 0;
        }

        public void PrintIconPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("  |");
            Console.SetCursorPosition(x, y + 1);
            Console.Write(" | |");
            Console.SetCursorPosition(x, y + 2);
            Console.Write(@" |_|\");
            Console.SetCursorPosition(x, y + 3);
            Console.Write(" __  |");
            Console.SetCursorPosition(x, y + 4);
            Console.Write(@"/``\ |");
            Console.SetCursorPosition(x, y + 5);
            Console.Write(@"\__/_|");
            Console.SetCursorPosition(x, y + 6);
            Console.Write("|  |");
        }

        public void ClearIconPlayer(int x, int y)
        {
            Console.SetCursorPosition(x + 2, y);
            Console.Write(' ');
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("   ");
            Console.SetCursorPosition(x + 1, y + 2);
            Console.Write("    ");
            Console.SetCursorPosition(x + 1, y + 3);
            Console.Write("     ");
            Console.SetCursorPosition(x, y + 4);
            Console.Write("      ");
            Console.SetCursorPosition(x, y + 5);
            Console.Write("      ");
            Console.SetCursorPosition(x, y + 6);
            Console.Write("    ");
        }

        public override string ToString()
        {
            return Nick;
        }
        
    }
}
