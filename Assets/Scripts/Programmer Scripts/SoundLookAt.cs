using UnityEngine;
using System.Collections;

public class SoundLookAt : MonoBehaviour
{

    public AudioClip Idle;
    public AudioClip activated;
    public AudioClip enkNames;
    public int timesPlayed;
    public int maxTimesPlayed;
    public int timeStamp;
    private AudioSource Source;
    //private Transform player;
    private bool isPlaying = false;
    private float nextTimeStamp = 0;
    //public bool Activated;
    public bool isClue = false;
    public bool isActivated = false;
    public bool enkNameObject = false;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
        player = FindObjectOfType<Player>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Source.isPlaying == false)
        {
            if (isPlaying)
            {
                //sound has just finished
                nextTimeStamp = Time.time + 3; // 3 == gap
            }
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                if (Time.time > nextTimeStamp)
                {
                    if (Idle == null)
                    {
                        Source.PlayOneShot(activated, 0.25f);
                        //isActivated = true;
                        if (GetComponent<MovingClue>() && isActivated)
                        {
                            
                            //Taking obj out of world and puts it in player hand
                            gameObject.SetActive(false);
                            //FindObjectOfType<Player>().clueObject = GetComponent<MovingClue>().gameObject;
                        }

                        

                    }
                    else
                    {
                        Source.PlayOneShot(Idle, 0.25f);
                    }
                }
            }

            isPlaying = Source.isPlaying;
        }

    }
}
