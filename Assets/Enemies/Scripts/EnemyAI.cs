using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour {

    /*
     * Patroluje teren między 2 punktami.
     * Jeśli zobaczy gracza na tym terenie, to biegnie w jego stronę na pewną odległość.
     */

    [Tooltip("Jak daleko widzi gracza")]
    public float patrolDistance = 3.0f;
    [Tooltip("Na jaką odległość zbliża się do gracza")]
    public float runOffset = 0.3f;
    public float moveSpeed = 4.0f;
    public float runSpeed = 6.0f;

    public float waitTime = 1.0f;
    public Transform[] waypoints;

    private Vector3[] realWaypoints;
    private bool waiting = false;
    private int currentPoint = 0;
    private CharacterController controller;
    private GameObject player;
    private bool seePlayer = false;

    void Awake () {
        realWaypoints = new Vector3[waypoints.Length + 1];
        realWaypoints[0] = transform.position;
        for (int i = 0; i < waypoints.Length; i++)
        {
            realWaypoints[i + 1] = waypoints[i].position;
        }

        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        SeePlayer();
        //Debug.Log(string.Format("Waiting: {0}", waiting));
        if (seePlayer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > runOffset)
                MoveToPlayer();
        }
        else if (!waiting)
        {
            if (transform.position.x != realWaypoints[currentPoint].x)
            {
                MoveToNextPoint();
            }
            else
            {
                ChangeTargetPoint(true);
            }
        }
    }

    private void SeePlayer()
    {
        seePlayer = false;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * patrolDistance, Color.white, Time.deltaTime);
        RaycastHit[] hitted = Physics.RaycastAll(ray, patrolDistance);

        foreach (RaycastHit rch in hitted)
        {
            if (rch.collider.CompareTag("Player"))
                seePlayer = true;
        }
    }

    private void MoveToNextPoint()
    {
        float maxDistance = moveSpeed * Time.deltaTime;
        Vector3 motion = Vector3.MoveTowards(transform.position, realWaypoints[currentPoint], maxDistance);
        motion -= transform.position;
        motion.y = -maxDistance;

       // Debug.Log(string.Format("Motion: {0}", motion.ToString()));

        controller.Move(motion);
    }

    private void MoveToPlayer()
    {
        float maxDistance = runSpeed * Time.deltaTime;
        Vector3 motion = Vector3.MoveTowards(transform.position, player.transform.position, maxDistance);
        motion -= transform.position;
        motion.y = -maxDistance;
        motion.z = 0;

        Debug.Log(string.Format("Motion To Player: {0}", motion.ToString()));

        controller.Move(motion);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
      //  Debug.Log(string.Format("Hit: Point: {0}, MoveDir: {1}, Position: {2}", hit.point, "X: " + hit.moveDirection.x + " Y: " + hit.moveDirection.y, hit.transform.position));
        if (hit.moveDirection.y == 0) {
            //Debug.Log("Controller Collider Hit, current Point: " + currentPoint);
            ChangeTargetPoint(true);
            //Debug.Log("After change: current Point: " + currentPoint);
        }
    }

    private void ChangeTargetPoint(bool wait)
    {
        if (++currentPoint >= realWaypoints.Length)
            currentPoint = 0;

        StartCoroutine(Wait());
    }

    private void LookAtTarget()
    {
        Vector3 target = realWaypoints[currentPoint];
        target.y = transform.position.y;
        target.z = transform.position.z;
        transform.LookAt(target);
    }

    private IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;

        LookAtTarget();
    }
}
