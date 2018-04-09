using UnityEngine;
using System.Collections;

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
    [HideInInspector]
    public float bossSmoothSpeed = 0.125f;

    private static bool bossArea = false;
    #region Instance
    private static CameraMove GetInstance { get; set; }

    private void Awake()
    {
        if (GetInstance == null)
        {
            GetInstance = this;
        }
    }
    #endregion

    void LateUpdate()
    {
        if (!bossArea || !bossLevel)
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
        Vector3 targetPosition = player.transform.TransformPoint(cameraOffset_boss_fromPlayer);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        gameObject.transform.position = smoothedPosition;

        Vector3 fwd = boss.transform.position + cameraOffset_boss_fromBoss - transform.position;
        Vector3 smoothedFwd = Vector3.Lerp(transform.forward, fwd, bossSmoothSpeed);
        transform.forward = smoothedFwd;
    }

    public static void SetBossArea(bool isInBossArea)
    {
        GetInstance.RunCoroutine();
        bossArea = isInBossArea;
    }

    private void RunCoroutine()
    {
        StartCoroutine(ChangingArea(2f));
    }

    private IEnumerator ChangingArea(float time)
    {
        float actual = bossSmoothSpeed;
        bossSmoothSpeed = actual / 10;
        yield return new WaitForSeconds(time);
        bossSmoothSpeed = actual;
    } 
}
