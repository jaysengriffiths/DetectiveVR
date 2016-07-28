using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				transform.forward = Vector3.MoveTowards(transform.forward, ray.direction, Time.deltaTime) * 10;
			}
			// Moving forward
			transform.position += transform.forward * (Time.deltaTime * 10);
		}
     
}
