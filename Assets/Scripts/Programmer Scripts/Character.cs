using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public AudioClip nameClip;
    public AudioClip introClip;
    public string warningcuff;
    public string identifier;
    public float lookAtTime;
    public string hqStatue;
    public string awake;
    public string idle;

    public bool introPlayed = false;
    //true if you're part of the current mission
    public bool isSuspect = false;

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
