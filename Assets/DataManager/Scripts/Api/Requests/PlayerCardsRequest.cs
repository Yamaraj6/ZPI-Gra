using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.DataManager.Scripts.Models;

namespace Assets.DataManager.Scripts.Api.Requests
{
    public class PlayerCardsRequest
    {
        public int Id { get; set; }
        public List<PlayerCard> Cards { get; set; }
    }
}
