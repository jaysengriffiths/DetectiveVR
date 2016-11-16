using UnityEngine;
using System.Collections;

public class MovingClue : MonoBehaviour
{
    public float speed;
    private Player player;
    private SoundLookAt soundLook;
    bool movingToTarget = false;
    private MissionManager missionManager;

    void Awake()
    {
   
    }
    void Start ()
    {
        missionManager = FindObjectOfType<MissionManager>();
        player = FindObjectOfType<Player>();
        soundLook = GetComponent<SoundLookAt>();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundLook.isActivated && !movingToTarget)
        {
            gameObject.transform.position = player.enkHoldingCloth.transform.position;
            gameObject.transform.rotation = player.enkHoldingCloth.transform.rotation;
            player.clueObject = this.gameObject;
            //gameObject.GetComponent<SoundLookAt>().enabled = false;
            //gameObject.SetActive(false);
        }

        if(player.selectedCharacter == null)
        {
            movingToTarget = false;
            player.clueComparisonPlayed = false;
        }
    }

    public void MoveTowards(Character character)
    {
        DialogManager dialogManager = GameObject.FindGameObjectWithTag("Player").GetComponent<DialogManager>();
        movingToTarget = true;
        float step = speed * Time.deltaTime;
        Transform clueTransform = character.transform;
        if (character.tornClothLocation != null)
            clueTransform = character.tornClothLocation;
        if ((transform.position - clueTransform.position).magnitude < 0.1f)
        {
            DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
            clips[0] = new DialogManager.Dialog(soundLook.activated, player.selectedCharacter.tornClothLocation);
            clips[1] = new DialogManager.Dialog(missionManager.currentMission.soundFX[missionManager.currentMission.GetIndexOfCharacter(player.selectedCharacter)], player.selectedCharacter.tornClothLocation);
            dialogManager.setDialog(clips);

        }
        else
            transform.position = Vector3.MoveTowards(transform.position, clueTransform.position, step);
    }

}
