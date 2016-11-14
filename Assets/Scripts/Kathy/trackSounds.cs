using UnityEngine;
using System.Collections;

public class trackSounds : MonoBehaviour

    //place on trackSource component of Player

{
    public AudioClip C0_Track_Blacksmith_7;
    public AudioClip S1_Track_Witch_3;
    public AudioClip S2_Track_Bellringer_4;
    public AudioClip S3_Track_Cobbler_5;
    public AudioClip S4_Track_Farmwife_5;

    public AudioSource trackSource;

    void Start()
    {

    }

    void detectTrack()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Track")
                {
                    if (hit.collider.gameObject.name == "track0")
                    {
                        trackSource.PlayOneShot(C0_Track_Blacksmith_7, 0);
                    }

                    else if (hit.collider.gameObject.name == "track1")
                    {
                        trackSource.PlayOneShot(S1_Track_Witch_3);
                    }

                    else if (hit.collider.gameObject.name == "track2")
                    {
                        trackSource.PlayOneShot(S2_Track_Bellringer_4);
                    }

                    else if (hit.collider.gameObject.name == "track3")
                    {
                        trackSource.PlayOneShot(S3_Track_Cobbler_5);
                    }

                    else if (hit.collider.gameObject.name == "track4")
                    {
                        trackSource.PlayOneShot(S4_Track_Farmwife_5);
                    }
                }
            }
        }
    }
}
