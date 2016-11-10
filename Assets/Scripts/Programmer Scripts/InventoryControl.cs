using UnityEngine;
using System.Collections;

public class InventoryControl : MonoBehaviour
{ 
    public GameObject WarningBook;
    public GameObject SkyHint;
    public GameObject HandCuffs;
    private Player player;
    private MissionManager missionManager;
    private DialogManager dialogManager;
    private bool givingClue = false;

    // Use this for initialization
    void Start()
    {
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
            AudioClip clip;
            clip = player.selectedCharacter.threatenedClip;
            DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
            clips[0] = new DialogManager.Dialog(clip, player.selectedCharacter); 
            dialogManager.setDialog(clips);
            WarningBook.SetActive(true);
        }
        else
        {
            WarningBook.SetActive(false);
        }
   
        if (player.selectedCharacter != null && player.selectedCharacter.introClip && player.selectedCharacter.IsInteracted)
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
        if (player.cameraAngle > 270 && player.cameraAngle < 280)
        {
            GiveClue();
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
