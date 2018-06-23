using UnityEngine;
using Assets.DataManager.Scripts.Api;
using Assets.Scripts.Containers;
using Assets.DataManager.Scripts.Api.Responses;
using Assets.DataManager.Scripts;
using ZPIGame.Assets.DataManager.Scripts.AntiCheat;

namespace ZPIGame.Assets.DataManager.Scripts.GameManagers
{
    public class GameSynchronizingManager : MonoBehaviour
    {
        private IAntiCheatLogic _antiCheatLogic;
        private IPlayerService _playerService;
        private IPlayerContainer _playerContainer;
        private float _actualTimer = 0;
        public float SyncingOffset { get; set; } = 30;

        private void Start()
        {
            _antiCheatLogic = ContainerInstaller.DiContainer.Resolve<IAntiCheatLogic>();
            _playerService = ContainerInstaller.DiContainer.Resolve<IPlayerService>();
            _playerContainer = ContainerInstaller.DiContainer.Resolve<IPlayerContainer>();
        }

        private async void Update()
        {
            if (!Timer(Time.deltaTime))
                return;

            var playerResponse = await _playerService.GetPlayerData(new PlayerRequest()
            {
                Id = _playerContainer.Player.Id
            });

            if (playerResponse == null)
                return;

            if (!_antiCheatLogic.CheckIfCheated(playerResponse.Player, _playerContainer.Player))
            {
                _playerContainer.Player = playerResponse.Player;
            }


            await _playerService.UpdatePlayerInfo(new PlayerRequest()
            {
                //         Id = _playerContainer.Player.Id
                //TODO: update player info/stats etc ...
            });



        }



        private bool Timer(float deltaTime)
        {
            if (_actualTimer >= SyncingOffset)
            {
                _actualTimer = 0;
                return true;
            }

            _actualTimer += deltaTime;
            return false;
        }
    }
}