using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour {

    public int maxHP = 100;
    private int actualHP = 100;

    [Tooltip("Dodaj jeśli potrzebujesz wyświetlać liczbę HP")]
    public TextMesh text;
    
	void Start () {
        actualHP = maxHP;
	}

    public void ReduceHP(int hp_points)
    {
        actualHP -= hp_points;
        AfterReduce();
    }

    public void ReduceHP(float hp_percentage)
    {
        actualHP -= (int) (maxHP * hp_percentage);
        AfterReduce();
    }

    private void AfterReduce()
    {
        if (actualHP <= 0)
        {
            Destroy(gameObject);
        } else if (text != null)
        {
            text.text = "" + actualHP;
        }
    }

    private void OnDestroy()
    {
        Debug.Log(gameObject.name + "Destroyed");
    }

    /*
    Do spella dodać event na zderzeniu z bossem:
 
     if (collider.gameObject.tag == "Boss")
        {
            collider.gameObject.GetComponent<HPManager>().ReduceHP(damage);
        }

     */
}
