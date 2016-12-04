using UnityEngine;
using System.Collections;

public class SoundLookAt : MonoBehaviour
{
    public AudioClip additionalDialog;
    public float soundDelay = 1;
    public AudioClip[] Idle = new AudioClip[0]; //do not use routinely as it blocks up chain of dialog clips
    public AudioClip activated;
    public AudioClip enkNames;
    [HideInInspector]
    public int activateDistance = 10;
    private AudioSource Source;
    //private Transform player;
    private bool isPlaying = false;
    private float nextTimeStamp = 0;

    public bool isClue = false;
    public bool isActivated = false;
    public bool enkNameObject = false;
    private GameObject player;

    public string missionName;

    // Use this for initialization
    void Start()
    {   
        player = FindObjectOfType<Player>().gameObject;

        if (Source != null)         //added by Kathy to get rid of warnings clogging up the console
        {
            Source = gameObject.GetComponent<AudioSource>();
        }

        /*if (Source == null)                          - still get error that script is trying to access audio source
            {
                Source = null;
            }
        */

        /*
        if (this.gameObject.GetComponent<AudioSource>() != null)
        {
            Source = this.gameObject.GetComponent<AudioSource>();
        }
        */

        /*if this is a mission marker in HQ, if prerequisite missions completed, leave on, else turn off

       if (missionName != "")
         {
            //this.gameObject.SetActive(false);


            /*if (PlayerPrefs.GetInt("missionManager.currentMission", reward) = 1
            print(PlayerPrefs.GetInt("missionManager.currentMission"));

            if (missionName == "M1_Cat"  && MissionManager.currentMission.complete = true;


            for (PlayerPrefs.SetInt(mm.currentMission.name, reward)
                {
                PlayerStats.maxExp = PlayerPrefs.GetInt("max_exp");
                PlayerPrefs.SetInt(mm.currentMission.name, reward); // could save out 1 = trophy 2 = present                          PlayerPrefs.SetString("missionManager.currentMission", "Complete");
                PlayerPrefs.SetInt("missionManager.currentMission", 2);
                }
        */


        // turn off if this is a mission marker in HQ, and the previous mission hasn't been completed
        if (missionName != "")
            {
                string[] missionNames = { "M1_Cat", "M2_TornCloth", "M3_MStalls", "M4_Lockets", "M5_Well", "M6_Bonfire"};
                string prevName = "";
                // start at 1 because the first one is always unlocked
                for (int i = 1; i < missionNames.Length; i++)
                {
                    if (missionName == missionNames[i])
                        prevName = missionNames[i - 1];
                }

                if (prevName != "")
                {
                    if (PlayerPrefs.GetInt(prevName) == 0)
                        gameObject.SetActive(false);
                }
            }
        }




        // Update is called once per frame
        void Update()
    {
       
        int index = Random.Range(0, Idle.Length);

        if (Source != null)             //Kathy
        {
            if (Source.isPlaying == false)
            {
                if (isPlaying)
                {
                    //sound has just finished
                    nextTimeStamp = Time.time + soundDelay; // 3 == gap
                }
                if (Vector3.Distance(player.transform.position, transform.position) < activateDistance)
                {
                    if (Time.time > nextTimeStamp)
                    {
                        if (isActivated && activated)
                        {
                            Source.PlayOneShot(activated, 1);   //changed from 0.25 to 1 as volume of speech was too quiet compared to other sounds
                        }
                        else
                        {
                            //add delay between multiple sounds  //this randomises idle sounds
                            if (Idle.Length != 0)
                            {

                                Source.PlayOneShot(Idle[index], 1.0f);
                            }
                        }
                    }
                }
                
                isPlaying = Source.isPlaying;       //this stops sounds occurring on top of each other so SoundLookAt script should only be used for dialogue
            }
        }

    }
}
