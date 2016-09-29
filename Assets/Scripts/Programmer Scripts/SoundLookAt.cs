using UnityEngine;
using System.Collections;

public class SoundLookAt : MonoBehaviour {

    public AudioClip onHover;
    public AudioClip activated;
    public int timesPlayed;
    public int maxTimesPlayed;

    // Use this for initialization
    void Start () {
       
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(timesPlayed == maxTimesPlayed)
        {
            gameObject.SetActive(false);
        }
	}
}
