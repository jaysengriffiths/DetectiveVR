using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Quaternion startRot;
    private Vector3 startPos;
    public Transform trans;
    //private Rigidbody player;
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
        startPos = trans.transform.position;
        startRot = trans.transform.rotation;

        float cameraAngle = Camera.main.transform.rotation.x;
        trans = gameObject.GetComponent<Transform>();

        //gettting rigidbody 
        //player = gameObject.GetComponent<Rigidbody>();
        counter = 0;
        mouseLook.Init(transform, Camera.main.transform);
    }
	
	// Update is called once per frame
	private void Update ()
    {
//#if !MOBILE_INPUT
        RotateView();
//#endif
        float cameraAngle = Camera.main.transform.eulerAngles.x;
        Quaternion q = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.Head);
        cameraAngle += q.eulerAngles.x;

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

        mouseLook.LookRotation(trans, Camera.main.transform);

        //if (m_IsGrounded || advancedSettings.airControl)
        //{
        //    // Rotate the rigidbody velocity to match the new direction that the character is looking
        //    Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
        //    m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
        //}
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        //SceneManager.LoadScene(0);
        //Camera.main.transform.localPosition = new Vector3(0, 2, 0);
        //trans.transform.rotation = startRot;
        //Camera.main.transform.rotation = new Quaternion(0, 0, 0, 1);
        //trans.localPosition = startPos;
        //trans.rotation = new Quaternion(0, 0, 0, 1);
    }
    void isMoving()
    {
        //Vector3 fwd = Camera.main.transform.forward;
        Quaternion q = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.Head);
        Vector3 fwd = q * Camera.main.transform.forward;
        fwd.y = 0;
        trans.transform.position = trans.transform.position + speed * fwd;
    }
     
}
