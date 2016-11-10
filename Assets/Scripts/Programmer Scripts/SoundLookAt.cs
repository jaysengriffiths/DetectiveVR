using UnityEngine;
using System.Collections;

public class SoundLookAt : MonoBehaviour
{
    public float soundDelay = 3;
    public AudioClip[] Idle = new AudioClip[0];
    public AudioClip activated;
    public AudioClip enkNames;
    [HideInInspector]
    public int timesPlayed;
    public int maxTimesPlayed;
    public int activateDistance = 10;
    private AudioSource Source;
    //private Transform player;
    private bool isPlaying = false;
    private float nextTimeStamp = 0;

    public bool isClue = false;
    [HideInInspector]
    public bool isActivated = false;
    public bool enkNameObject = false;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
        player = FindObjectOfType<Player>().gameObject;
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
                    if (Idle.Length == 0)
                    {
                        
                        Source.PlayOneShot(activated, 0.25f);
                    }
                    else
                    {
                        //add delay between multiple sounds
                        if (Idle.Length != 0)
                        {
                            
                            Source.PlayOneShot(Idle[index], 0.25f);
                        }
                    }
                }
            }

            isPlaying = Source.isPlaying;
        }

    }
}
