using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform[] waypoints;
    public float speed = 5f;
    public float waitTime = 1.0f;

    private Vector3[] realWaypoints;
    private bool waiting = false;
    private int currentPoint = 0;

    private void Awake()
    {
        realWaypoints = new Vector3[waypoints.Length + 1];
        realWaypoints[0] = transform.position;
        for (int i = 0; i < waypoints.Length; i++)
        {
            realWaypoints[i + 1] = waypoints[i].position;
        }
    }

    private void FixedUpdate()
    {
        if (!waiting)
        {
            if (transform.position != realWaypoints[currentPoint])
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, realWaypoints[currentPoint], speed * Time.deltaTime);
            } else
            {
                if (++currentPoint >= realWaypoints.Length)
                    currentPoint = 0;

                StartCoroutine(Wait());
            }
        }
    }

    private IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }
}
