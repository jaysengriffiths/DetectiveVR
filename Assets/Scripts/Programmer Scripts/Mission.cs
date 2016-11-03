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
    public AudioClip clueComparison;
    public int guiltyIndex;
    public GameObject[] clueObjects;
    public Transform clueStartPos;
    public GameObject clueSpawned;

    //public Transform startMissionPosition;
    void OnActivate()
    {
        guiltyIndex = Random.Range(0, 5);
        clueSpawned = clueObjects[guiltyIndex];
        clueSpawned.SetActive(true);
        clueSpawned.transform.position = clueStartPos.position;
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
    void Awake()
    {
        OnActivate();
    }
    void Start ()
    {
    
	}
	
	void Update ()
    {
      
	}


}
