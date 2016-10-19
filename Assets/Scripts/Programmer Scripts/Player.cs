using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;  //Kathy

public class Player : MonoBehaviour
{

    class GazeTimer
    {
        private float timeStamp;
        public float timeInterval;
        private Object obj;

        public void SetObject(Object o)
        {
            if (o != obj)
            {
                obj = o;
                if (o == null)
                {
                    timeStamp = 0;
                }
                else
                {
                    timeStamp = Time.time + timeInterval;
                }
            }

        }

        public bool IsExpired()
        {
            bool result = timeStamp != 0 && Time.time > timeStamp;
            if (result)
            {
                timeStamp = 0;
            }
            return result;

        }
    }

    // Use this for initialization
    private float minBounds = 10;
    private float maxBounds = 20;
    float counter;

    private int homeTimeStamp = 0;
    public float walkTimeStamp = 1;

    public float speed = 0.01f;  //Kathy changed from 0.03f


    public AudioClip nameClip;
    private float soundLookAtTime = 0;
    private AudioSource audioSource;
   
    Collider lookedAtObject = null;
    public float cameraAngle;
    public bool complain = false;
    public float soundLookAtTimestamp = 2.0f;
    public GameObject clueObject;


    //Footsteps 
    private float m_StepCycle;  //
    public float m_StepPeriod;//Kathy
    public AudioSource feetSource;  //Kathy
    [SerializeField]
    public AudioClip[] m_GroudFootstepSounds;    // an array of footstep sounds that will be randomly selected from - Kathy copied from Standard Assets character script
    public AudioClip[] m_MudFootstepSounds;
    public AudioClip[] m_GrassFootstepSounds;
    
    //Getting other components required
    private MissionManager missionManager;
    private DialogManager dialogManager;
    private CharacterController controller;
    private SoundLookAt selectedSoundObject;

    public Character selectedCharacter;



    void Awake()
    {
        dialogManager = GetComponent<DialogManager>();
        GameObject mic = GameObject.Find("Microphone");
        if (mic)
            audioSource = mic.GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
        counter = 0;
      
        m_StepCycle = 0f;  //Kathy

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
        dialogManager.updateDialog();

        LookAtSoundObjects();
        if (dialogManager.pendingDialog.Length == 0 && (audioSource==null || !audioSource.isPlaying))
        {
            if (audioSource)
            {
                walk();
            }
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
                            if (soundItem.timesPlayed < soundItem.maxTimesPlayed || soundItem.maxTimesPlayed == 0)
                            {
                                DialogManager.Dialog[] clips = new DialogManager.Dialog[2];  //Kathy
                                clips[0] = new DialogManager.Dialog(soundItem.activated, soundItem.transform);  //Kathy
                                clips[1] = new DialogManager.Dialog(soundItem.enkNames);  //Kathy
                                if (soundItem.isClue || soundItem.enkNameObject)
                                {
                                    dialogManager.setDialog(clips);
                                    soundItem.timesPlayed++;
                                    soundItem.isActivated = true;

                                }
                                else
                                {
                                    audioSource.clip = soundItem.activated;
                                    audioSource.Play();
                                    soundItem.isActivated = true;
                                }
                            }
                        }
   
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

    void isMoving()
    {
        Quaternion q = UnityEngine.VR.InputTracking.GetLocalRotation(UnityEngine.VR.VRNode.Head);
        Vector3 fwd = q * Camera.main.transform.forward;
        //fwd.y = 0;
        controller.Move(speed * fwd);
        ProgressStepCycle();  //Kathy
        //Invoke(("PlaySound"), 2);   
    }
    
    void ProgressStepCycle()  //Kathy this whole struct amended from Standard Assets character controller
    {
        m_StepCycle += Time.deltaTime;
       
        if (!(m_StepCycle > m_StepPeriod))
        {
            return;
        }

        PlayFootStepAudio();
        m_StepCycle = 0;
    }

    void PlayFootStepAudio() //Kathy this whole struct amended from Standard Assets character controller
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                int n = Random.Range(1, m_GroudFootstepSounds.Length);
                feetSource.clip = m_GroudFootstepSounds[n];
                feetSource.PlayOneShot(feetSource.clip);
                //move picked sound to index 0 so it's not picked next time
                m_GroudFootstepSounds[n] = m_GroudFootstepSounds[0];
                m_GroudFootstepSounds[0] = feetSource.clip;

                m_GroudFootstepSounds[n] = m_GroudFootstepSounds[0];
                m_GroudFootstepSounds[0] = feetSource.clip;

            }
        }


        if (GetComponent<Collider>().CompareTag("Grass"))
        {
            int n = Random.Range(1, m_GrassFootstepSounds.Length);
            feetSource.clip = m_GrassFootstepSounds[n];
            feetSource.PlayOneShot(feetSource.clip);
            
            m_GrassFootstepSounds[n] = m_GrassFootstepSounds[0];
            m_GrassFootstepSounds[0] = feetSource.clip;

            m_GrassFootstepSounds[n] = m_GrassFootstepSounds[0];
            m_GrassFootstepSounds[0] = feetSource.clip;
        }

        if (GetComponent<Collider>().CompareTag("Mud"))
        {
            int n = Random.Range(1, m_MudFootstepSounds.Length);
            feetSource.clip = m_MudFootstepSounds[n];
            feetSource.PlayOneShot(feetSource.clip);
            
            m_MudFootstepSounds[n] = m_MudFootstepSounds[0];
            m_MudFootstepSounds[0] = feetSource.clip;

            m_MudFootstepSounds[n] = m_MudFootstepSounds[0];
            m_MudFootstepSounds[0] = feetSource.clip;
        }
    }
    
    //Handles the removing headset, resets scene though?
    //private void OnApplicationPause(bool pauseStatus)
    //{
    //    SceneManager.LoadScene(0);
    //    Camera.main.transform.localPosition = new Vector3(0, 2, 0);
    //    transform.rotation = startRot;
    //    Camera.main.transform.rotation = new Quaternion(0, 0, 0, 1);
    //    transform.localPosition = startPos;
    //    transform.rotation = new Quaternion(0, 0, 0, 1);
    //}



    

}