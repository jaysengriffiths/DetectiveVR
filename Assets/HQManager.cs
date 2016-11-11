using UnityEngine;
using System.Collections;

public class HQManager : MonoBehaviour {

    public float startGameStart;
    public AudioClip[] greetSounds;

    DialogManager manager;

	// Use this for initialization
	void Start () {

        startGameStart = Time.time+5;
        manager = FindObjectOfType<DialogManager>();

        PlayerPrefs.SetInt("Greetings", 0);
    }
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetInt("Greetings") == 0)
        {

            if (startGameStart < Time.time)
            {
                int num = greetSounds.Length;
                DialogManager.Dialog[] clips = new DialogManager.Dialog[num];
                for (int i = 0; i < num; i++)
                    clips[i] = new DialogManager.Dialog(greetSounds[i]);
                manager.setDialog(clips);

                PlayerPrefs.SetInt("Greetings", 1);

            }
        }

	
	}
}
