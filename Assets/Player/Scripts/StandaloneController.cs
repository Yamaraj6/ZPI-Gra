using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class StandaloneController : MonoBehaviour
{

#if UNITY_EDITOR || UNITY_STANDALONE
    void Update()
    {
        if (Input.GetKey("d"))
            CrossPlatformInputManager.SetButtonDown("Right");
        else
            CrossPlatformInputManager.SetButtonUp("Right");

        if (Input.GetKey("a"))
            CrossPlatformInputManager.SetButtonDown("Left");
        else
            CrossPlatformInputManager.SetButtonUp("Left");

        if (Input.GetKey("space"))
            CrossPlatformInputManager.SetButtonDown("Jump");
        else
            CrossPlatformInputManager.SetButtonUp("Jump");

        CrossPlatformInputManager.SetAxis("Horizontal", Input.GetAxis("Horizontal"));     
    }
#endif
}