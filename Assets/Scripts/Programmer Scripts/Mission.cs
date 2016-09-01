using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{
    [System.Serializable]
    public class SoundFX : System.Object
    {
        public string Identifier;
    }

    [System.Serializable]
    public class Suspect : System.Object
    {
        public Character character;
        public string wrongArrest;
        public string rightArrest;
        public string wrongWarn;
        public string rightWarn;
        public string interrogate;
        public string thankyou;
    }
    
    //populate with characters from scene
    public Suspect[] suspects;
    public SoundFX[] soundFX;

    public int guiltyIndex;
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
