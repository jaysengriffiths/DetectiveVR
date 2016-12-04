using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

    private MissionManager missionManager;
    private AudioSource audioSource;
    public GameObject mic;
    public float soundDelayAfterDialogSpokenInSeconds = 1;
    private float nextTimeStamp = 0;
    private Player player;
    private bool relevationSpeechPlayed = false;
    private bool thankyouSpeechPlayed = false;
    // private bool mysterySpeechPlayed = false;  //commented out because the private field `DialogManager.mysterySpeechPlayed' is assigned but its value is never used
    private bool isDialog = false;
    private bool isPlaying = false;
    // private savedData saveGame;     // commented out because the private field `DialogManager.saveGame' is assigned but its value is never used
    Animator talker;

    public enum DialogType
    {
        Normal,
        Warning,
        Arrest
    };
    DialogType currentDialogType = DialogType.Normal;

    public struct Dialog
    {
        //This plays at suspect
        public Dialog(AudioClip _clip, Character _ch)
        {
            clip = _clip;
            transform = _ch.transform;
        }

        //This takes gameobject transform
        public Dialog(AudioClip _clip, Transform _trans)
        {
            clip = _clip;
            transform = _trans;
        }

        //plays at player
        public Dialog(AudioClip _clip)
        {
            clip = _clip;
            transform = null;
        }
        public AudioClip clip;
        public Transform transform;
    };

    //Dialog queue in memory
    public Dialog[] pendingDialog = new Dialog[0];

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Enk").GetComponent<Player>();
        // saveGame = GameObject.FindObjectOfType<savedData>();  //// commented out because the private field `DialogManager.saveGame' is assigned but its value is never used

        if (mic)
        {
            isDialog = (mic.name == "DialogMicrophone");
            audioSource = mic.GetComponent<AudioSource>();
        }
        missionManager = FindObjectOfType<MissionManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        updateDialog();
 
    }

    public void setDialog(Dialog[] array, DialogType t = DialogType.Normal)
    {
        pendingDialog = array;
        currentDialogType = t;
    }

    public void ClearDialog(Character _ch)
    {
        pendingDialog = new Dialog[0];
        audioSource.Stop();
        if (currentDialogType == DialogType.Warning)
            _ch.warnPlayed = false;
        if (currentDialogType == DialogType.Arrest)
            _ch.arrestPlayed = false;
        missionManager.state = MissionManager.MissionState.Ongoing;
    }

    

    public void updateDialog()
    {
        //if not playing
        if (audioSource && !audioSource.isPlaying)
        {
            if (talker!=null)
            {

                talker.SetTrigger("isIdle"); // TODO
  
                talker = null;

            }
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
                    audioSource.transform.position = pendingDialog[0].transform.position; // snap to character
                    talker = pendingDialog[0].transform.GetComponent<Animator>();
                    if (talker)
                    {
                        talker.SetTrigger("isTalking");  //brings up error: Animator has not been initialized
                        //talker.SetTrigger("isStartingToTalk");
                    }
                }

                if (audioSource.isPlaying == false)
                {
                    if (isPlaying)
                    {
                        nextTimeStamp = Time.time + soundDelayAfterDialogSpokenInSeconds;
                    }

                    if (Time.time > nextTimeStamp)
                    {
                        // audio clip = the first pending
                        audioSource.clip = pendingDialog[0].clip;
                        audioSource.Play();

                        //copy non playing left over audio into temp array that will become the pending once play is over 
                        Dialog[] dialog = new Dialog[pendingDialog.Length - 1];

                        for (int i = 0; i < dialog.Length; i++)
                        {

                            dialog[i] = pendingDialog[i + 1];
                        }
                        pendingDialog = dialog;
                    }


                }
                isPlaying = audioSource.isPlaying;

            }
            else
            {
                if (missionManager != null && missionManager.state == MissionManager.MissionState.EndByWarning && isDialog)
                {
                    //Debug.Log("play thankyou");
                    if (!thankyouSpeechPlayed)
                    {
                        DialogManager.Dialog[] clips = new DialogManager.Dialog[3];
                        clips[0] = new DialogManager.Dialog(missionManager.currentMission.GetGuiltySuspect().thankyou);
                        clips[1] = new DialogManager.Dialog(missionManager.currentMission.revelationSpeech);
                        clips[2] = new DialogManager.Dialog(missionManager.currentMission.mysterySpeech);

                        setDialog(clips);
                        thankyouSpeechPlayed = true;
                        relevationSpeechPlayed = true;
                        // mysterySpeechPlayed = true;
                        //missionManager.currentMission.complete = true;  //already done in MissionManager
                        // missionManager.state = MissionManager.MissionState.MissionOver;
                    }
                }

                if (missionManager != null && missionManager.state == MissionManager.MissionState.EndByArrest && isDialog)
                {
                    talker = player.selectedCharacter.GetComponent<Animator>();
                    talker.SetTrigger("isHandcuffed");
                    player.selectedCharacter.transform.FindChild("handcuffs").gameObject.SetActive(true);  //note Lady of Manor does not have handcuffs and is never arrested
                    player.selectedCharacter = null;
                    if (!relevationSpeechPlayed)
                    {
                        DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
                        clips[0] = new DialogManager.Dialog(missionManager.currentMission.revelationSpeech);
                        clips[1] = new DialogManager.Dialog(missionManager.currentMission.mysterySpeech);


                        setDialog(clips);
                        relevationSpeechPlayed = true;
                        // mysterySpeechPlayed = true;
                        // missionManager.state = MissionManager.MissionState.MissionOver;
                        // missionManager.currentMission.complete = true;  //already done in MissionManager
                    }

                }
                if (missionManager != null && (missionManager.state == MissionManager.MissionState.EndByWarning || missionManager.state == MissionManager.MissionState.EndByArrest) && isDialog)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        missionManager.currentMission.suspects[i].character.isSuspect = false;
                        //saveGame.UpdateSave();

                    }
                }


            }
        }
    }
}
