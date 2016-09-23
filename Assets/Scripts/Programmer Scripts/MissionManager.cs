using UnityEngine;
using System.Collections;

//JG to work on

public class MissionManager : MonoBehaviour
{

    Player player;

    //public GameObject currentSuspectSelected;
    Mission[] missions;

    public AudioClip arrestClip;
    public enum MissionState
    {
        Ongoing,
        EndByWarning,
        EndByArrest,
        MissionOver
    }

    public MissionState state = MissionState.Ongoing;

    public Mission currentMission;
    void Start()
    {
        missions = FindObjectsOfType<Mission>();
        currentMission = missions[0];
        player = FindObjectOfType<Player>();
        //player.transform.position = missions[0].startMissionPosition.transform.position;
    }

    void FixedUpdate()
    {

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
                if (currentMission.suspects[i].character.introPlayed)
                {
                    Player.Dialog[] clips = new Player.Dialog[2];
                    clips[0] = new Player.Dialog(currentMission.interogateSpeech, null);
                    clips[1] = new Player.Dialog(currentMission.suspects[i].explanation, currentMission.suspects[i].character);


                    player.setDialog(clips);
                }
                else
                {
                    if (i == 0)
                    {
                        AudioClip[] clips = new AudioClip[2];
                        clips[0] = player.nameClip;
                        clips[1] = currentMission.suspects[i].character.introClip;

                        player.setDialog(clips);
                        currentMission.suspects[i].character.introPlayed = true;
                    }
                    else
                    {
                        AudioClip[] clips = new AudioClip[4];
                        clips[0] = player.nameClip;
                        clips[1] = currentMission.interogateSpeech;
                        clips[2] = currentMission.suspects[i].character.introClip;
                        clips[3] = currentMission.suspects[i].explanation;

                        player.setDialog(clips);
                        currentMission.suspects[i].character.introPlayed = true;
                    }
                }
            }
        
        }
     
    }

    public void Complainant()
    {
        AudioClip clip;
        clip = currentMission.complainantSpeech;
        AudioClip[] clips = new AudioClip[1];
        clips[0] = clip;

        player.setDialog(clips);
    }


    public void Arrest(Character character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
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
                AudioClip[] clips = new AudioClip[3];
                clips[0] = currentMission.suspects[i].character.nameClip;
                clips[1] = arrestClip;
                clips[2] = clip;

                player.setDialog(clips);

            }
        }

    }


    public void Warn(Character character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
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
                AudioClip[] clips = new AudioClip[2];
                clips[0] = currentMission.suspects[i].playerWarning;
                clips[1] = clip;

                player.setDialog(clips);
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