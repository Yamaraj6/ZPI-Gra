using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDestroyer : MonoBehaviour
{
    public float timeForDestruction;

    void Start()
    {
        Destroy(gameObject, timeForDestruction);
    }
}