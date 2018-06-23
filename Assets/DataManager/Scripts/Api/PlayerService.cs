using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.DataManager.Scripts.Api.Requests;
using Assets.DataManager.Scripts.Api.Responses;
using Assets.DataManager.Scripts.Models;
using UnityEngine.Networking.NetworkSystem;

namespace Assets.DataManager.Scripts.Api
{
    public interface IPlayerService
    {
        Task<PlayerResponse> GetPlayerData(PlayerRequest request);
        Task<CreatePlayerResponse> CreateNewPlayer(PlayerRequest request);
        Task<PlayerResponse> UpdatePlayerInfo(PlayerRequest request);
        Task<PlayerResponse> AddCardsToPlayer(PlayerCardsRequest request);
        Task<PlayerResponse> AddShopItemsToPlayer(PlayerShopItemsRequest request);

    }

    public class PlayerService : IPlayerService
    {
        private readonly IServiceConnector _serviceConnector;

        public PlayerService(IServiceConnector serviceConnector)
        {
            _serviceConnector = serviceConnector;
        }

        public async Task<PlayerResponse> GetPlayerData(PlayerRequest request)
        {
            return await _serviceConnector.GetReportResponse<PlayerRequest, PlayerResponse>("api/user/get-user-info", request);
        }

        public async Task<CreatePlayerResponse> CreateNewPlayer(PlayerRequest request)
        {
            return await _serviceConnector.GetReportResponse<PlayerRequest, CreatePlayerResponse>("api/user/create-new-user", request);
        }

        public async Task<PlayerResponse> UpdatePlayerInfo(PlayerRequest request)
        {
            return await _serviceConnector.GetReportResponse<PlayerRequest, PlayerResponse>("api/user/update-user-info", request);
        }

        public async Task<PlayerResponse> AddCardsToPlayer(PlayerCardsRequest request)
        {
            return await _serviceConnector.GetReportResponse<PlayerCardsRequest, PlayerResponse>("api/user/add-cards-to-user", request);
        }

        public async Task<PlayerResponse> AddShopItemsToPlayer(PlayerShopItemsRequest request)
        {
            return await _serviceConnector.GetReportResponse<PlayerShopItemsRequest, PlayerResponse>("api/user/add-shop-item-to-user", request);
        }


    }
}
