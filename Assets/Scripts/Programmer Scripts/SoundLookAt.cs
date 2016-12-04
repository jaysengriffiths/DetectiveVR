using UnityEngine;
using System.Collections;

public class SoundLookAt : MonoBehaviour
{
    public AudioClip additionalDialog;
    public float soundDelay = 1;
    public AudioClip[] Idle = new AudioClip[0];
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
        Source = gameObject.GetComponent<AudioSource>();
        player = FindObjectOfType<Player>().gameObject;

        // turn off if this is a mission marker in HQ, and the previous mission hasn't been completed
        if (missionName != "")
        {
            string[] missionNames = { "M1_Cat", "M2_TornCloth", "M3_MStalls", "M4_Lockets", "M5_Well", "M6_Bonfire", MA_Tracks, MC_Graffiti};
            string prevName = "";
            // start at 2 because the first two are always unlocked
            for (int i = 2; i < missionNames.Length; i++)
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
                        Source.PlayOneShot(activated, 0.25f);
                    }
                    else
                    {
                        //add delay between multiple sounds
                        if (Idle.Length != 0)
                        {
                            
                            Source.PlayOneShot(Idle[index], 1.0f);
                        }
                    }
                }
            }

            isPlaying = Source.isPlaying;
        }

    }
}
