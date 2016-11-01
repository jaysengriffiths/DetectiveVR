using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;  //Kathy1

public class Kathy2_Player : MonoBehaviour
{

    class GazeTimer
    {
        public GazeTimer(float ti)
        {
            timeInterval = ti;
        }

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

    //Player movement
    private float minBounds = 10;
    private float maxBounds = 20;
    private float counter;
    //[HideInInspector]
    public float cameraAngle;
    public float speed = 0.01f;  //Kathy1 changed from 0.03;
    public float walkDelay;
    public AudioClip nameClip;
    public Transform enkHoldingCloth;

    //setting new timers
    GazeTimer soundLookAtTimer = new GazeTimer(2);
    GazeTimer suspectGazeTimer = new GazeTimer(2);

    //audio source set
    private AudioSource audioSource;

    //toggles initial complainant
    public bool complain = false;

    //clue obj setup
    public GameObject clueObject;
    [HideInInspector]
    public bool clueComparisonPlayed = false;

    //Footsteps 
    private float m_StepCycle;  //
    public AudioSource feetSource;  //Kathy1
    public float m_StepPeriod;//Kathy1

    [SerializeField]
    public AudioClip[] m_GroudFootstepSounds;    // an array of footstep sounds that will be randomly selected from - Kathy1 copied from Standard Assets character script
    public AudioClip[] m_MudFootstepSounds;
    public AudioClip[] m_GrassFootstepSounds;

    //Getting other components required
    private MissionManager missionManager;
    private DialogManager dialogManager;
    private CharacterController controller;
    private InventoryControl.Accumulator goHomeTimer;
    public Character selectedCharacter;
    private GameObject enkModel;



    void Awake()
    {
        dialogManager = GetComponent<DialogManager>();
        GameObject mic = GameObject.Find("Microphone");
        if (mic)
            audioSource = mic.GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        goHomeTimer = new InventoryControl.Accumulator(2);
        enkModel = GameObject.Find("gypsy_mesh");
    }

    void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
        counter = 0;

        m_StepCycle = 0f;  //Kathy1

        if (complain)
        {
            missionManager.Complainant();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion fwd = Camera.main.transform.rotation;
        enkModel.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        cameraAngle = fwd.eulerAngles.x;
        //rb.velocity = new Vector3(0, 0, 0);
        if (dialogManager != null)
        {
            dialogManager.updateDialog();

            LookAtSoundObjects();
            if (dialogManager.pendingDialog.Length == 0 && (audioSource == null || !audioSource.isPlaying))
            {
                if (audioSource)
                {
                    walk();
                }
                Look();
            }
        }
    }

