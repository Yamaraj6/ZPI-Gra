using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    GameObject player;
	
	void Update ()
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y, 
            player.transform.position.z+2);
    }
}
