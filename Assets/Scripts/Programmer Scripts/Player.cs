using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    // Use this for initialization
    private Quaternion startRot;
    private Vector3 startPos;
    public Camera cam;
    public Transform trans;
    //private Rigidbody player;
    public float minBounds = 10;
    public float maxBounds = 20;
    int counter;
    public int homeCounter = 0;
    public int moveTime = 20;
    public int guessTime = 20;
    private MouseLook mouseLook;
    public float speed = 0.04f;
    public bool confirm = false;


    [SerializeField]
    GameObject clue;

    [SerializeField]
    GameObject cuffs;

    [SerializeField]
    GameObject warn;

    void Awake()
    {
        mouseLook = GetComponent<MouseLook>();
    }

    void Start()
    {
        cam = Camera.main;
        startPos = trans.transform.position;
        startRot = trans.transform.rotation;
        //float cameraAngle = Camera.main.transform.rotation.x;
        trans = gameObject.GetComponent<Transform>();
        //player = gameObject.GetComponent<Rigidbody>();
        counter = 0;
        mouseLook.Init(transform, cam.transform);    
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        walk();
        Look();
       // Debug.Log(Camera.main.transform.eulerAngles.x);
    }

    public void Look()
    {
        RaycastHit hit;
        //Camera.main.
       
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        {
            if (hit.collider.CompareTag("GoldStar"))
            {

                homeCounter++;

                if (homeCounter >= 300)
                {
                    //return home 
                    SceneManager.LoadScene("HQ");
                    Debug.Log("testgohome");

                    homeCounter = 0;
                }

                if (homeCounter >= 150)
                {
                    Debug.Log("are you sure ");
                }
            }
            else {
                homeCounter = 0;
            }
     }
       


        RotateView();
        float cameraAngle = Camera.main.transform.eulerAngles.x;

        if (cameraAngle > 270 && cameraAngle < 280)
        {
            clue.SetActive(true);
        }
        else
        {
            clue.SetActive(true);
        }
        if (cameraAngle > 44 && cameraAngle < 54)
        {
            cuffs.SetActive(true);
        }
        else
        {
            cuffs.SetActive(false);
        }

        if (cameraAngle > 30 && cameraAngle < 44)
        {
            warn.SetActive(true);
        }
        else
        {
            warn.SetActive(false);
        }
    }



    public void walk()
    {
        RotateView();
        float cameraAngle = Camera.main.transform.eulerAngles.x;

        if (cameraAngle > minBounds && cameraAngle < maxBounds && homeCounter == 0)
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
        //float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation(trans, cam.transform);
    }

    void isMoving()
    {
        Quaternion q = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.Head);
        Vector3 fwd = q * Camera.main.transform.forward;
        fwd.y = 0;
        trans.transform.position = trans.transform.position + speed * fwd;
        Invoke(("PlaySound"), 2);   
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SceneManager.LoadScene(0);
        Camera.main.transform.localPosition = new Vector3(0, 2, 0);
        trans.transform.rotation = startRot;
        Camera.main.transform.rotation = new Quaternion(0, 0, 0, 1);
        trans.localPosition = startPos;
        trans.rotation = new Quaternion(0, 0, 0, 1);
    }
}

