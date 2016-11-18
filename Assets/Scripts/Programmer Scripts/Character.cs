using UnityEngine;
using System.Collections.Generic;
using System;


public class Character : MonoBehaviour
{
    public AudioClip nameClip;
    public AudioClip introClip;
    public AudioClip awakeClip;
    public AudioClip threatenedClip;
    public Transform tornClothLocation;
    public Transform locketLocation;
    public Transform graffitiLocation;
    public Transform catHairLocation;

    [HideInInspector]
    public bool introPlayed, IsInteracted, isSuspect, arrestPlayed,
        warnPlayed, threatenedPlayed = false;
    //public string spawnPointName;

    // Use this for initialization
    void Start ()
    {
	        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
