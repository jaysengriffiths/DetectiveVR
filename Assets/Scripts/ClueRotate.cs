using UnityEngine;
using System.Collections;

public class ClueRotate : MonoBehaviour {

	public float speed = 20f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * speed);
	}
}
