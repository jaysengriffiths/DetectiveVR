using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{

    [System.Serializable]
    public class Suspect : System.Object
    {

        public Character character;
        public AudioClip explanation;
        public AudioClip wrongArrest;
        public AudioClip rightArrest;
        public AudioClip playerWarning; 
        public AudioClip wrongWarn;
        public AudioClip rightWarn;
        public AudioClip thankyou;
        public GameObject thanker;
        public Transform spawnPoint;
    }

    //populate with characters from scene
    public int missionName;
    private GameObject mission;
    public Suspect[] suspects;
    public AudioClip[] soundFX;
    public AudioClip mysterySpeech;
    public AudioClip revelationSpeech;
    public AudioClip complainantSpeech;
    public AudioClip interrogateSpeech;
    public AudioClip clueDialogue;
    public AudioClip clueComparison;
    [HideInInspector]
    public int guiltyIndex;
    public GameObject[] clueObjects;
    public Transform clueStartPos;
    public GameObject clueSpawned;
    public GameObject enkSpawnPoint;
    public GameObject[] objDisable;
    public GameObject[] soundObjDisable;
    public bool complete = false;
    public Material skybox;

    //public Transform startMissionPosition;
    public void OnActivate()
    {
        guiltyIndex = Random.Range(0, 5);

        if (clueObjects != null && clueObjects.Length > guiltyIndex)
        {
            clueSpawned = clueObjects[guiltyIndex];
            clueSpawned.SetActive(true);
            clueSpawned.transform.position = clueStartPos.position;
        }
        // find all characters and set their suspect flag to false
        Character[] allCharacters = GameObject.FindObjectsOfType<Character>();
        for (int i = 0; i < allCharacters.Length; i++)
            allCharacters[i].isSuspect = false;

        for (int i = 0; i < suspects.Length; i++)
        {
            suspects[i].character.isSuspect = true;
            Vector3 spawnPos = suspects[i].spawnPoint.position;
            spawnPos.y = suspects[i].character.transform.position.y;
            suspects[i].character.transform.position = spawnPos;
            suspects[i].character.transform.rotation = suspects[i].spawnPoint.rotation;
            suspects[i].character.gameObject.SetActive(true);
        }

        RenderSettings.skybox = skybox;
    }

    public Suspect GetGuiltySuspect()
    {
        return suspects[guiltyIndex];
    }
    // Use this for initialization


    public int GetIndexOfCharacter(Character ch)
    {
        for (int i = 0; i < suspects.Length; i++)
        {
            if (suspects[i].character == ch)
            {
                return i;
            }
        }
        return -1;
    }
    void Start()
    {
        TurnOffObj();
    }
	
	void Update ()
    {
      
	}

    void TurnOffObj()
    {
        for (int i = 0; i < objDisable.Length; i++)
        {
            objDisable[i].SetActive(false);
        }

        for (int i = 0; i < soundObjDisable.Length; i++)
        {
            soundObjDisable[i].GetComponent<AudioSource>().Pause();  //brings up NullReferenceExceptions
        }
    }
}
