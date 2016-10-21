﻿using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

    private MissionManager missionManager;
    private AudioSource audioSource;


    public struct Dialog
    {
        //This plays at suspect
        public Dialog(AudioClip _clip, Character _ch)
        {
            clip = _clip;
            transform = _ch.transform;
        }

        //This takes gameobject transform
        public Dialog(AudioClip _clip, Transform _trans)
        {
            clip = _clip;
            transform = _trans;
        }

        //plays at player
        public Dialog(AudioClip _clip)
        {
            clip = _clip;
            transform = null;
        }
        public AudioClip clip;
        public Transform transform;
    };

    //Dialog queue in memory
    public Dialog[] pendingDialog = new Dialog[0];

    // Use this for initialization
    void Start ()
    {
        GameObject mic = GameObject.Find("Microphone");
        if (mic)
            audioSource = mic.GetComponent<AudioSource>();
        missionManager = FindObjectOfType<MissionManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateDialog();
 
    }

    public void setDialog(Dialog[] array)
    {
        pendingDialog = array;
    }

    public void updateDialog()
    {
        //if not playing
        if (audioSource && !audioSource.isPlaying)
        {
            //and the length isnt 0
            if (pendingDialog.Length > 0)
            {

                //start audio
                if (pendingDialog[0].transform == null)
                {
                    audioSource.transform.position = transform.position; // snap to player
                }
                else
                {
                    audioSource.transform.position = pendingDialog[0].transform.position; // snap to player
                }

                // audio clip = the first pending
                audioSource.clip = pendingDialog[0].clip;
                audioSource.Play();

                //copy non playing left over audio into temp array tha twill become the pending once play is over 
                Dialog[] dialog = new Dialog[pendingDialog.Length - 1];

                for (int i = 0; i < dialog.Length; i++)
                {

                    dialog[i] = pendingDialog[i + 1];
                }
                pendingDialog = dialog;
            }
            else
            {
                if (missionManager.state == MissionManager.MissionState.EndByWarning)
                {
                    Debug.Log("play thankyou");

                    DialogManager.Dialog[] clips = new DialogManager.Dialog[1];  //Kathy
                    clips[1] = new DialogManager.Dialog(missionManager.currentMission.GetGuiltySuspect().thankyou);  //Kathy

                    setDialog(clips);
                }

                if (missionManager.state == MissionManager.MissionState.EndByArrest)
                {
                    Debug.Log("play overarching");
                    DialogManager.Dialog[] clips = new DialogManager.Dialog[1];  //Kathy
                    clips[0] = new DialogManager.Dialog(missionManager.currentMission.revelationSpeech);  //Kathy

                    setDialog(clips);

                }

                if (missionManager.state == MissionManager.MissionState.MissionOver)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        missionManager.currentMission.suspects[i].character.isSuspect = false;
                    }
                }


            }
        }
    }
}