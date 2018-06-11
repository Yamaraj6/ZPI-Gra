using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField]
    private Transform followingObject;

    private void Update()
    {
        gameObject.transform.SetPositionAndRotation(followingObject.position, followingObject.rotation);
    }
}
