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
    private bool clueGiven = false;
    public AudioClip nameClip;
    public AudioClip introClip;

    private AudioSource audioSource;

    Collider lookedAtObject = null;

   AudioClip[] pendingDialog = new AudioClip[0];

    [SerializeField]
    GameObject clue;

    [SerializeField]
    GameObject cuffs;

    [SerializeField]
    GameObject warn;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mouseLook = GetComponent<MouseLook>();
    }

    void Start()
    {
        //GetComponent<MissionManager>();
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
    private void Update()
    {
        updateDialog();


        if (pendingDialog.Length == 0)
        {
            walk();
            Look();
        }
    }

    public void Look()
    {
        RaycastHit hit;
        //Camera.main.

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        { 
            
            if(hit.collider.CompareTag("SUSPECT")) // we're looking at a charcter
            {
                Character ch = hit.collider.gameObject.GetComponent<Character>();
                if (lookedAtObject != hit.collider) // we've changed what we're looking at
                {

                    Debug.Log("Looking at suspect");
                    ch.lookAtTime = Time.time + 3.0f;
                }
                else
                {
                    if (Time.time > ch.lookAtTime && ch.lookAtTime != 0)
                    {
                        // we've been starting at thisa one thing for three seconds...
                        //play interact sound
                        if (!ch.introPlayed)
                        {
                            AudioClip[] dialog = new AudioClip[2];
                            dialog[0] = this.introClip;
                            dialog[1] = ch.introClip;
                            setDialog(dialog);
                           
                            ch.introPlayed = true;
                        }
                        Debug.Log("interact");
                        ch.lookAtTime = 0;
                    }
                }

                //Debug.Log("Awake");
                //play awake sound
            }
            lookedAtObject = hit.collider;

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
            else
            {
                homeCounter = 0;
            }


        }
        else
            lookedAtObject = null;

        RotateView();
        float cameraAngle = Camera.main.transform.eulerAngles.x;

        if (cameraAngle > 270 && cameraAngle < 280)
        {
            clue.SetActive(true);
            if(!clueGiven)
            {
                Debug.Log("Give Clue");
                clueGiven = true;
            }
            
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
        //Invoke(("PlaySound"), 2);   
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

    public void setDialog(AudioClip[] array)
    {
        pendingDialog = array;
    }

    private void updateDialog()
    {
        //if not playing
        if (!audioSource.isPlaying)
        {
            //and the lenfth isnt 0
            if (pendingDialog.Length > 0)
            {
                //audio clip = the first pending
                audioSource.clip = pendingDialog[0];
                //start audio 
                audioSource.Play();
                //copy non playing left over audio into temp array tha twill become the pending once play is over 
                AudioClip[] dialog = new AudioClip[pendingDialog.Length - 1];

                for (int i = 0; i < dialog.Length; i++)
                {

                    dialog[i] = pendingDialog[i + 1];
                }
                pendingDialog = dialog;
            }


        }
    }





}

