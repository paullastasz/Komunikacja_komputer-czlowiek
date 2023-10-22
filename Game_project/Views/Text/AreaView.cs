using GameProject.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Views.Text
{
    internal class AreaView
    {
        private string _nameColumn1;
        private string _nameColumn2;
        private string _nameColumn3;

        private TextView _textView;

        public AreaView()
        {
            _nameColumn1 = "RANK";
            _nameColumn2 = "NICK";
            _nameColumn3 = "POINTS";
            _textView = new TextView();
        }
        private void DrawLineHorizontally(int length)
        {
            for (int i = 0; i < length; i++)
                Console.Write('-');
        }

        private void DrawLinesHorizontally(int startDrawPosition, int length)
        {

            Console.Write(new string(' ', startDrawPosition) + '+');
            DrawLineHorizontally(length);
            Console.Write('+');
            DrawLineHorizontally(length);
            Console.Write('+');
            DrawLineHorizontally(length);
            Console.WriteLine('+');
        }

        private void DrawColumns(int startDrawPosition, string column1, string column2, string column3, int length)
        {
            Console.Write(new string(' ', startDrawPosition));
            Console.Write("| " + column1 + new string(' ', length - column1.Length - 1));
            Console.Write("| " + column2 + new string(' ', length - column2.Length - 1));
            Console.WriteLine("| " + column3 + new string(' ', length - column3.Length - 1) + "|");
            
        }

        public void DrawArray(int length, List<Player> players, int position, int hintStartPosition)
        {
            string name = players[hintStartPosition].Nick;
            int lengthName = name.Length;
            int startDrawPosition = position - length - (lengthName / 2);

            if (lengthName == 3 || lengthName == 5)
            { 
                startDrawPosition = startDrawPosition - 1;
            }
            else if (lengthName == 2)
            {
                startDrawPosition = startDrawPosition - 3;
            }
            else if (lengthName == 1)
            {
                startDrawPosition = startDrawPosition - 6;
            }

            if (length - _nameColumn3.Length - 1 <= 0)
            {
                if(lengthName == 2)
                    length += 2;
                else if (lengthName == 1)
                    length += 3;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            DrawLinesHorizontally(startDrawPosition, length);
            DrawColumns(startDrawPosition, _nameColumn1, _nameColumn2, _nameColumn3, length);
            DrawLinesHorizontally(startDrawPosition, length);

            int i = 1;

            foreach (Player player in players)
            {
                DrawColumns(startDrawPosition, i.ToString(), player.Nick, player.CurrentPoints.ToString(), length);
                i++;
            }

            DrawLinesHorizontally(startDrawPosition, length);

        }

        public void DrawFrame()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.Write(new string('-', Console.WindowWidth));

            for (int i = 1; i < Console.WindowHeight - 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write('|');
                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write('|');
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write(new string('-', Console.WindowWidth));

        }

        public void DrawMenuPanel(List<string> choices)
        {

            _textView.PrintTitleWindow(@".-----..-. .-. .----.  .----.  .---. .----..-----.", 3);
            _textView.PrintTitleWindow(@"{ {__  | {_} |/  {}  \/  {}  \{_   _}| {_  | {}  }", 4);
            _textView.PrintTitleWindow(@".-._} }| { } |\      /\      /  | |  | {__ | .-. \", 5);
            _textView.PrintTitleWindow(@"`----' `-' `-' `----'  `----'   `-'  `----'`-' `-'", 6);

            int position = 10;
            foreach (string choice in choices)
            {
                _textView.Center(choice, position);
                position++;
            }

            _textView.Center("To proceed, use the up and down arrow keys.", 23);
        }

        public void DrawNewGamePanel()
        {

            _textView.PrintTitleWindow("PLAYER CREATION WINDOW", 3);
            _textView.Center("press \"n/N\" to cancel and go back to the menu", 5);
            _textView.Center("or press \"y/Y\" to enter nickname", 6);
        }

        public void DrawContinuePanel()
        {

            _textView.PrintTitleWindow("PLAYER LIST", 3);
            _textView.Center("To proceed, use the up and down arrow keys.", 23);
        }

        public void DrawRankingPanel()
        {
            _textView.PrintTitleWindow("RANKING", 3);
            _textView.Center("press \"n/N\" to cancel and go back to the menu", 23);
        }

        public void DrawSettingsPanel(List<string> choices)
        {

            _textView.PrintTitleWindow("SETTINGS", 3);
            _textView.Center("Select a game mode", 5);

            int position = 10;
            foreach (string choice in choices)
            {
                _textView.Center(choice, position);
                position++;
            }

            _textView.Center("To proceed, use the up and down arrow keys.", 23);
        }

        public void DrawGameplayPanel(Player player)
        {
            DrawFrame();
            _textView.DrawTopStatusBar(player); 
        }

        public void SetTimeOnBar(Player player, int countdown)
        {
            string text = "       Nick: " + player.Nick + "       |       Level: " + player.Level + "       |       Points: " + player.CurrentPoints + "       |       Time left: ";
            int position = _textView.GetPositionText(text, "Time left");

            if (player.CurrentPoints < 10)
                position = position + 11;
            else if (player.CurrentPoints >= 10 && player.CurrentPoints < 100)
                position = position + 10;
            else if (player.CurrentPoints >= 100 && player.CurrentPoints < 1000)
                position = position + 9;

           /* if (player.Level >= 10 && player.Level < 100)
                position = position - 1;
            else if (player.CurrentPoints >= 100 && player.CurrentPoints < 1000)
                position = position - 2;*/

            Console.SetCursorPosition(position, 1);

            if ((countdown + 1) % 10 == 0)
            {
                Console.Write("    ");
            }

            Console.SetCursorPosition(position , 1);
            Console.Write(countdown);
        }

        internal Player SetPointsOnBar(Player player, int points)
        {
            string text = "       Nick: " + player.Nick + "       |       Level: " + player.Level + "       |       Points: " + player.CurrentPoints;
            int position = _textView.GetPositionText(text, "Points") + 8;

            player.CurrentPoints += points;

            /*if (player.Level >= 10 && player.Level < 100)
                position = position - 1;
            else if (player.CurrentPoints >= 100 && player.CurrentPoints < 1000)
                position = position - 2;*/

            Console.SetCursorPosition(position, 1);
            Console.Write(player.CurrentPoints);

            return player;
        }
    }
}
