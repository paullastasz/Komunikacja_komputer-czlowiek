using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Models.Characters;
using GameProject.Views.Text;

namespace GameProject.Models.Menu
{
    internal class Ranking
    {

        private List<Player> _players;
        TextView _textView;
        AreaView _areaView;
        public Ranking(List<Player> players)
        {
            _textView = new TextView();
            _players = players;
            _areaView = new AreaView();
        }

        public void Show()
        {
            Console.Clear();
            int getPosition = _textView.GetPositionCenteringText("RANKING", 'R');

            if (_players is not null)
            {
                _players.Sort(Comparer<Player>.Create((player1, player2) => player1.CurrentPoints.CompareTo(player2.CurrentPoints)));
                _players.Reverse();

                int columnMaxWidth = FindColumnMaxWidth() + 4;
                _areaView.DrawArray(columnMaxWidth, _players, getPosition, LongestNamePlayer());
            }
            else
                _textView.Center("there are not any players on the list", 10);


            _areaView.DrawRankingPanel();
            _areaView.DrawFrame();

            if (Console.ReadKey().Key == ConsoleKey.N)
            {
                Console.Clear();
                MainMenu mainMenu = MainMenu.GetInstance;
                mainMenu.Run();
            }
        }

        private int FindColumnMaxWidth()
        {
            int columnRankWidth = _players.Count();
            int columnNickWidth = _players.Max(item => item.Nick.Length);
            int columnPointsWidth = _players.Max(item => item.CurrentPoints).ToString().Length;

            List<int> maxList = new List<int>() { columnRankWidth, columnNickWidth, columnPointsWidth };

            return maxList.Max(item => item);
        }

        private int LongestNamePlayer()
        {
            int index = 0;
            int maxLength = _players.Max(item => item.Nick.Length);

            foreach (var player in _players)
            {
                if (maxLength == player.Nick.Length)
                    break;
                else
                    index++;
            }
            return index;
        }
    }
}
