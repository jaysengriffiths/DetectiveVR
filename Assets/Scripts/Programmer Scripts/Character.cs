using UnityEngine;
using System.Collections.Generic;
using System;


public class Character : MonoBehaviour
{
    public AudioClip nameClip;
    public AudioClip introClip;
    public AudioClip awakeClip;
    public Transform tornClothLocation;
    public bool introPlayed = false;
    public bool IsInteracted = false;
    //true if you're part of the current mission
    public bool isSuspect = false;
    public bool arrestPlayed = false;
    public bool warnPlayed = false;
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
