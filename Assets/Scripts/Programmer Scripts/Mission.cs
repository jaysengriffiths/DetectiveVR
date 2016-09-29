using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{

    [System.Serializable]
    public class Suspect : System.Object
    {

        public Character character;
        public AudioClip wrongArrest;
        public AudioClip rightArrest;
        public AudioClip playerWarning; 
        public AudioClip wrongWarn;
        public AudioClip rightWarn;
        public AudioClip explanation;
        public AudioClip thankyou;
        public GameObject thanker;
    }
    
    //populate with characters from scene
    public Suspect[] suspects;
    public AudioClip[] soundFX;
    public AudioClip mysterySpeech;
    public AudioClip revelationSpeech;
    public AudioClip complainantSpeech;
    public AudioClip interogateSpeech;
    public AudioClip clueDialogue;
    public int guiltyIndex;
    //public Transform startMissionPosition;
    void OnActivate()
    {
        guiltyIndex = Random.Range(0, 5);
        // find all characters and set their suspect flag to false
        Character[] allCharacters = GameObject.FindObjectsOfType<Character>();
        for (int i = 0; i < allCharacters.Length; i++)
            allCharacters[i].isSuspect = false;

        for (int i = 0; i < suspects.Length; i++)
        {
            suspects[i].character.isSuspect = true;
        }
    }

    public Suspect GetGuiltySuspect()
    {
        return suspects[guiltyIndex];
    }
    // Use this for initialization
    void Start ()
    {
        OnActivate();
    
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
