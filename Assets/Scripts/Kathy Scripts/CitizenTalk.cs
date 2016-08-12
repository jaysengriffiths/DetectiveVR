using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CitizenTalk : MonoBehaviour {

	public RaycastHit hit;
	public GameObject speak;
	public Camera playerCam;


	void Update () {

		//var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 1000)) {
			
			if (hit.collider.gameObject.tag == "Citizen") {
				speak.gameObject.SetActive (true);
			} else {
				speak.gameObject.SetActive (false);
			}
		}
	}
}
