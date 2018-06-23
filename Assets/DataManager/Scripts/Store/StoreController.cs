using System.Collections;
using System.Collections.Generic;
using Assets.DataManager.Scripts.Api;
using Assets.DataManager.Scripts.Api.Requests;
using Assets.DataManager.Scripts.Containers;
using Assets.Scripts.Containers;
using UnityEngine;
using ZPIGame.Assets.DataManager.Scripts.Configuration;
using ZPIGame.Assets.DataManager.Scripts.Containers;

namespace ZPIGame.Assets.DataManager.Scripts.Store
{

    public class StoreController : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _positiveCurve;
        [SerializeField]
        private AnimationCurve _negativeCurve;
        private IPlayerService _playerService;
        private IStoreConfiguration _storeConfiguration;
        private ICardContainer _cardContainer;
        private IPlayerContainer _playerContainer;

        private RandCardParameter _randCardParameter = new RandCardParameter();
        public void Awake()
        {
            _storeConfiguration = ContainerInstaller.DiContainer.Resolve<IStoreConfiguration>();
            _cardContainer = ContainerInstaller.DiContainer.Resolve<ICardContainer>();
            _playerContainer = ContainerInstaller.DiContainer.Resolve<IPlayerContainer>();
            _playerService = ContainerInstaller.DiContainer.Resolve<IPlayerService>();

            _randCardParameter.MaxManaCost = _storeConfiguration.MaxCardManaCost;
            _randCardParameter.MaxPower = _storeConfiguration.MaxCardPower;
            _randCardParameter.MaxCastingTime = _storeConfiguration.MaxCardManaCost;
            _randCardParameter.PositiveCurve = _positiveCurve;
            _randCardParameter.NegativeCurve = _negativeCurve;
        }


        public async void OnCardBought()
        {
            var newCard = _cardContainer.Cards.GetRandomCard(_randCardParameter);
            Debug.Log($"Succesfully purchased new card! Power: {newCard.Power}, mana cost: {newCard.ManaCost}, casting time: {newCard.CastingTime} ");

            _playerContainer.Player.Cards.Add(newCard);
            var addCardsResponse = await _playerService.AddCardsToPlayer(new PlayerCardsRequest()
            {
                Id = _playerContainer.Player.Id,
                Cards = _playerContainer.Player.Cards
            });

            if (addCardsResponse == null)
            {
                Debug.Log("Could not synchronize bought card with API :(");
                return;
            }

            Debug.Log("Succesfully synchronised your new card with API!");

        }


    }
}
