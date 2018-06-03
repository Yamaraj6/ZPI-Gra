using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject damageDealer;
    public float power;
    public Element element;

    private void OnTriggerEnter(Collider other)
    {
        if(damageDealer==null || other.tag != damageDealer.tag)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
