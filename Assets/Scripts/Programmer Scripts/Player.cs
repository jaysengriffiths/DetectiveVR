using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // Use this for initialization
    private Quaternion startRot;
    private Vector3 startPos;
    //private Rigidbody player;
    private float minBounds = 10;
    private float maxBounds = 20;
    float counter;
    private int warnTimeStamp = 0;
    private int cuffTimeStamp = 0;
    private int homeTimeStamp = 0;
    public float walkTimeStamp = 1;
    //private int guessTime = 20;
    private bool arrestSuspect = false;
    private bool warnSuspect = false;
    private MouseLook mouseLook;
    public float speed = 0.03f;
    //private bool confirm = false;
    private bool clueGiven = false;
    public Character selectedCharacter;
    public SoundLookAt selectedSoundObject;
    //public AudioClip collisionWallClip;
    //public AudioClip collisionTreeClip;
    //public AudioClip collisionPigstyClip;
    public AudioClip nameClip;
    private float soundLookAtTime = 0;
    //private Rigidbody rb;
    private AudioSource audioSource;
    private MissionManager missionManager;
    Collider lookedAtObject = null;
    private float cameraAngle;
    public bool complain = false;

    public struct Dialog
    {
        public Dialog(AudioClip _clip, Character _ch)
        {
            clip = _clip;
            character = _ch;
        }
        public AudioClip clip;
        public Character character;
    };

    Dialog[] pendingDialog = new Dialog[0];

    [SerializeField]
    GameObject clue;

    [SerializeField]
    GameObject cuffs;

    [SerializeField]
    GameObject warn;

    void Awake()
    {

        audioSource = GameObject.Find("Microphone").GetComponent<AudioSource>();
        mouseLook = GetComponent<MouseLook>();
    }

    void Start()
    {
        warn.SetActive(false);
        cuffs.SetActive(false);
        clue.SetActive(false);
        //rb = GetComponent<Rigidbody>();
        missionManager = FindObjectOfType<MissionManager>();
        //selectedSoundObject = FindObjectOfType<SoundLookAt>();
        startPos = transform.position;
        startRot = transform.rotation;
        counter = 0;
        mouseLook.Init(transform, Camera.main.transform);
        if (complain)
        {
            missionManager.Complainant();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.Head);
        Quaternion fwd = Camera.main.transform.rotation * q;

        cameraAngle = fwd.eulerAngles.x;
        //rb.velocity = new Vector3(0, 0, 0);
        updateDialog();

        RotateView();
        LookAtSoundObjects();
        if (pendingDialog.Length == 0 && !audioSource.isPlaying)
        {
            walk();
            Look();
        }



    }

    void LookAtSoundObjects()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20))
        {
            SoundLookAt soundItem = hit.collider.GetComponent<SoundLookAt>();
            if (soundItem != selectedSoundObject)
            {
                if (soundItem != null)
                {
                    //3 seconds delay
                    soundLookAtTime = Time.time + 3.0f;
                }
                selectedSoundObject = soundItem;
            }
            else
            {
                if (Time.time > soundLookAtTime && soundLookAtTime != 0)
                {
                    if (soundItem != null)
                    {
                        if (soundItem.timesPlayed < soundItem.maxTimesPlayed)
                        {
                            audioSource.clip = soundItem.sound;
                            audioSource.Play();
                        }
                        soundItem.timesPlayed++;
                    }
                    soundLookAtTime = 0;

                }
            }


        }
        else
        {
            selectedSoundObject = null;
            soundLookAtTime = 0;
        }

    }
    public void Look()
    {
        RaycastHit hit;
        //Camera.main.



        //Interaction with NPCs
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        {

            if (hit.collider.CompareTag("SUSPECT")) // we're looking at a charcter
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
                        missionManager.Interrogate(ch);
                        selectedCharacter = ch;
                        ch.lookAtTime = 0;

                    }
                }


            }

            lookedAtObject = hit.collider;

            //Interaction with returning to HQ
            if (hit.collider.CompareTag("GoldStar"))
            {
                homeTimeStamp++;

                if (homeTimeStamp >= 300)
                {
                    //return home 
                    SceneManager.LoadScene("HQ");
                    Debug.Log("testgohome");

                    homeTimeStamp = 0;
                }

                if (homeTimeStamp >= 150)
                {
                    Debug.Log("are you sure ");
                }
            }
            else
            {
                homeTimeStamp = 0;
            }
        }
        else
            lookedAtObject = null;

        if (selectedCharacter != null && Vector3.Dot(Camera.main.transform.forward, selectedCharacter.transform.position - transform.position) < 0)
        {
            Debug.Log("Looking Away");
            selectedCharacter = null;
            warnSuspect = false;
            arrestSuspect = false;
        }

        RotateView();

        if (cameraAngle > 270 && cameraAngle < 280)
        {
            clue.SetActive(true);
            if (!clueGiven)
            {
                Debug.Log("Give Clue");
                AudioClip clip;
                clip = missionManager.currentMission.clueDialogue;
                Player.Dialog[] clips = new Player.Dialog[1];
                clips[0] = new Player.Dialog(clip, null);
                setDialog(clips);
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
            if (selectedCharacter != null)
            {
                cuffTimeStamp++;
                if (cuffTimeStamp == 200 && selectedCharacter.introClip && selectedCharacter.IsInteracted)
                {
                    Debug.Log("Cuffing jingling sound");
                    missionManager.Arrest(selectedCharacter);
                }
            }
        }
        else
        {
            cuffs.SetActive(false);
            cuffTimeStamp = 0;
        }

        if (cameraAngle > 30 && cameraAngle < 44)
        {
            warn.SetActive(true);
            if (selectedCharacter != null)
            {
                warnTimeStamp++;
                if (warnTimeStamp == 200 && selectedCharacter.introPlayed && selectedCharacter.IsInteracted)
                {
                    Debug.Log("warning book sound");
                    missionManager.Warn(selectedCharacter);
                }

                //if(warnTimeStamp == 500 && selectedCharacter.IsInteracted)
                //{
                //    Debug.Log("Suspect checking");
                //    warnSuspect = true;
                //}
                //if(warnTimeStamp == 800)
                //{
                //    Debug.Log("Enks warning");
                //    Debug.Log("Suspect dialogue response");
                //}

            }
        }
        else
        {
            warn.SetActive(false);
            warnTimeStamp = 0;
        }
    }



    public void walk()
    {
        bool lookingDown = (cameraAngle > minBounds && cameraAngle < maxBounds && homeTimeStamp == 0);

        if (lookingDown)
        {
            if (counter == 0)
            {
                counter = Time.time + walkTimeStamp;
            }
            else
            {
                if (Time.time > counter)
                    isMoving();
            }
        }
        else
            counter = 0;

    }


    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        // get the rotation before it's changed
        //float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation(transform, Camera.main.transform);
    }

    void isMoving()
    {
        Quaternion q = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.Head);
        Vector3 fwd = q * Camera.main.transform.forward;
        fwd.y = 0;
        transform.position = transform.position + speed * fwd;
        //Invoke(("PlaySound"), 2);   
    }

    //private void OnApplicationPause(bool pauseStatus)
    //{
    //    SceneManager.LoadScene(0);
    //    Camera.main.transform.localPosition = new Vector3(0, 2, 0);
    //    transform.rotation = startRot;
    //    Camera.main.transform.rotation = new Quaternion(0, 0, 0, 1);
    //    transform.localPosition = startPos;
    //    transform.rotation = new Quaternion(0, 0, 0, 1);
    //}

    public void setDialog(Dialog[] array)
    {
        pendingDialog = array;
    }

    private void updateDialog()
    {
        //if not playing
        if (!audioSource.isPlaying)
        {
            //and the length isnt 0
            if (pendingDialog.Length > 0)
            {

                //start audio
                if (pendingDialog[0].character == null)
                {
                    audioSource.transform.position = transform.position; // snap to player
                }
                else
                {
                    audioSource.transform.position = pendingDialog[0].character.transform.position; // snap to player
                }

                // audio clip = the first pending
                audioSource.clip = pendingDialog[0].clip;
                audioSource.Play();

                //copy non playing left over audio into temp array tha twill become the pending once play is over 
                Dialog[] dialog = new Dialog[pendingDialog.Length - 1];

                for (int i = 0; i < dialog.Length; i++)
                {

                    dialog[i] = pendingDialog[i + 1];
                }
                pendingDialog = dialog;
            }
            else
            {
                if (missionManager.state == MissionManager.MissionState.EndByWarning)
                {
                    Debug.Log("play thankyou");

                    Player.Dialog[] clips = new Player.Dialog[1];
                    clips[1] = new Player.Dialog(missionManager.currentMission.GetGuiltySuspect().thankyou, null);

                    setDialog(clips);
                }

                if (missionManager.state == MissionManager.MissionState.EndByArrest)
                {
                    Debug.Log("play overarching");
                    Player.Dialog[] clips = new Player.Dialog[1];
                    clips[0] = new Player.Dialog(missionManager.currentMission.revelationSpeech, null);

                    setDialog(clips);

                }

                if (missionManager.state == MissionManager.MissionState.MissionOver)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        missionManager.currentMission.suspects[i].character.isSuspect = false;
                    }
                }


            }
        }
    }

}