using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public string identifier;
    public string idle;
    public string awake;
    public string intro;
    public string warningcuff;
    public string hqStatue;
    public float lookAtTime;
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
