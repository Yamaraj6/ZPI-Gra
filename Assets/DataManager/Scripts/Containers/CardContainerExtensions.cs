using System;
using System.Collections;
using System.Collections.Generic;
using Assets.DataManager.Scripts.Models;
using UnityEngine;
using System.Linq;
using ZPIGame.Assets.DataManager.Scripts.Containers;

public static class CardContainerExtensions
{
    public static PlayerCard GetRandomCard(this List<CardType> cardTypes, RandCardParameter randCardParameter)
    {
        var choosenType = UnityEngine.Random.Range(0, cardTypes.Count);
        var choosenCard = cardTypes[choosenType];
        return new PlayerCard()
        {
            CardType = choosenCard,
            DatePurchased = DateTime.Now,
            Element = (Element)UnityEngine.Random.Range(1, typeof(Element).GetEnumNames().Count()),
            Id = UnityEngine.Random.Range(1, 1000000000),
            ManaCost = Mathf.RoundToInt(randCardParameter.NegativeCurve.Evaluate(UnityEngine.Random.value) * randCardParameter.MaxManaCost),
            Power = Mathf.RoundToInt(randCardParameter.PositiveCurve.Evaluate(UnityEngine.Random.value) * randCardParameter.MaxPower),
            CastingTime = Mathf.RoundToInt(randCardParameter.NegativeCurve.Evaluate(UnityEngine.Random.value) * randCardParameter.MaxCastingTime)

        };
    }
}
