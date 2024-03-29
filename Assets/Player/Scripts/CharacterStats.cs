﻿
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int level;
    public float experience;
    public int lifes;

    public ResourceManager health;
    public ResourceManager mana;
    public float specialEffect = 0;
    
    public float spellCastDuration = 1;

    private Dictionary<Element, float> spellsPower; // in %
    private Dictionary<Element, float> resistances; // in %


    private void Awake()
    {
        spellsPower = new Dictionary<Element, float>();
        resistances = new Dictionary<Element, float>();
        foreach (var _element in Enum.GetNames(typeof(Element)))
        {
            Element _elem = (Element)Enum.Parse(typeof(Element), _element, true);
            spellsPower.Add(_elem, 100);
            resistances.Add(_elem, 0);
        }
    }

    public void Start()
    {
    }

    public void Update()
    {
        health.Update();
        mana.Update();
    }

    public float GetResistance(Element element)
    {
        return resistances[element];
    }

    public void DealDamage(Attack damage)
    {
        health.ReduceByValue(damage.power - GetResistance(damage.element));
    }

    public bool IsDead()
    {
        return health.IsEmpty();
    }
}