using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.DataManager.Scripts.Api.Responses;

namespace Assets.DataManager.Scripts.Api
{
    public interface ICardService
    {
        Task<CardsResponse> GetAllCards();
    }

    public class CardService : ICardService
    {
        private readonly IServiceConnector _serviceConnector;

        public CardService(IServiceConnector serviceConnector)
        {
            _serviceConnector = serviceConnector;
        }

        public async Task<CardsResponse> GetAllCards()
        {
            return await _serviceConnector.GetReportResponse<CardsResponse>("api/card/all");
        }
    }
}
