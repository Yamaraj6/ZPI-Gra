using Assets.Scripts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Containers
{
    public class PlayerContainer : IPlayerContainer
    {
        public Player Player { get; set; }

        private string path = Application.persistentDataPath + @"/PlayerData.json";


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
