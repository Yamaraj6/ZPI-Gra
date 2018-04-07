using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    GameObject player;
    public Vector3 cameraOffset;
    public float smoothSpeed = 0.125f;

    public bool bossLevel;
    [HideInInspector]
    public GameObject boss;
    [HideInInspector]
    public Vector3 cameraOffset_boss_fromPlayer;
    [HideInInspector]
    public Vector3 cameraOffset_boss_fromBoss;


    void LateUpdate()
    {
        if (!bossLevel)
            MoveClassicCamera();
        else
            MoveBossCamera();
    }

    void MoveClassicCamera()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        gameObject.transform.position = smoothedPosition;
    }

    void MoveBossCamera()
    {        
        Vector3 targetPosition = player.transform.position + cameraOffset_boss_fromPlayer;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        gameObject.transform.position = smoothedPosition;
        
        transform.forward = boss.transform.position + cameraOffset_boss_fromBoss - transform.position;
    }
}
