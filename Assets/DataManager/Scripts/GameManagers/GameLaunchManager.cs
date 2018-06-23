using Assets.DataManager.Scripts.Api;
using Assets.DataManager.Scripts.Api.Responses;
using Assets.Scripts.Containers;
using UnityEngine;

namespace Assets.DataManager.Scripts.GameManagers
{
    public class GameLaunchManager : MonoBehaviour
    {
        private IPlayerContainer _playerContainer;
        private IPlayerService _playerService;
        public async void Awake()
        {
            _playerContainer = ContainerInstaller.DiContainer.Resolve<IPlayerContainer>();
            _playerService = ContainerInstaller.DiContainer.Resolve<IPlayerService>();

            var playerResponse = await _playerService.GetPlayerData(new PlayerRequest()
            {
                Id = _playerContainer.Player.Id
            });

            if (playerResponse == null)
            {
                Debug.Log($"Could not get player data from api. Player id: {_playerContainer.Player.Id}");
            }

            _playerContainer.Player = playerResponse.Player;
            Debug.Log($"Succesfully downloaded player with id: {_playerContainer.Player.Id}");
            _playerContainer.SavePlayer();
        }

    }
}
