using GameProject;
using GameProject.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameProject.Models.Characters;
using System.Xml.Linq;

public class Program
{
    public static void Main(string[] args)
    {

        FileOperation fileOperation = new FileOperation("DatabasePlayers.json");
        List<Player> players = fileOperation.DeserializeFromFile();

        Console.Write("\u001b[?25l"); //ukryj kursor

        MainMenu mainMenu = MainMenu.GetInstance;
        mainMenu.UpdateDatabasePlayers(players);
        mainMenu.Run();
    }
}