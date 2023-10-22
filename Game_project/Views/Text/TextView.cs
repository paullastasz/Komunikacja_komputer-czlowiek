using GameProject.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Views.Text
{
    internal class TextView
    {
        public void Center(string text, int positionY)
        { 
            Console.SetCursorPosition(GetPositionCenteringText(text), positionY);
            Console.Write(text);
        }

        public int GetPositionCenteringText(string text)
        {
            string centeredText = text.PadLeft(text.Length + (Console.WindowWidth - text.Length) / 2).PadRight(Console.WindowWidth);

            int number = 0;
            foreach (char letter in centeredText) 
            {
                if (letter == ' ')
                    number++;
                else
                    break;
            }

            return number;

        }

        public int GetPositionCenteringText(string text, char selectedLetter)
        {
            string centerText = text.PadLeft(text.Length + (Console.WindowWidth - text.Length) / 2).PadRight(Console.WindowWidth);

            int position = 0;
            foreach (char letter in centerText)
            {
                if (selectedLetter != letter)
                    position++;
                else
                    break;
            }
            return position;
        }

        public void PrintTitleWindow(string title, int positionY)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Center(title, positionY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ResetColor();
        }

        public int GetPositionText(string text, string searchedWord)
        {
            return text.IndexOf(searchedWord) + 1;
        }

        public void DrawTopStatusBar(Player player)
        {
            Console.SetCursorPosition(1, 1);
            Console.Write("       Nick: " + player.Nick);
            Console.Write("       |       Level: " + player.Level);
            Console.Write("       |       Points: " + player.CurrentPoints);
            Console.Write("       |       Time left: ");
            Console.SetCursorPosition(1, 2);
            Console.Write(new string('-', Console.WindowWidth - 2));
        }

    }
}
