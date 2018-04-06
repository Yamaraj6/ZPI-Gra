using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    GameObject player;
    public Vector3 cameraOffset;
    public float smoothSpeed = 0.125f;
	
	void LateUpdate ()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        gameObject.transform.position = smoothedPosition;
    }
}
