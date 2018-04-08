using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class StandaloneController : MonoBehaviour
{

#if UNITY_EDITOR || UNITY_STANDALONE
    void Update()
    {

        CrossPlatformInputManager.SetAxis("Horizontal", Input.GetAxis("Horizontal"));

        CrossPlatformInputManager.SetAxis("Vertical", Input.GetAxis("Vertical"));

    }
#endif
}