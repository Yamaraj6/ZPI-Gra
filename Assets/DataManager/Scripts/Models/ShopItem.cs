using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DataManager.Scripts.Models
{
    public class ShopItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public long Bonus { get; set; }
        public long Value { get; set; }
        public AvailabilityType AvailabilityType { get; set; }
    }

    public enum AvailabilityType
    {
        Basic = 0,
        Premium = 1
    }
}
