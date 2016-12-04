using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//JG to work on

public class MissionManager : MonoBehaviour
{
    Player player;
    //public GameObject currentSuspectSelected;
    public static Mission[] missions;
    private DialogManager dialogManager;
    // private AudioSource audioSource;        // commented out as the private field `MissionManager.audioSource' is assigned but its value is never used
    public AudioClip arrestClip;
    public AudioClip[] tutorialDialog;
    // private savedData saveGame;     //  commented out because the private field `MissionManager.saveGame' is assigned but its value is never used
    public enum MissionState
    {
        Ongoing,
        EndByWarning,
        EndByArrest,
        MissionOver
    }
    public MissionState state = MissionState.Ongoing;
    public Mission currentMission;
    public GameObject SkyHint;
    public bool congratulationsPlayed = false;  //Kathy
   
    //Set dialogmanager
    void Awake()
    {

    }
    void Start()
    {
        dialogManager = GameObject.Find("Enk").GetComponent<DialogManager>();
        //mic commented out as its value is never used
        /* 
            GameObject mic = GameObject.Find("DialogMicrophone");

            if (mic)
            {
                audioSource = mic.GetComponent<AudioSource>();
            }
        */

        // saveGame = GetComponent<savedData>();        //commented out because the private field `MissionManager.saveGame' is assigned but its value is never used
        missions = FindObjectsOfType<Mission>();
        // load the mission name to activate from the save game         //this was already commented out.  Don't think save game works
        string mission = PlayerPrefs.GetString("Mission");
            Debug.Log("Mission =  " + mission); //Kathy
        if (mission != "")
        {
            GameObject missionObj = GameObject.Find(mission);
            currentMission = missionObj.GetComponent<Mission>();
            Debug.Log("currentMission = " + currentMission);    //Kathy
        }
        if (currentMission == null)
        {
            //currentMission = missions[5]; //commented out as this always starts TornCloth mission
            SceneManager.LoadScene("HQ");
        }

        if (currentMission != null) //Kathy
        {
            currentMission.OnActivate();
            if (mission == "M1_Cat")
            {
                TutorialDialog();
            }
            else
            {
                Complainant();
            }
            // turn off all missions except the current one>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> turn them back on after missionOVer
            for (int i = 0; i < missions.Length; i++)
                if (missions[i] != currentMission)
                    missions[i].gameObject.SetActive(false);

            player = FindObjectOfType<Player>();
            player.transform.position = currentMission.enkSpawnPoint.transform.position;
        }
    }

    void Update()
    {
        if (state == MissionState.MissionOver)  //Kathy
        {
            StartCoroutine(MissionOverActions());
        }

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
                        clips[0] = new DialogManager.Dialog(currentMission.interrogateSpeech);
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
                            clips[1] = new DialogManager.Dialog(currentMission.interrogateSpeech);
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

    public void TutorialDialog()
    {
        DialogManager.Dialog[] clips = new DialogManager.Dialog[tutorialDialog.Length];
        for (int i = 0; i < tutorialDialog.Length; i++)
        {
            clips[i] = new DialogManager.Dialog(tutorialDialog[i]);
        }
        dialogManager.setDialog(clips);
    
    }
    public void Complainant()
    {
        AudioClip clip;
        clip = currentMission.complainantSpeech;
        DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
        clips[0] = new DialogManager.Dialog(clip, currentMission.suspects[0].character.transform);

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
                        PlayerPrefs.SetInt("missionManager.currentMission", 1); //Kathy
                        PlayerPrefs.Save();                                     //Kathy
                        print(PlayerPrefs.GetInt("missionManager.currentMission")); //Kathy
                        currentMission.complete = true;

                    }
                    else
                    {
                        clip = currentMission.suspects[i].wrongArrest;
                        player.selectedCharacter = null;
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
                        PlayerPrefs.SetInt("missionManager.currentMission", 2); //Kathy
                        PlayerPrefs.Save(); //Kathy
                        state = MissionState.EndByWarning;//Kathy
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
                    currentMission.complete = true;
                }
            }
        }
    }

    IEnumerator MissionOverActions()   //Kathy
    {
        if (!congratulationsPlayed)
        {
            yield return null;

            DialogManager.Dialog[] clips = new DialogManager.Dialog[1];
            clips[0] = new DialogManager.Dialog(currentMission.congratulationSpeech, SkyHint.transform);
           
            dialogManager.setDialog(clips);
            congratulationsPlayed = true;
        }
        
        //increase range of LadyOfManor homeplace audiosource
        
        
        
        //Setup skybox for mission environment
        //upon mission completion change skybox to night
    }
}