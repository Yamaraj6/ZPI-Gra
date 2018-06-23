using System;
using System.Threading.Tasks;
using Assets.DataManager.Scripts.Api;
using Assets.DataManager.Scripts.Api.Responses;
using Assets.DataManager.Scripts.Containers;
using Assets.Scripts.Containers;
using UnityEngine;
using ZPIGame.Assets.DataManager.Scripts.FirstLaunch;

namespace Assets.DataManager.Scripts.FirstLaunch
{
    public class FirstLaunchManager : MonoBehaviour
    {
        private IFirstLaunchChecker _firstLaunchChecker;
        private IPlayerService _playerService;
        private ICardService _cardService;
        private IPlayerContainer _playerContainer;
        private ICardContainer _cardContainer;

        public async void Awake()
        {
            _firstLaunchChecker = ContainerInstaller.DiContainer.Resolve<IFirstLaunchChecker>();
            if (_firstLaunchChecker.IsFirstLaunch)
            {
                return;
            }

            _playerService = ContainerInstaller.DiContainer.Resolve<IPlayerService>();
            _playerContainer = ContainerInstaller.DiContainer.Resolve<IPlayerContainer>();
            _cardService = ContainerInstaller.DiContainer.Resolve<ICardService>();
            _cardContainer = ContainerInstaller.DiContainer.Resolve<ICardContainer>();



            var newPlayerResult = await CreateNewPlayer();
            if (newPlayerResult == null)
            {
                return;
            }
            _playerContainer.Player.Id = newPlayerResult.Id;
            _playerContainer.SavePlayer();
            Debug.Log($"Created new player on server! Id: {newPlayerResult.Id}");


            var getCardsResult = await GetCards();
            if (getCardsResult == null)
            {
                return;
            }

            _cardContainer.Cards = getCardsResult.Cards;
            Debug.Log($"Downloaded cards from server!");

            _firstLaunchChecker.SetFirstLaunchToTrue();


        }

        private async Task<CreatePlayerResponse> CreateNewPlayer()
        {
            return await _playerService.CreateNewPlayer(new PlayerRequest()
            {
                // Id = newUniqueId,
                Country = _playerContainer.Player.Country,
                FacebookId = _playerContainer.Player.FacebookId,
                FirstLogin = DateTime.Today,
                ImageUrl = _playerContainer.Player.ImageUrl,
                LastLogout = DateTime.Now,
                Name = _playerContainer.Player.Name
            });
        }

        private async Task<CardsResponse> GetCards()
        {
            return await _cardService.GetAllCards();
        }

    }
}
