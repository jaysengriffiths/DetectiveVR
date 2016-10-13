using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;  //Kathy

public class Kathy_Player : MonoBehaviour
{

    // Use this for initialization
    private Quaternion startRot;
    private Vector3 startPos;
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
    public float speed = 0.01f;  //Kathy changed from 0.03f
    private float m_StepCycle;  //Kathy
    private float m_NextStep;  //Kathy
    public AudioSource feetSource;  //Kathy
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
    public float soundLookAtTimestamp = 2.0f;
    private Rigidbody rb;

    [SerializeField]
    public AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from - Kathy copied from Standard Assets character script

    public struct Dialog
    {
        public Dialog(AudioClip _clip, Character _ch)
        {
            clip = _clip;
            transform = _ch.transform;
        }


        public Dialog(AudioClip _clip, Transform _trans)
        {
            clip = _clip;
            transform = _trans;
        }

        public Dialog(AudioClip _clip)
        {
            clip = _clip;
            transform = null;
        }
        public AudioClip clip;
        public Transform transform;
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
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        warn.SetActive(false);
        cuffs.SetActive(false);
        clue.SetActive(false);
        missionManager = FindObjectOfType<MissionManager>();
        startPos = transform.position;
        startRot = transform.rotation;
        counter = 0;
        mouseLook.Init(transform, Camera.main.transform);
        m_StepCycle = 0f;  //Kathy
        m_NextStep = m_StepCycle / 2f;  //Kathy

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
        rb.velocity = new Vector3(0, 0, 0);
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
                    soundLookAtTime = Time.time + soundItem.timeStamp;
                }
                selectedSoundObject = soundItem;
            }
            else
            {
                if (Time.time > soundLookAtTime && soundLookAtTime != 0)
                {
                    if (soundItem != null)
                    {
                        if (!audioSource.isPlaying)
                        {
                            if (soundItem.timesPlayed < soundItem.maxTimesPlayed)
                            {
                                Kathy_Player.Dialog[] clips = new Kathy_Player.Dialog[2];  //Kathy
                                clips[0] = new Kathy_Player.Dialog(soundItem.activated, soundItem.transform);  //Kathy
                                clips[1] = new Kathy_Player.Dialog(soundItem.enkNames);  //Kathy
                                if (soundItem.isClue)
                                {
                                    setDialog(clips);

                                }
                                else
                                {
                                    audioSource.clip = soundItem.activated;
                                    audioSource.Play();
                                }
                            }
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
                Kathy_Player.Dialog[] clips = new Kathy_Player.Dialog[1];  //Kathy
                clips[0] = new Kathy_Player.Dialog(clip);  //Kathy
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
        ProgressStepCycle();  //Kathy
        //Invoke(("PlaySound"), 2);   
    }
    
    void ProgressStepCycle()  //Kathy this whole struct amended from Standard Assets character controller
    {
        m_StepCycle += (speed * Time.fixedDeltaTime);
       
        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        PlayFootStepAudio();
    }

    void PlayFootStepAudio() //Kathy this whole struct amended from Standard Assets character controller
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        feetSource.clip = m_FootstepSounds[n];
        feetSource.PlayOneShot(feetSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = feetSource.clip;
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
                if (pendingDialog[0].transform == null)
                {
                    audioSource.transform.position = transform.position; // snap to player
                }
                else
                {
                    audioSource.transform.position = pendingDialog[0].transform.position; // snap to player
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

                    Kathy_Player.Dialog[] clips = new Kathy_Player.Dialog[1];  //Kathy
                    clips[1] = new Kathy_Player.Dialog(missionManager.currentMission.GetGuiltySuspect().thankyou);  //Kathy

                    setDialog(clips);
                }

                if (missionManager.state == MissionManager.MissionState.EndByArrest)
                {
                    Debug.Log("play overarching");
                    Kathy_Player.Dialog[] clips = new Kathy_Player.Dialog[1];  //Kathy
                    clips[0] = new Kathy_Player.Dialog(missionManager.currentMission.revelationSpeech);  //Kathy

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