    void LookAtSoundObjects()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20))
        {
            SoundLookAt soundItem = hit.collider.GetComponent<SoundLookAt>();
            soundLookAtTimer.SetObject(soundItem);
            if (soundLookAtTimer.IsExpired())
            {
                if (soundItem != null)
                {
                    if (!audioSource.isPlaying)
                    {
                        if (soundItem.timesPlayed < soundItem.maxTimesPlayed || soundItem.maxTimesPlayed == 0)
                        {
                            DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
                            clips[0] = new DialogManager.Dialog(soundItem.activated, soundItem.transform);
                            clips[1] = new DialogManager.Dialog(soundItem.enkNames);
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
            }
        }
        else
        {
            soundLookAtTimer.SetObject(null);
        }

    }
    public void Look()
    {
        RaycastHit hit;
        //Camera.main.

        //Interaction with NPCs
        bool lookingAtGoldStar = false;

        //turn off viewing
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        {

            if (hit.collider.CompareTag("SUSPECT")) // we're looking at a charcter
            {
                Character ch = hit.collider.gameObject.GetComponent<Character>();
                suspectGazeTimer.SetObject(ch);
                if (suspectGazeTimer.IsExpired())
                {
                    // we've been starting at thisa one thing for three seconds...
                    missionManager.Interrogate(ch);
                    selectedCharacter = ch;
                }
            }
            else
                suspectGazeTimer.SetObject(null);

            //Interaction with returning to HQ
            if (hit.collider.CompareTag("GoldStar"))
            {
                lookingAtGoldStar = true;
            }
        }
        else
        {
            suspectGazeTimer.SetObject(null);
        }

        if (goHomeTimer.IsFull(lookingAtGoldStar))
            SceneManager.LoadScene("HQ");

        if (selectedCharacter != null)
        {
            Vector3 v1 = Camera.main.transform.forward;
            Vector3 v2 = selectedCharacter.transform.position - transform.position;
            v1.y = 0;
            v2.y = 0;
            if (Vector3.Dot(v1, v2) < 0)
                selectedCharacter = null;
        }
    }

    public void walk()
    {
        bool lookingDown = (cameraAngle > minBounds && cameraAngle < maxBounds);

        if (lookingDown)
        {
            if (counter == 0)
            {
                counter = Time.time + walkDelay;
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
        Vector3 fwd = Camera.main.transform.forward;
        controller.Move(speed * fwd);
        ProgressStepCycle();
    }

    void ProgressStepCycle()  //Kathy1 this whole struct from Standard Assets character controller
    {
        m_StepCycle += Time.deltaTime;

        if (!(m_StepCycle > m_StepPeriod))
        {
            return;
        }

        PlayFootStepAudio();
        m_StepCycle = 0;
    }

    void PlayFootStepAudio() //Kathy1 this whole struct amended from Standard Assets character controller
                            //Kathy2 commented out raycasting to get back footsteps sound temporarily
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0

        /*RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 2, 0), Vector3.down, out hit, 5))
            Debug.DrawRay(transform.position + new Vector3(0, 2, 0), Vector3.down, Color.green);  //Kathy2 - can't see raycast working
            print("Kathy2Player script footsteps raycast found an object - distance: " + hit.distance); //Kathy2 - hits are between 0.54 and 0.63
         */

        RaycastHit hitGround;

        if (Physics.Raycast(transform.position, Vector3.down, out hitGround)) //Kathy2
            Debug.DrawRay(transform.position, Vector3.down, Color.red);  //Kathy2 - still can't see raycast.
            //I think it can't be inside player's collider as it is just registering hits of itself.  Kathy 2 
            print("Kathy2Player script footsteps raycast found an object - distance: " + hitGround.distance); //Kathy2 - hits are between 0 and 0.02*/

        {
            // if (hit.collider.CompareTag("Ground"))  //if(hit.transform.tag == "ground")
            {
                int n = Random.Range(1, m_GroudFootstepSounds.Length);
                feetSource.clip = m_GroudFootstepSounds[n];
                feetSource.PlayOneShot(feetSource.clip);
                //move picked sound to index 0 so it's not picked next time
                m_GroudFootstepSounds[n] = m_GroudFootstepSounds[0];
                m_GroudFootstepSounds[0] = feetSource.clip;

               // m_GroudFootstepSounds[n] = m_GroudFootstepSounds[0];
               // m_GroudFootstepSounds[0] = feetSource.clip;

            }
        }


       /* if (hit.collider.CompareTag("Grass"))
        {
            int n = Random.Range(1, m_GrassFootstepSounds.Length);
            feetSource.clip = m_GrassFootstepSounds[n];
            feetSource.PlayOneShot(feetSource.clip);

            m_GrassFootstepSounds[n] = m_GrassFootstepSounds[0];
            m_GrassFootstepSounds[0] = feetSource.clip;

            m_GrassFootstepSounds[n] = m_GrassFootstepSounds[0];
            m_GrassFootstepSounds[0] = feetSource.clip;
        }

        if (hit.collider.CompareTag("Mud"))
        {
            int n = Random.Range(1, m_MudFootstepSounds.Length);
            feetSource.clip = m_MudFootstepSounds[n];
            feetSource.PlayOneShot(feetSource.clip);

            m_MudFootstepSounds[n] = m_MudFootstepSounds[0];
            m_MudFootstepSounds[0] = feetSource.clip;

            m_MudFootstepSounds[n] = m_MudFootstepSounds[0];
            m_MudFootstepSounds[0] = feetSource.clip;
        }*/
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