using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

    private MissionManager missionManager;
    private AudioSource audioSource;
    public GameObject mic;
    public float soundDelayTime = 0.5f;
    private bool relevationSpeechPlayed = false;
    private bool thankyouSpeechPlayed = false;
    private bool mysterySpeechPlayed = false;
    private bool isDialog = false;

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
                audioSource.PlayDelayed(soundDelayTime);

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
                if (missionManager.state == MissionManager.MissionState.EndByWarning && isDialog)
                {
                    //Debug.Log("play thankyou");
                    if (!thankyouSpeechPlayed)
                    {
                        DialogManager.Dialog[] clips = new DialogManager.Dialog[3];  //Kathy
                        clips[0] = new DialogManager.Dialog(missionManager.currentMission.GetGuiltySuspect().thankyou);  //Kathy
                        clips[1] = new DialogManager.Dialog(missionManager.currentMission.revelationSpeech);  //Kathy
                        clips[2] = new DialogManager.Dialog(missionManager.currentMission.mysterySpeech);  //Kathy

                        setDialog(clips);
                        thankyouSpeechPlayed = true;
                        relevationSpeechPlayed = true;
                        mysterySpeechPlayed = true;
                        
                        //
                        missionManager.state = MissionManager.MissionState.MissionOver;
                    }
                }

                if (missionManager.state == MissionManager.MissionState.EndByArrest && isDialog)
                {
                    if (!relevationSpeechPlayed)
                    {
                        DialogManager.Dialog[] clips = new DialogManager.Dialog[2];  //Kathy
                        clips[0] = new DialogManager.Dialog(missionManager.currentMission.revelationSpeech);  //Kathy
                        clips[1] = new DialogManager.Dialog(missionManager.currentMission.mysterySpeech);  //Kathy

                        setDialog(clips);
                        relevationSpeechPlayed = true;
                        mysterySpeechPlayed = true;
                        missionManager.state = MissionManager.MissionState.MissionOver;
                    }

                }

                if (missionManager.state == MissionManager.MissionState.MissionOver && isDialog)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        missionManager.currentMission.suspects[i].character.isSuspect = false;
                        //save data

                    }
                }


            }
        }
    }
}
