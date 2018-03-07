using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Money { get; set; }
        public int Health { get; set; }
        public List<Card> EquippedCards { get; set; }



    }
}
