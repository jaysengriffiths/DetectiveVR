using UnityEngine;
using System.Collections;

//JG to work on

public class MissionManager : MonoBehaviour
{
    public GameObject currentSuspectSelected;
    Mission[] missions;
    public Mission currentMission;
    void Start()
    {
        missions = FindObjectsOfType<Mission>();
        currentMission = missions[0];
    }

    void FixedUpdate()
    {

    }

    void PlaySound(AudioClip sound)
    {
        //use oculus sound library
    }

    void Arrest(GameObject character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                string clip;
                if (i == currentMission.guiltyIndex)
                {
                    clip = currentMission.suspects[i].rightArrest;
                }
                else
                {
                    clip = currentMission.suspects[i].wrongArrest;
                }
                //AudioClip audioClip = SoundLibrary.GetClip(clip);
                //PlaySound(audioClip);
            }
        }

    }

    void Warn(GameObject character)
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentMission.suspects[i].character == character)
            {
                string clip;
                if (i == currentMission.guiltyIndex)
                {
                    clip = currentMission.suspects[i].rightWarn;
                }
                else
                {
                    clip = currentMission.suspects[i].wrongWarn;
                }
                //AudioClip audioClip = SoundLibrary.GetClip(clip);
                //PlaySound(audioClip);
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