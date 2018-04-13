using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    private bool casting = false;
    
	// Update is called once per frame
	void Update () {
        LookAtPlayer();
        if (!casting)
            StartCoroutine(CastSpell(5.0f));
	}

    void LookAtPlayer()
    {
        Vector3 _target = GameObject.FindGameObjectWithTag("Player").transform.position;
        _target.y = transform.position.y;
        transform.LookAt(_target);
    }

    IEnumerator CastSpell(float time)
    {
        casting = true;
        yield return new WaitForSeconds(time);
        casting = false;
        CastSpell();
    }

    private void CastSpell()
    {
        Vector3 vel = GetComponent<Rigidbody>().velocity;
        vel.y = 10;
        GetComponent<Rigidbody>().velocity = vel;
    }
}
