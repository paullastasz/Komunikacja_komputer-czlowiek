using GameProject.Models.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Models.Characters
{
    internal class Character
    {
        public Position Position { get; set; }

        public Character() 
        {
            Position = new Position(0,0);
        }
    }
}
