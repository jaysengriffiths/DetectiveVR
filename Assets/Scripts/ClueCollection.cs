using UnityEngine;
using System.Collections;

public class ClueCollection : MonoBehaviour {

	void OnTriggerStay (Collider col) {

		if ((col.gameObject.tag == "Clue") && (Input.GetKeyDown (KeyCode.E))) {
			Debug.Log ("Clue Get");
			Destroy (col.gameObject); 
		}
	}
}
