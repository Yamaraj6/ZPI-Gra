using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    GameObject player;
	
	void Update ()
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            player.transform.position.y + 2, 
            player.transform.position.z+2);
    }
}
