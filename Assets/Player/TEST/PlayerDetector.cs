using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour {


    [SerializeField]
    private List<float> heights;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private Vector3 cubeSize;

    private List<BoxCaster> boxCasters;

    private BoxCaster forwardBoxCaster;
    private BoxCaster backwardBoxCaster;
    private BoxCaster upBoxCaster;
    private BoxCaster downBoxCaster;
    private BoxCaster rightBoxCaster;
    private BoxCaster leftBoxCaster;

    // Use this for initialization
    void Start()
    {
        boxCasters = new List<BoxCaster>();
        boxCasters.Add(forwardBoxCaster);
        boxCasters.Add(backwardBoxCaster);
        boxCasters.Add(upBoxCaster);
        boxCasters.Add(downBoxCaster);
        boxCasters.Add(rightBoxCaster);
        boxCasters.Add(leftBoxCaster);
        for (int i = 0; i < 6; i++)
        {
            boxCasters[i] = new BoxCaster(gameObject, heights[i], maxDistance, cubeSize,
                (Direction)i);
        }
    }
	
	void Update ()
    {
        foreach (var caster in boxCasters)
            caster.CheckCollisions();
    }

 /*   private void OnDrawGizmosSelected()
    {
        //foreach (var caster in boxCasters)
        //    caster.DrawCubeGizoms();
    }
    */
}