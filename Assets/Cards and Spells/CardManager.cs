using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Sprite[] imgMarks;
    public Sprite[] imgElements;

    public Dictionary<Mark, Sprite> marks;
    public Dictionary<Element, Sprite> elements;

    // Use this for initialization
    void Awake ()
    {
        marks = new Dictionary<Mark, Sprite>();
        elements = new Dictionary<Element, Sprite>();

        foreach (var mark in imgMarks)
        {
            marks.Add((Mark)Enum.Parse(typeof(Mark), mark.name, true), mark);
        }

        foreach (var element in imgElements)
        {
            elements.Add((Element)Enum.Parse(typeof(Element), element.name, true), element);
        }
    }

    public Sprite GetMarkSprite(Mark mark)
    {
        return marks[mark];
    }

    public Sprite GetElementSprite(Element element)
    {
        return elements[element];
    }
}
