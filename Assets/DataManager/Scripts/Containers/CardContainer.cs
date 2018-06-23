using System.Collections.Generic;
using System.IO;
using Assets.DataManager.Scripts.Api.Responses;
using Assets.DataManager.Scripts.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.DataManager.Scripts.Containers
{
    public interface ICardContainer
    {
        List<CardType> Cards { get; set; }
        void SaveCards();
    }
    public class CardContainer : ICardContainer
    {
        public List<CardType> Cards { get; set; }
        private readonly string path = Application.persistentDataPath + @"/CardsData.json";

        public CardContainer()
        {
            LoadCards();
        }

        private void LoadCards()
        {
            if (!File.Exists(path))
            {
                var jsonAsset = Resources.Load("CardsData") as TextAsset;
                var jsonContent = jsonAsset.text;
                File.WriteAllText(path, jsonContent);

            }

            var jsonData = File.ReadAllText(path);
            Cards = JsonConvert.DeserializeObject<List<CardType>>(jsonData);
        }

        public void SaveCards()
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(Cards));
        }

    }
}