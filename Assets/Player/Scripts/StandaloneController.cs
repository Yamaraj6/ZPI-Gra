using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class StandaloneController : MonoBehaviour
{

#if UNITY_EDITOR || UNITY_STANDALONE
    void Update()
    {
        if (Input.GetKey("space"))
            CrossPlatformInputManager.SetButtonDown("Jump");
        else
            CrossPlatformInputManager.SetButtonUp("Jump");
        
        if (Input.GetKey("c"))
            CrossPlatformInputManager.SetButtonDown("Sneak");
        else
            CrossPlatformInputManager.SetButtonUp("Sneak");

        CrossPlatformInputManager.SetAxis("Horizontal", Input.GetAxis("Horizontal"));
        CrossPlatformInputManager.SetAxis("Vertical", Input.GetAxis("Vertical"));
    }
#endif
}