using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Views.Text;
using GameProject.Views.Control;
using GameProject.Controllers;
using GameProject.Models.Characters;
using System.Text.Json;

namespace GameProject.Models.Menu
{
    internal class MainMenu
    {
        int index;

        List<string> choices = new List<string>() { "New Game", "Continue", "Ranking ", "Settings", "Quit" };

        TextView text;

        MenuView menuView;

        MenuController gameplay;


        private static MainMenu? instance = null;
        private MainMenu()
        {
            index = 0;
            text = new TextView();
            menuView = new MenuView();
            gameplay = new MenuController(1);
        }

        public static MainMenu GetInstance => instance ?? (instance = new MainMenu());

        public void PressedEnterOnChoice()
        {
            switch (index)
            {
                case 0:
                    gameplay.NewGame();
                    break;
                case 1:
                    gameplay.Continue();
                    break;
                case 2:
                    gameplay.Ranking();
                    break;
                case 3:
                    gameplay.Settings();
                    break;
                case 4:
                    Quit();
                    break;
            }
        }

        private void Quit()
        {
            FileOperation fileOperation = new FileOperation("DatabasePlayers.json");
            fileOperation.SerializeToFile(gameplay.players);
            Console.Clear();
            Environment.Exit(0);
        }

        public void Run()
        {
            index = menuView.MoveUpDownOnList(choices, 0);
            PressedEnterOnChoice();
        }

        public void ChangeModeGame(int mode)
        {
            gameplay.SetModeGame(mode);
        }

        public void UpdatePlayer(Player player)
        {
            gameplay.UpdatePlayer(player);
        }

        internal void UpdateDatabasePlayers(List<Player> players)
        {
            gameplay.UpdateDatebase(players);
        }
    }
}
