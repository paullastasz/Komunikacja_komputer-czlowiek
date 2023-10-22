using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using GameProject.Models.Characters;

namespace GameProject
{
    internal class FileOperation
    {
        private string name;
        public FileOperation(string name) 
        { 
            this.name = name;
        }

        public void SerializeToFile(List<Player> players)
        {
            var serializedPlayers = JsonSerializer.Serialize(players);
            File.WriteAllText(name, serializedPlayers);
        }

        public List<Player> DeserializeFromFile()
        {
            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);
                
                var serializedPlayers = File.ReadAllText(fullPath);
                
                List<Player> players = JsonSerializer.Deserialize<List<Player>>(serializedPlayers)!;

                return players;

            }
            catch (Exception e)
            {
                Console.WriteLine("Wyjątek: " + e.Message);
            }
            
            return null;
        }

    }
}
