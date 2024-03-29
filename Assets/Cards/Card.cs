﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;
    public Mark mark;
    public Element element;

    public int manaCost;
    public int spellPower;
    public int spellCastTime;
}
