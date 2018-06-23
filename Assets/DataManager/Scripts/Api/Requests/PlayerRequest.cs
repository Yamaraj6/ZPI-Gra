using System;

namespace Assets.DataManager.Scripts.Api.Responses
{
    public class PlayerRequest
    {
        public int Id { get; set; }

        public string FacebookId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FirstLogin { get; set; }
        public DateTime LastLogout { get; set; }
    }
}