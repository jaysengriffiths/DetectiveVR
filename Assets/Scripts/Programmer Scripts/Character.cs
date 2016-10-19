using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public AudioClip nameClip;
    public AudioClip introClip;
    public AudioClip clothRip;
    public Transform MovingClueLocation;
    public bool introPlayed = false;
    public bool IsInteracted = false;
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
