using GameProject.Models.Characters;
using GameProject.Models.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Views.Control
{
    internal class CharactersView
    {
        public void ShootingAnimation(int positionPlayerX, int positionPlayerY)
        {
            positionPlayerX += 2;
            for (int i = 1; i < 14; i++) 
            {
                Console.SetCursorPosition(positionPlayerX, positionPlayerY - i);
                Console.Write("|");
                Thread.Sleep(5);
                Console.SetCursorPosition(positionPlayerX, positionPlayerY - i);
                Console.Write(' ');
            }
        }

        public Player MoveLeft(Player player)
        {
            player.ClearIconPlayer(player.Position.X, player.Position.Y);
            player.Position.X = player.Position.X - 1;
            player.PrintIconPlayer(player.Position.X, player.Position.Y);

            return player;
        }

        public Player MoveRight(Player player)
        {
            player.ClearIconPlayer(player.Position.X, player.Position.Y);
            player.Position.X = player.Position.X + 1;
            player.PrintIconPlayer(player.Position.X, player.Position.Y);

            return player;
        }
    }
}
