using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private  Transform transform;

    // Use this for initialization
    void Start ()
    {
        transform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (transform.rotation.z < 75)
            isMoving();
    }

    void isMoving()
    {

    }
     
}
