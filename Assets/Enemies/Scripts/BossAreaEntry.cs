using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaEntry : MonoBehaviour {

    private bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !done)
        {
            CameraMove.SetBossArea(true);
            other.gameObject.GetComponent<CharacterControllerRB>().bossArea = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            done = true;
        }
    }


}
