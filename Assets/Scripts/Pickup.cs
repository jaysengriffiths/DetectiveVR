using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public float speed;
	public RaycastHit hit;
	public GameObject player;
	public Camera playerCam;
	public Transform target;

	public bool stopMoving;
	public bool isMoving;

	public bool objectRotate;
	public float cluePickup = 4.5f;
	public GameObject clue;

	public Transform look;

	public float rotateSpeed;

	void Moving () {

	//	transform.LookAt (target);
		//if (!stopMoving) {
		speed = 1.5f;
			//player.transform.position += player.transform.forward * (Time.deltaTime * speed);
		player.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		//}
	}

	void distTooClose() {
		speed = 0f;
	}

	void Update () {



		Vector3 pos = player.transform.position;
		pos.y = 1f;
		player.transform.position = pos;

		//if (Input.GetMouseButtonDown (0)) {
		//var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		if (Vector3.Distance (GetComponent<Collider>().transform.position, clue.transform.position) < cluePickup) {
			//Destroy (clue.gameObject); 
			clue.transform.position = look.position;
			objectRotate = true;
		}

		if (objectRotate == true) {
			clue.transform.Rotate (Vector3.up * Time.deltaTime * rotateSpeed);
		}

		if (target) {
			float dist = Vector3.Distance (target.position, transform.position);
			//print ("Distance to other: " + dist);

			if (dist <= 1.5) {
				Debug.Log ("yes");
				stopMoving = true;
				distTooClose ();

			}
			if (dist > 1.5) {
				stopMoving = false;
			}
		} 

		if (isMoving == true) {
			Moving ();
		}

		if (Physics.Raycast (ray, out hit, 1000)) {

			target = hit.transform;

			//	Debug.Log ("ray hit (name): " + hit.collider.gameObject.name);
			//	Debug.Log ("ray hit (tag): " + hit.collider.gameObject.tag);

			//Debug.DrawRay (transform.position, Vector3.forward * 10, Color.green);

			if ((hit.collider.gameObject.tag == "Object" || hit.collider.gameObject.tag == "Clue")  && stopMoving == false) {

				isMoving = true;

			} else {

				speed = 0f;
				isMoving = false;
			

				//	}
			}
		}
	}
}
