using System;
using System.Collections.Generic;

namespace Assets.DataManager.Scripts.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string FacebookId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FirstLogin { get; set; }
        public DateTime LastLogout { get; set; }


        public List<PlayerCard> Cards { get; set; }
        public List<PlayerShopItems> ShopItems { get; set; }
        public PlayerStats Stats { get; set; }
    }

    public class PlayerStats
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public decimal Money { get; set; }
        public long Diamonds { get; set; }
        //tu dodajemy kolejne potrzebne properties

        public DateTime ModificationTime { get; set; }

    }
}
