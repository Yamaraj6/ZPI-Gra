using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DataManager.Scripts.Models
{
    public class PlayerShopItems
    {
        public int Id { get; set; }
        public long ItemLevel { get; set; }
        public DateTime DatePurchased { get; set; }

        public ShopItem ShopItem { get; set; }
    }
}
