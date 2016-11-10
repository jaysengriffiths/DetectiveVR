using UnityEngine;
using System.Collections;

//JG to work on

public class MissionManager : MonoBehaviour
{
    Player player;
    //public GameObject currentSuspectSelected;
    public static Mission[] missions;
    private DialogManager dialogManager;
    private AudioSource audioSource;
    public AudioClip arrestClip;
    private savedData saveGame;
    public Mission[] missionList;

    public enum MissionState
    {
        Ongoing,
        EndByWarning,
        EndByArrest,
        MissionOver
    }
    public MissionState state = MissionState.Ongoing;
    public Mission currentMission;
   
    //Set Mic and dialogmanager
    void Awake()
    {
        dialogManager = GameObject.FindGameObjectWithTag("Player").GetComponent<DialogManager>();
        GameObject mic = GameObject.Find("Microphone");
        if (mic)
            audioSource = mic.GetComponent<AudioSource>();
    }
    void Start()
    {
        saveGame = GetComponent<savedData>();
        missions = FindObjectsOfType<Mission>();
        // load the mission name to activate from the save game
        string mission = PlayerPrefs.GetString("Mission");
        if (mission != "")
        {
            GameObject missionObj = GameObject.Find(mission);
            currentMission = missionObj.GetComponent<Mission>();
        }
        if (currentMission == null)
        {
            currentMission = missions[3];
        }
        // turn off all missions excepot the current one
        for (int i = 0; i < missions.Length; i++)
            if (missions[i] != currentMission)
                missions[i].gameObject.SetActive(false);

       
        player = FindObjectOfType<Player>();
        player.transform.position = currentMission.enkSpawnPoint.transform.position;
    }

    void Update()
    {

        if (player.clueObject && player.clueComparisonPlayed && dialogManager.pendingDialog.Length < 1)
        {
            if (player.selectedCharacter)
            {
                player.clueObject.GetComponent<MovingClue>().MoveTowards(player.selectedCharacter);
            }

            //player.clueObject.transform.eulerAngles = new Vector3(0, -90, 0);
        }    
        //if (player.clueObject && player.clueObject.activeSelf == true && dialogManager.pendingDialog.Length == 0 && player.selectedCharacter != null)
        //{
           
        //}
    }

    void PlaySound(AudioClip sound)
    {
        //use oculus sound library
    }

    public void Interrogate(Character character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                if (!character.arrestPlayed && !character.warnPlayed)
                {
                    if (currentMission.suspects[i].character.introPlayed)
                    {
                        int num = player.clueObject ? 3 : 2;
                        DialogManager.Dialog[] clips = new DialogManager.Dialog[num];
                        clips[0] = new DialogManager.Dialog(currentMission.interogateSpeech);
                        clips[1] = new DialogManager.Dialog(currentMission.suspects[i].explanation, currentMission.suspects[i].character);

                        if (player.clueObject)
                        {
                            clips[2] = new DialogManager.Dialog(currentMission.clueComparison);
                            player.clueComparisonPlayed = true;
                        }
                        ActivateMovingClue();

                        dialogManager.setDialog(clips);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
                            clips[0] = new DialogManager.Dialog(player.nameClip);
                            clips[1] = new DialogManager.Dialog(currentMission.suspects[i].character.introClip, currentMission.suspects[i].character);

                            dialogManager.setDialog(clips);
                            currentMission.suspects[i].character.introPlayed = true;
                            //currentMission.suspects[i].character.IsInteracted = true;

                        }
                        else
                        {
                            int num = player.clueObject ? 5 : 4;
                            DialogManager.Dialog[] clips = new DialogManager.Dialog[num];
                            clips[0] = new DialogManager.Dialog(player.nameClip);
                            clips[1] = new DialogManager.Dialog(currentMission.interogateSpeech);
                            clips[2] = new DialogManager.Dialog(currentMission.suspects[i].character.introClip, currentMission.suspects[i].character);
                            clips[3] = new DialogManager.Dialog(currentMission.suspects[i].explanation, currentMission.suspects[i].character);

                            currentMission.suspects[i].character.introPlayed = true;
                            //currentMission.suspects[i].character.IsInteracted = true;
                            if (player.clueObject)
                            {
                                clips[4] = new DialogManager.Dialog(currentMission.clueComparison);
                                player.clueComparisonPlayed = true;
                            }
                            ActivateMovingClue();
                            dialogManager.setDialog(clips);
                        }
                    }
                }
            }
        
        }
     
    }

    public void ActivateMovingClue()
    {
        if(player.clueObject == null)
        {
            return;
        }

        player.clueObject.SetActive(true);
        
    }
    public void Complainant()
    {
        AudioClip clip;
        clip = currentMission.complainantSpeech;
        DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
        clips[0] = new DialogManager.Dialog(clip);

        dialogManager.setDialog(clips);
    }
    public void Arrest(Character character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                if (!character.arrestPlayed)
                {
                    AudioClip clip;
                    if (i == currentMission.guiltyIndex)
                    {
                        clip = currentMission.suspects[i].rightArrest;
                        state = MissionState.EndByArrest;
                    }
                    else
                    {
                        clip = currentMission.suspects[i].wrongArrest;
                    }
                    DialogManager.Dialog[] clips = new DialogManager.Dialog[3];
                    clips[0] = new DialogManager.Dialog(currentMission.suspects[i].character.nameClip);
                    clips[1] = new DialogManager.Dialog(arrestClip);
                    clips[2] = new DialogManager.Dialog(clip);

                    dialogManager.setDialog(clips, DialogManager.DialogType.Arrest);
                    character.arrestPlayed = true;
                    
                }
                
            }
        }

    }
    public void Warn(Character character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                if (!character.warnPlayed)
                {
                    AudioClip clip;
                    if (i == currentMission.guiltyIndex)
                    {
                        clip = currentMission.suspects[i].rightWarn;
                        state = MissionState.EndByWarning;
                    }
                    else
                    {
                        clip = currentMission.suspects[i].wrongWarn;
                    }
                    DialogManager.Dialog[] clips = new DialogManager.Dialog[2];
                    clips[0] = new DialogManager.Dialog(currentMission.suspects[i].playerWarning);
                    clips[1] = new DialogManager.Dialog(clip);

                    dialogManager.setDialog(clips, DialogManager.DialogType.Warning);
                    character.warnPlayed = true;
                }
            }
        }
    }

    void UpdateSkybox()
    {
        //Setup skybox for mission environment
        //upon mission completion change skybox to night
    }


    void AwardObject()
    {
        //
    }  
}