using UnityEngine;
using System.Collections;

public class SoundLookAt : MonoBehaviour {

    public AudioClip Idle;
    public AudioClip activated;
    public int timesPlayed;
    public int maxTimesPlayed;
    public int timeStamp;
    private AudioSource Source;
    int timer = 0;

    // Use this for initialization
    void Start ()
    {
        Source = gameObject.GetComponent<AudioSource>(); 
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Idle == null)
        {
            if (timer > 800)
            {
                Source.PlayOneShot(activated, 0.25f);
                timer = 0;
                Debug.Log("rip quiet");
                timer = 0;
            }
            timer++;
        }
    }
}
