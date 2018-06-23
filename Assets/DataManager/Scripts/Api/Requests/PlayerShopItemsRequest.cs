using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.DataManager.Scripts.Models;

namespace Assets.DataManager.Scripts.Api.Requests
{
    public class PlayerShopItemsRequest
    {
        public int Id { get; set; }
        public List<PlayerShopItems> UserShopItems { get; set; }
    }
}
