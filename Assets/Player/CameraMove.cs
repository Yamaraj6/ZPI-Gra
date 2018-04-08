using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    GameObject player;
	
	void Update ()
    {
        gameObject.transform.position = new Vector3(
            player.transform.position.x ,
            player.transform.position.y + 2,
            gameObject.transform.position.z);
    }
}
