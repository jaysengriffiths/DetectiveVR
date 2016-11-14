using UnityEngine;
using System.Collections;

public class logSounds : MonoBehaviour
{
    //attach to each log so when log is hit, audio source instantiates at collision point, plays click sound depending on how hard hit(with random pitch), then self-destructs
    //note that using PlayClipAtPoint precludes the ability to set volume of clip play or rotation of audio source

    public AudioSource log_source;

    public AudioClip Idle_Log_L;
    public AudioClip Dropped_Log_1;

    private float velocityClipSplit = 1F;

    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    bool initialGroundContact = true;

    void OnCollisionEnter(Collision collision)
    {
        {
            Debug.Log("log contact detected");
            if (initialGroundContact == true)
            {
                initialGroundContact = false;
            }
            else
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 pos = contact.point;

                log_source.pitch = Random.Range(lowPitchRange, highPitchRange);

                if (collision.relativeVelocity.magnitude < velocityClipSplit)
                {
                    AudioSource.PlayClipAtPoint(Idle_Log_L, pos);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(Dropped_Log_1, pos);
                }
            }
        }
    }
}
