using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        LookAtPlayer();
	}

    void LookAtPlayer()
    {
        Vector3 _target = GameObject.FindGameObjectWithTag("Player").transform.position;
        _target.y = transform.position.y;
        transform.LookAt(_target);
    }
}
