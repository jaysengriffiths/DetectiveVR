using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{
    public class Suspect
    {
        GameObject character;
        public AudioClip wrongArrest;
        public AudioClip rightArrest;
        public AudioClip wrongWarn;
        public AudioClip rightWarn;
        public AudioClip suspect;
        public AudioClip thankyou;

    }
    
    //populate with characters from scene
    public Suspect[] suspects;

    public int guiltyIndex;
    //suspects[guiltyIndex
    void OnActivate()
    {
        guiltyIndex = Random.Range(0, 5);
    }

    // Use this for initialization
    void Start ()
    {
        
    
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if(cluesCollected)
        //{
        //    //warning book and handcuffs play sound and shine
        //}	
	}
}
