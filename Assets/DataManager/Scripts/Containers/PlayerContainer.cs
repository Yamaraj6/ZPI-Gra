using System.IO;
using Assets.DataManager.Scripts.Models;
using Assets.Scripts.Containers;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.DataManager.Scripts.Containers
{
    public class PlayerContainer : IPlayerContainer
    {
        public Player Player { get; set; }

        private readonly string path = Application.persistentDataPath + @"/PlayerData.json";


        public PlayerContainer()
        {
           LoadPlayer();
        }

        private void LoadPlayer()
        {
            if (!File.Exists(path))
            {
                var xmlAsset = Resources.Load("PlayerData") as TextAsset;
                var jsonContent = xmlAsset.text;
                File.WriteAllText(path, jsonContent);

            }

            var jsonData = File.ReadAllText(path);
            Player = JsonConvert.DeserializeObject<Player>(jsonData);
        }

        public void SavePlayer()
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(Player));
        }

    }
}
