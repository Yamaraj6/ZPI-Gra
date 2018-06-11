using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    private CardManager cardManager;

    public Text nameText;

    public Image artworkImage;
    public Image markImage;
    public Image elementImage;

    public Text manaText;
    public Text powerText;
    public Text castTimeText;

    // Use this for initialization
    void Start ()
    {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();

        nameText.text = card.name;
        artworkImage.sprite = card.artwork;

        markImage.sprite = cardManager.GetMarkSprite(card.mark);
        elementImage.sprite = cardManager.GetElementSprite(card.element);

        manaText.text = card.manaCost.ToString();
        powerText.text = card.spellPower.ToString();
        castTimeText.text = card.spellCastTime.ToString();
	}
}
