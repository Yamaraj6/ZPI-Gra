using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCaster
{
    public GameObject currentHitObject { get; private set; }
    public float currentHitDistance { get; private set; }

    private GameObject gameObject;
    private float high;
    private float maxDistance;
    private Vector3 cubeSize;

    private LayerMask layerMask = -1;
    private Direction direction;
    private Vector3 directionVector;
    private Vector3 origin;

    public BoxCaster(GameObject gameObject, float high, float maxDistance, 
        Vector3 cubeSize, Direction direction)
    {
        this.gameObject = gameObject;
        this.high = high;
        this.maxDistance = maxDistance;
        this.cubeSize = cubeSize;
        this.direction = direction;
    }
    

    public void CheckCollisions()
    {
        directionVector = GetDirectionVector();
        origin = gameObject.transform.position;
        origin.y += high;
        RaycastHit hit;
        if (Physics.BoxCast(origin, cubeSize, directionVector, out hit, gameObject.transform.rotation, 
            maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
        }

    }

    public void DrawCubeGizoms()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + directionVector * currentHitDistance);
        Gizmos.DrawWireCube(origin + directionVector * currentHitDistance, cubeSize);
    }

    private Vector3 GetDirectionVector()
    {
        var _forward = gameObject.transform.forward;
        switch (direction)
        {
            case Direction.Forward:
                return _forward;
            case Direction.Backward:
                return -_forward;
            case Direction.Down:
                return new Vector3(_forward.x, _forward.z, _forward.y);
            case Direction.Up:
                return new Vector3(_forward.x, _forward.z, -_forward.y);
            case Direction.Right:
                return new Vector3(_forward.x, _forward.y, _forward.z);
            default:
                return new Vector3(_forward.x, _forward.z, -_forward.y);
        }
    }
}