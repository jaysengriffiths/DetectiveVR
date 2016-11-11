using UnityEngine;
using System.Collections;

public class InventoryControl : MonoBehaviour
{ 
    public GameObject WarningBook;
    public GameObject SkyHint;
    public GameObject HandCuffs;
    private AudioSource mic;
    private Player player;
    private MissionManager missionManager;
    private DialogManager dialogManager;
    private bool givingClue = false;

    // Use this for initialization
    void Start()
    {
        mic = GameObject.Find("DialogMicrophone").GetComponent<AudioSource>();
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

    Accumulator warningTimer = new Accumulator(2);
    Accumulator cuffTimer = new Accumulator(2);
    Accumulator hintTimer = new Accumulator(2);

    // Update is called once per frame
    void Update()
    {
        if (!mic.isPlaying)
        {


            if (player.cameraAngle > 65 && player.cameraAngle < 75)
            {
                HandCuffs.SetActive(true);
            }

            else
            {
                HandCuffs.SetActive(false);
            }

            if (player.cameraAngle > 55 && player.cameraAngle < 65)
            {
                ThreatenDialog();
                WarningBook.SetActive(true);
            }
            else
            {
                WarningBook.SetActive(false);
            }
        }
        if (player.selectedCharacter != null)
        {
            if (cuffTimer.IsFull(player.cameraAngle > 65 && player.cameraAngle < 70))
            {
                missionManager.Arrest(player.selectedCharacter);
            }

            if (warningTimer.IsFull(player.cameraAngle > 60 && player.cameraAngle < 65))
            {
                missionManager.Warn(player.selectedCharacter);
            }
        }
        if (player.cameraAngle > 270 && player.cameraAngle < 300)
        {
            GiveClue();
        }

    }

    public void ThreatenDialog()
    {
        AudioClip clip;
        if (player.selectedCharacter)
        {
            clip = player.selectedCharacter.threatenedClip;
            DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
            clips[0] = new DialogManager.Dialog(clip, player.selectedCharacter);
            dialogManager.setDialog(clips);
        }
    }
    public void GiveClue()
    {
        AudioClip clip;
        clip = missionManager.currentMission.clueDialogue;
        DialogManager.Dialog[] clips = new DialogManager.Dialog[1];  //Kathy
        clips[0] = new DialogManager.Dialog(clip, SkyHint.transform);  //Kathy
        dialogManager.setDialog(clips);
    }

    public void Warn()
    {

    }

    public void Arrest()
    {

    }
}
