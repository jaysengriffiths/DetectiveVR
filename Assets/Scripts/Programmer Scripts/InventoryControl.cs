using UnityEngine;
using System.Collections;

public class InventoryControl : MonoBehaviour
{

    [SerializeField]
    GameObject SkyHint;

    [SerializeField]
    GameObject HandCuffs;

    [SerializeField]
    GameObject WarningBook;

    //public float minAngle;
    //public float maxAngle;

    private Player player;
    private MissionManager missionManager;
    private DialogManager dialogManager;

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        missionManager = FindObjectOfType<MissionManager>();
        dialogManager = gameObject.GetComponent<DialogManager>();
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

    // Update is called once per frame
    void Update()
    {
        if (player.cameraAngle > 270 && player.cameraAngle < 280)
        {
            GiveClue();
        }

        if (player.selectedCharacter != null && player.selectedCharacter.introClip && player.selectedCharacter.IsInteracted)
        {
            if (cuffTimer.IsFull(player.cameraAngle > 60 && player.cameraAngle < 65))
            {
                HandCuffs.SetActive(true);
                missionManager.Arrest(player.selectedCharacter);
            }

            if (warningTimer.IsFull(player.cameraAngle > 65 && player.cameraAngle < 70))
            {
                WarningBook.SetActive(true);
                missionManager.Warn(player.selectedCharacter);
            }

            else
            {
                HandCuffs.SetActive(false);
                WarningBook.SetActive(false);
            }
        }
    }

    public void GiveClue()
    {
        Debug.Log("Give Clue");
        AudioClip clip;
        clip = missionManager.currentMission.clueDialogue;
        DialogManager.Dialog[] clips = new DialogManager.Dialog[1];  //Kathy
        clips[0] = new DialogManager.Dialog(clip);  //Kathy
        dialogManager.setDialog(clips);
    }

    public void Warn()
    {

    }

    public void Arrest()
    {

    }
}
