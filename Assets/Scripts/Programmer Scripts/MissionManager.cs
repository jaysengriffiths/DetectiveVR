using UnityEngine;
using System.Collections;

//JG to work on

public class MissionManager : MonoBehaviour
{

    Player player;

    //public GameObject currentSuspectSelected;
    Mission[] missions;

    public AudioClip arrestClip;

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

    public void Arrest(GameObject character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                AudioClip clip;
                if (i == currentMission.guiltyIndex)
                {
                    clip = currentMission.suspects[i].rightArrest;
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

    public void Warn(GameObject character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                AudioClip clip;
                if (i == currentMission.guiltyIndex)
                {
                    clip = currentMission.suspects[i].rightWarn;
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