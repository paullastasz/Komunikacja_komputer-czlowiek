using GameProject.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Views.Text;
using GameProject.Views.Control;
using GameProject.Models.Characters;

namespace GameProject.Controllers
{
    internal class MenuController
    {
        public List<Player> players { get; set; }

        private TextView _textView;
        private MenuView _menuView;
        private AreaView _areaView;
        private int _modeGame;

        public MenuController(int modeGame)
        {
            players = new List<Player>();
            _textView = new TextView();
            _modeGame = modeGame;
            _menuView = new MenuView();
            _areaView = new AreaView();
        }

        public void NewGame()
        {
            Console.Clear();
            _areaView.DrawFrame();
            _areaView.DrawNewGamePanel();

            var key = ConsoleKey.X;

            while (key != ConsoleKey.Y && key != ConsoleKey.N)
            {
                key = Console.ReadKey(intercept: true).Key;

            }

            if (key == ConsoleKey.N)
            {
                MainMenu mainMenu = MainMenu.GetInstance;
                mainMenu.Run();
            }
            else if (key == ConsoleKey.Y)
            {
                _textView.Center("Enter nickname ", 10);
                Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 12);
                string nick = Console.ReadLine();

                while (nick.Length == 0 || nick == null)
                {
                    _textView.Center("Nickname is empty. Enter again", 11);
                    Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 12);

                    nick = Console.ReadLine();

                }

                Player player = new Player(nick);
                players.Add(player);
                GameplayController gameplay = new GameplayController(player, _modeGame);
                gameplay.Play();
            }

        }

        public void Continue()
        {
            Console.Clear();
            _areaView.DrawFrame();
            _areaView.DrawContinuePanel();

            if (players is not null && players.Count > 0)
            {
                int index = 0;
                index = _menuView.MoveUpDownOnListTemplate(players);

                GameplayController gameplay = new GameplayController(players[index], _modeGame);
                gameplay.Play();

            }
            else
            {
                _textView.Center("List is empty. In about 7 seconds you will be taken to a new game.", 10);

                Thread.Sleep(7000);

                NewGame();
            }
        }
        public void Ranking()
        {
            Ranking ranking = new Ranking(players);
            ranking.Show();
        }

        public void Settings()
        {
            Console.Clear();
            _areaView.DrawFrame();

            List<string> settings = new List<string>() { "EASY", "NORMAL", "HARD" };
            _modeGame = _menuView.MoveUpDownOnList(settings, 1);

            MainMenu mainMenu = MainMenu.GetInstance;
            mainMenu.ChangeModeGame(_modeGame);
            mainMenu.Run();
        }

        public void SetModeGame(int mode)
        {
            _modeGame = mode;
        }

        internal void UpdatePlayer(Player playerToUpdate)
        {
            foreach (Player playerOnList in players)
            {
                if (playerOnList.Nick == playerToUpdate.Nick)
                {
                    playerOnList.CurrentPoints = playerToUpdate.CurrentPoints;
                    playerOnList.Level = playerToUpdate.Level;
                }
            }
        }

        internal void UpdateDatebase(List<Player> players)
        {
            if (players is not null)
                this.players = players;
        }
    }
}
