using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using VR = UnityEngine.VR;

//TODO LIST
//Fix interaction with handcuffs and warningbook
//Mission dialog all correct, delay needed between final dialog
//Fix cloth ripping when comparing


public class Player : MonoBehaviour
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

        public void SetObject(Object o, Color? c = null)
        {
            if (o != obj)
            {
                if (c != null)
                {
                    MonoBehaviour go = obj as MonoBehaviour;
                    if (go)
                        //go.GetComponent<MeshRenderer>().material.color = Color.white;
                        go.transform.localScale *= 0.909f;
                    MonoBehaviour go2 = o as MonoBehaviour;
                    if (go2)
                        //go2.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        go2.transform.localScale *= 1.1f;
                }

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
    //camera angle used for inventory control
    [HideInInspector]
    public float cameraAngle;
    private float minBounds = 10;
    private float maxBounds = 20;
    public float speed;  //Kathy changed from 0.03;
    private float counter;
    private CharacterController characterController;
    private float footstepVolumeScale = 0.5f;
    [HideInInspector]
    private float walkDelay = 0;
    public AudioClip nameClip;
    public int rayCastToSoundLookAt = 20;
    public int rayCastToSuspectLookAt = 10;

    public float soundLookAtTimeInSeconds = 0.25f;  //Kathy changed from 0.5f
    public float suspectLookAtTimeInSeconds =  0.25f;  //Kathy changed from 1
    public float hqGazeLookAtTimeInSeconds = 1;


    //setting new timers
    GazeTimer soundLookAtTimer;
    GazeTimer suspectGazeTimer;
    GazeTimer hqGazeTimer;
    
    //audio source set
    private AudioSource audioSource;
    public AudioClip[,] collideSounds;
    public AudioClip[] treeCollide;
    public AudioClip[] wallCollide;
    public AudioClip[] pigstyCollide;
    public AudioSource heartbeatSource;
    public AudioClip Heartbeat_Medium_12;

    //Clueholder on enk
    [HideInInspector]
    public GameObject clueObject;
    [HideInInspector]
    public Transform enkHoldingCloth;
    [HideInInspector]
    public bool clueComparisonPlayed = false;

    //Footsteps 
    private float m_StepCycle;
    private AudioSource feetSource;
    private float m_StepPeriod = 0.6f;
 
    [SerializeField]
    public AudioClip[] m_GroundFootstepSounds;    // an array of footstep sounds that will be randomly selected from - Kathy copied from Standard Assets character script
    public AudioClip[] m_MudFootstepSounds;
    public AudioClip[] m_GrassFootstepSounds;
    
    //Getting other components required
    private MissionManager missionManager;
    private DialogManager dialogManager;
    private DialogManager soundManager;
    private CharacterController controller;
    private InventoryControl.Accumulator goHomeTimer;
    private InventoryControl.Accumulator puzzleSceneLoadTimer;
    // private string missionName; //Kathy - commented out as now using mission in PlayerPrefs
    
    //Current selected character
    public Character selectedCharacter;
    private GameObject enkModel;
    [HideInInspector]
    public bool sceneLoaded;
    [HideInInspector]
    public bool puzzle = false;
    private bool loadScene;
    private bool canLoad = false;
    //public Camera mainCam; do not need public variable to find main camera which Unity already recognises from its tag

    void Awake()
    {
        enkHoldingCloth = GameObject.Find("clothHolder").transform;
        feetSource = gameObject.GetComponentInChildren<AudioSource>();
        dialogManager = GetComponent<DialogManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<DialogManager>();
        GameObject mic = GameObject.Find("DialogMicrophone");
        if (mic)
            audioSource = mic.GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        goHomeTimer = new InventoryControl.Accumulator(1f);
        puzzleSceneLoadTimer = new InventoryControl.Accumulator(2);
        enkModel = GameObject.Find("gypsy_mesh");
    }

    void Start()
    {
        soundLookAtTimer = new GazeTimer(soundLookAtTimeInSeconds);
        suspectGazeTimer = new GazeTimer(suspectLookAtTimeInSeconds);
        hqGazeTimer = new GazeTimer(hqGazeLookAtTimeInSeconds);
        missionManager = FindObjectOfType<MissionManager>();
        counter = 0;
      
        m_StepCycle = 0f;  //Kathy made 0

    }

    IEnumerator LoadNewScene()
    {
        //loadScene = false;  // Kathy

        AsyncOperation async = SceneManager.LoadSceneAsync("Main_Scene");
        
        while(!async.isDone)
        {
            yield return null;
        }

            loadScene = false;  // Kathy
        // yield return new WaitForSeconds(12);
    }

    // Update is called once per frame
    void Update()
    {
        if(canLoad == true && loadScene == false)
        {

            Text loading = GameObject.Find("Canvas").GetComponent<Text>();
            loading.text = "Loading.......";
            loading.color = new Color(loading.color.r, loading.color.g, loading.color.b, Mathf.PingPong(Time.time, 1));
            heartbeatSource.PlayOneShot(Heartbeat_Medium_12);

            canLoad = false;  //Kathy
            loadScene = true;

            StartCoroutine(LoadNewScene());
        }

        if (speed != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 25;
                
            }
            else
            {
                speed = 0.75f;
            }


        }
        characterController = gameObject.GetComponent<CharacterController>();
        if (Camera.main)
        {
            Quaternion fwd = Camera.main.transform.rotation;
            enkModel.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            cameraAngle = fwd.eulerAngles.x;
        }
   
        //rb.velocity = new Vector3(0, 0, 0);
        if (dialogManager != null)
        {
            dialogManager.updateDialog();

            LookAtSoundObjects();
            if (dialogManager.pendingDialog.Length == 0 && (audioSource == null || !audioSource.isPlaying))
            {
                if (audioSource)
                {
                    if (selectedCharacter == null)
                    {
                        walk();
                    }
                }
                Look();
            }

            if (selectedCharacter != null)
            {
                Vector3 v1 = Camera.main.transform.forward;
                Vector3 v2 = selectedCharacter.transform.position - transform.position;
                v1.y = 0;
                v2.y = 0;
                if (Vector3.Dot(v1, v2) < 0)
                {
                    selectedCharacter.threatenedPlayed = false;
                    dialogManager.ClearDialog(selectedCharacter);
                    selectedCharacter = null;
                    
                }
            }
        }

        if (clueObject && clueComparisonPlayed && dialogManager.pendingDialog.Length < 1)
        {
            if (selectedCharacter)
            {
                clueObject.GetComponent<MovingClue>().MoveTowards(selectedCharacter);
            }

            //player.clueObject.transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }

    void LoadMissionFromHQ(string mission)  // see LookAtSoundObjects method below
    {
        PlayerPrefs.SetString("Mission", mission);
        PlayerPrefs.Save();
        canLoad = true;
        // mainCam.enabled = false; replaced with reference to Camera.main for clarity and to obviate the need for a public variable here
        Camera.main.enabled = false;

        //Camera cam = Find<Camera>();
        //cam.gameObject.SetActive(false);

        //SceneManager.LoadScene("Main_Scene");
    }

    void LookAtSoundObjects()
    {
        RaycastHit hit;

        if (Camera.main == null)
            return;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayCastToSoundLookAt))
        {
            SoundLookAt soundItem = hit.collider.GetComponent<SoundLookAt>();
            soundLookAtTimer.SetObject(soundItem, Color.yellow);

            // load the scene and set the mission after a delay
            hqGazeTimer.SetObject(soundItem);
            if (hqGazeTimer.IsExpired() && dialogManager.pendingDialog.Length == 0  )   //this means mission will not load while dialogue is being spoken, for example, "My map"
            {
                if (soundItem.missionName != "")
                    LoadMissionFromHQ(soundItem.missionName);   // see LoadMissionFromHQ(string) above
            }

            if (soundLookAtTimer.IsExpired())
            {

                if (soundItem != null)
                {
                    if (!audioSource.isPlaying)
                    {

                        DialogManager.Dialog[] clips = new DialogManager.Dialog[soundItem.additionalDialog ? 3 : 2];

                        clips[0] = new DialogManager.Dialog(soundItem.activated, soundItem.transform);
                        clips[1] = new DialogManager.Dialog(soundItem.enkNames);
                        if(soundItem.additionalDialog)
                        {
                            //DialogManager.Dialog[] clipss = new DialogManager.Dialog[1];  //this value is never used - Kathy commented out
                            clips[2] = new DialogManager.Dialog(soundItem.additionalDialog);
                        }
                        if (soundItem.isClue || soundItem.enkNameObject)
                        {
                            soundManager.setDialog(clips);
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
        else
        {
            soundLookAtTimer.SetObject(null, Color.yellow);
        }

    }
    public void Look()
    {
        if (Camera.main == null)
            return;

        RaycastHit hit;

        //Interaction with NPCs
        bool lookingAtGoldStar = false;
        bool lookingAtPuzzle = false;
        
        //turn off viewing
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayCastToSuspectLookAt))
        {

            if (hit.collider.CompareTag("SUSPECT")) // we're looking at a character
            {
                Character ch = hit.collider.gameObject.GetComponent<Character>();
                //selectedCharacter = ch; //commented out here as moved down
                suspectGazeTimer.SetObject(ch);
                if (suspectGazeTimer.IsExpired() && missionManager != null)
                {
                    // we've been staring at this thing for three seconds...  //Kathy made it quarter of a second
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
            else
                hqGazeTimer.SetObject(null);

            //Interaction to travel to puzzle scene
            if (hit.collider.CompareTag("Puzzle"))
            {
                string mission = PlayerPrefs.GetString("Mission");          //Kathy - puzzle sounds were playing in all missions

                if (mission == "M5_Well")                                   //Kathy
                {
                    puzzle = true;
                    lookingAtPuzzle = true;
                }
            }
            else
            {
                puzzle = false;
            }
        }
        else
        {
            suspectGazeTimer.SetObject(null);
        }
        if (puzzle)
        {
            if (puzzleSceneLoadTimer.IsFull(lookingAtPuzzle))
            {
                if (PlayerPrefs.GetInt("puzzleDone") == 0)
                {
                    SceneManager.LoadScene("JamiesSandBox");
                }
            }
        }
        if (goHomeTimer.IsFull(lookingAtGoldStar))
        {
            SceneManager.LoadScene("HQ");
        }

    }

    public void walk()
    {
        if (Camera.main == null)
            return;

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
        if (Camera.main == null)
            return;
        Vector3 fwd = Camera.main.transform.forward;
        controller.Move((speed  * Time.deltaTime) * fwd);
        Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
        float horizontalSpeed = horizontalVelocity.magnitude;
        if (horizontalSpeed != 0)
        {
            ProgressStepCycle();
        }
    }
    
    void ProgressStepCycle()  //Kathy this whole method amended from Standard Assets character controller
    {
        m_StepCycle += Time.deltaTime;
       
        if (!(m_StepCycle > m_StepPeriod))
        {
            return;
        }

        PlayFootStepAudio();
        m_StepCycle = 0;
    }

    void PlayFootStepAudio() //Kathy this whole method amended from Standard Assets character controller
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.down, out hit, 20))
            Debug.DrawRay(transform.position + new Vector3(0, 2, 0), Vector3.down, Color.green);  //Kathy - not working, can't see green
        {
            if (hit.collider.CompareTag("Ground"))
            {
                int n = Random.Range(1, m_GroundFootstepSounds.Length);
                feetSource.clip = m_GroundFootstepSounds[n];
                feetSource.PlayOneShot(feetSource.clip, footstepVolumeScale);
                //move picked sound to index 0 so it's not picked next time
                m_GroundFootstepSounds[n] = m_GroundFootstepSounds[0];
                m_GroundFootstepSounds[0] = feetSource.clip;

                m_GroundFootstepSounds[n] = m_GroundFootstepSounds[0];
                m_GroundFootstepSounds[0] = feetSource.clip;

            }
        }


        if (hit.collider.CompareTag("Grass"))
        {
            int n = Random.Range(1, m_GrassFootstepSounds.Length);
            feetSource.clip = m_GrassFootstepSounds[n];
            feetSource.PlayOneShot(feetSource.clip, footstepVolumeScale);
            
            m_GrassFootstepSounds[n] = m_GrassFootstepSounds[0];
            m_GrassFootstepSounds[0] = feetSource.clip;

            m_GrassFootstepSounds[n] = m_GrassFootstepSounds[0];
            m_GrassFootstepSounds[0] = feetSource.clip;
        }

        if (hit.collider.CompareTag("Mud"))
        {
            int n = Random.Range(1, m_MudFootstepSounds.Length);
            feetSource.clip = m_MudFootstepSounds[n];
            feetSource.PlayOneShot(feetSource.clip, footstepVolumeScale);

            m_MudFootstepSounds[n] = m_MudFootstepSounds[0];
            m_MudFootstepSounds[0] = feetSource.clip;

            m_MudFootstepSounds[n] = m_MudFootstepSounds[0];
            m_MudFootstepSounds[0] = feetSource.clip;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TREE")
        {
            DialogManager.Dialog[] clips = new DialogManager.Dialog[3];
            clips[0] = new DialogManager.Dialog(treeCollide[0]);
            clips[1] = new DialogManager.Dialog(treeCollide[1]);
            clips[2] = new DialogManager.Dialog(treeCollide[2]);
            dialogManager.setDialog(clips);

        }
        if (other.tag == "WALL")
        {
            DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
            clips[0] = new DialogManager.Dialog(wallCollide[0]);
            clips[1] = new DialogManager.Dialog(wallCollide[1]);
            clips[2] = new DialogManager.Dialog(wallCollide[2]);
            dialogManager.setDialog(clips);
        }

        if (other.tag == "Pigsty")
        {
            DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
            clips[0] = new DialogManager.Dialog(pigstyCollide[0]);
            clips[1] = new DialogManager.Dialog(pigstyCollide[1]);
            clips[2] = new DialogManager.Dialog(pigstyCollide[2]);
            dialogManager.setDialog(clips);
        }
    }

}