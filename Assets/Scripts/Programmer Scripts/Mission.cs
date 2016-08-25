using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{
    public Clue clue;
    private bool cluesCollected;
    public struct Suspect
    {
        public Transform suspectPosition;
        public AudioClip missionAudio;
        public GameObject suspectPrefab;
    }

    public Clue[] clues;
    //public GameObject[] Prop;

    public enum ClueType
    {
        STATIC,
        ATTACHED
    };

    

    // Use this for initialization
    void Start ()
    {
        
        
    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(cluesCollected)
        {
            //warning book and handcuffs play sound and shine
        }	
	}
}
