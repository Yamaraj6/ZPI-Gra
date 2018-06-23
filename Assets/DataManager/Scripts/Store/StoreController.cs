using System.Collections;
using System.Collections.Generic;
using Assets.DataManager.Scripts.Containers;
using UnityEngine;
using ZPIGame.Assets.DataManager.Scripts.Configuration;

namespace ZPIGame.Assets.DataManager.Scripts.Store
{

    public class StoreController : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _positiveCurve;
        [SerializeField]
        private AnimationCurve _negativeCurve;
        private IStoreConfiguration _storeConfiguration;
        private ICardContainer _cardContainer;
        private int _cardsMaxPower;
        private int _cardsMaxManaCost;
        private int _cardMaxCastingTime;
        public void Awake()
        {
            _storeConfiguration = ContainerInstaller.DiContainer.Resolve<IStoreConfiguration>();
            _cardsMaxManaCost = _storeConfiguration.MaxCardManaCost;
            _cardsMaxPower = _storeConfiguration.MaxCardPower;
            _cardMaxCastingTime = _storeConfiguration.MaxCardManaCost;

        }


        public void OnCardBought()
        {
            _cardContainer = ContainerInstaller.DiContainer.Resolve<ICardContainer>();
            var newCard = _cardContainer.Cards.GetRandomCard(new Containers.RandCardParameter()
            {
                PositiveCurve = _positiveCurve,
                NegativeCurve = _negativeCurve,
                MaxPower = _cardsMaxPower,
                MaxManaCost = _cardsMaxManaCost,
                MaxCastingTime = _cardMaxCastingTime,
            });
            Debug.Log($"Succesfully purchased new card! Power: {newCard.Power}, mana cost: {newCard.ManaCost}, casting time: {newCard.CastingTime} ");

        }


    }
}
