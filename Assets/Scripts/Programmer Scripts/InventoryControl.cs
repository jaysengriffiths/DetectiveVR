using UnityEngine;
using System.Collections;

public class InventoryControl : MonoBehaviour
{ 
    public GameObject WarningBook;
    public GameObject SkyHint;
    public GameObject HandCuffs;
    public AudioClip handcuffIdle;
    public AudioClip handcuffAwake;
    public AudioClip bookIdle;
    public AudioClip bookAwake;
    private bool bookAwakePlayed;
    private bool handcuffAwakePlayed;
    // private AudioSource mic;        //commented out as the private field `InventoryControl.mic' is assigned but its value is never used
    private Player player;
    private MissionManager missionManager;
    private DialogManager dialogManager;
    public bool threatened = false;
    private bool givingHint = false;

    // Use this for initialization
    void Start()
    {
        // mic = GameObject.Find("DialogMicrophone").GetComponent<AudioSource>();
        player = gameObject.GetComponent<Player>();
        missionManager = FindObjectOfType<MissionManager>();
        dialogManager = gameObject.GetComponent<DialogManager>();
        HandCuffs.SetActive(false);
        WarningBook.SetActive(false);
    }

    public class Accumulator
    {
        public Accumulator(float ts)
        {
            timeSpan = ts;
        }
        float timeSpan = 2;
        float timer = 0;

        public bool IsFull(bool condition)
        {
            if (condition)
                timer += Time.deltaTime;
            else
                timer = 0;

            return timer > timeSpan;
        }
    }

    Accumulator warningTimer = new Accumulator(1);
    Accumulator cuffTimer = new Accumulator(1);
    // Accumulator hintTimer = new Accumulator(1);     //commented out as the private field `InventoryControl.hintTimer' is assigned but its value is never used
    // Accumulator threatenTimer = new Accumulator(.25f);      //commented out as the private field `InventoryControl.threatenTimer' is assigned but its value is never used

    // Update is called once per frame
    void Update()
    {
        if (player.cameraAngle > 65 && player.cameraAngle < 75)
        {
            HandCuffs.SetActive(true);
            AudioSource cuff = HandCuffs.GetComponent<AudioSource>();

            if (!cuff.isPlaying)
            {
                cuff.clip = handcuffIdle;
                cuff.PlayDelayed(0.5f);
            }
            ThreatenDialog();


        }

        else
        {
            HandCuffs.SetActive(false);
        }


        if (player.cameraAngle > 55 && player.cameraAngle < 65)
        {
            WarningBook.SetActive(true);
            AudioSource book = WarningBook.GetComponent<AudioSource>();
            if (!book.isPlaying)
            {
                book.clip = bookIdle;
                book.PlayDelayed(0.5f);
            }
            ThreatenDialog();

        }
        else
        {
            WarningBook.SetActive(false);
        }

        if (player.selectedCharacter != null)
        {
            if (cuffTimer.IsFull(player.cameraAngle > 65 && player.cameraAngle < 75))
            {
                if (!handcuffAwakePlayed)
                {
                    AudioSource cuff = HandCuffs.GetComponent<AudioSource>();
                    cuff.PlayOneShot(handcuffAwake);
                }
                handcuffAwakePlayed = true;
                missionManager.Arrest(player.selectedCharacter);
            }

            if (warningTimer.IsFull(player.cameraAngle > 55 && player.cameraAngle < 65))
            {
                if (!bookAwakePlayed)
                {
                    AudioSource book = WarningBook.GetComponent<AudioSource>();
                    book.PlayOneShot(bookAwake);
                }
                bookAwakePlayed = true;
                missionManager.Warn(player.selectedCharacter);
            }
        }
        else
        {
            bookAwakePlayed = false;
            handcuffAwakePlayed = false;
        }
        if ((missionManager.state != MissionManager.MissionState.EndByArrest) || (missionManager.state != MissionManager.MissionState.EndByWarning))
        {
            if (player.cameraAngle > 270 && player.cameraAngle < 300)
            {
                if (givingHint == false)
                {
                    GiveHint();
                }
            }
        }
    }

    public void ThreatenDialog()
    {
        AudioClip clip;
        if (player.selectedCharacter && !player.selectedCharacter.threatenedPlayed)
        {
            clip = player.selectedCharacter.threatenedClip;
            DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
            clips[0] = new DialogManager.Dialog(clip, player.selectedCharacter);
            dialogManager.setDialog(clips);
            player.selectedCharacter.threatenedPlayed = true;
        }
    }
    public void GiveHint()
    {
        givingHint = true;
        AudioClip clip;
        clip = missionManager.currentMission.clueDialogue;
        DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
        clips[0] = new DialogManager.Dialog(clip, SkyHint.transform);
        dialogManager.setDialog(clips);
    }

    public void Warn()
    {

    }

    public void Arrest()
    {

    }
}
