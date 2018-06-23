using System;

namespace Assets.DataManager.Scripts.Models
{
    public class PlayerCard
    {
        public int Id { get; set; }

        public Element Element { get; set; }
        public int Power { get; set; }
        public int ManaCost { get; set; }
        public int CastingTime { get; set; }
        public DateTime DatePurchased { get; set; }

        public CardType CardType { get; set; }

    }
}
