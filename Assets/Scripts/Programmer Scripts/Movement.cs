using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour {

    public Camera cam;
    public Transform trans;
    private Rigidbody player;
    public float minBounds = 10;
    public float maxBounds = 20;
    int counter;
    public int moveTime = 20;
    private MouseLook mouseLook;
    public float speed = 0.04f;
    // Use this for initialization

    void Awake()
    {
        mouseLook = GetComponent<MouseLook>();
    }
    void Start ()
    {
        float cameraAngle = Camera.main.transform.rotation.x;
        trans = gameObject.GetComponent<Transform>();
        player = gameObject.GetComponent<Rigidbody>();
        counter = 0;
        mouseLook.Init(transform, cam.transform);
    }
	
	// Update is called once per frame
	private void Update ()
    {
        RotateView();
        float cameraAngle = Camera.main.transform.eulerAngles.x;

        if (cameraAngle > minBounds && cameraAngle < maxBounds)
            counter++;
        else
            counter = 0;
        if (counter > moveTime)
            isMoving();
    }

    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        // get the rotation before it's changed
        float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation(trans, cam.transform);

        //if (m_IsGrounded || advancedSettings.airControl)
        //{
        //    // Rotate the rigidbody velocity to match the new direction that the character is looking
        //    Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
        //    m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
        //}
    }

    void isMoving()
    {
        Vector3 fwd = Camera.main.transform.forward;
        fwd.y = 0;
        player.transform.position = player.transform.position + speed * fwd;
    }
     
}
