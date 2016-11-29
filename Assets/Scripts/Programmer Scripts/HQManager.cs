using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HQManager : MonoBehaviour {

   
    public float startGameStart;
    public float ladyLeave;
    public float ladyEnter;
    public AudioClip[] greetSounds;
    public GameObject lady;
    public GameObject startRotate;
    public GameObject endRotate;
    DialogManager manager;
    public Camera loading;

    void Awake()
    {
        PlayerPrefs.SetInt("Greetings", 0);  //doesn't work
    }

	// Use this for initialization
	void Start ()
    {
        startGameStart = Time.time+5;
        ladyEnter = Time.time + 4;
        ladyLeave = Time.time + 48;
        manager = FindObjectOfType<DialogManager>();

        /*
            //uncomment for lady to appear in every visit to hq
            //PlayerPrefs.SetInt("Greetings", 0);  //Kathy and Adam

            if (PlayerPrefs.GetInt("M1_Cat") == 1)  //Kathy and Adam
            //if(PlayerPrefs.GetInt("Greetings") == 1)
        */
        if(PlayerPrefs.GetInt("Greetings") == 1)
        {
            lady.SetActive(false);
        } 
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
        
        if (ladyEnter < Time.time)
        {

            lady.transform.position = Vector3.MoveTowards(lady.transform.position, startRotate.transform.position, 0.01f);
            lady.transform.rotation = Quaternion.Lerp(lady.transform.rotation, startRotate.transform.rotation, 0.01f);
        }

        if (ladyLeave < Time.time)
        {
            lady.transform.rotation = Quaternion.Lerp(lady.transform.rotation, endRotate.transform.rotation, 0.1f);
            lady.transform.position = Vector3.MoveTowards(lady.transform.position, endRotate.transform.position, 0.02f);


        }



    }
}
