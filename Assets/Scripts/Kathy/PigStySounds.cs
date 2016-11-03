using UnityEngine;
using System.Collections;

public class PigStySounds : MonoBehaviour
{

    public AudioClip pig1_29;
    public AudioClip pig2_16;
    public AudioClip pig3_28;

    public AudioSource sourcePigSty;
    int i;

    void Start()
    {
        StartCoroutine("PigSty_sounds");
    }

    IEnumerator PigSty_sounds()
    {
        while (true)

        {
            i = Random.Range(0, 3);

            if (i == 0)
            {
                sourcePigSty.PlayOneShot(pig1_29);
                //Debug.Log(0);
                yield return new WaitForSeconds(1);
            }

            else if (i == 1)
            {
                sourcePigSty.PlayOneShot(pig2_16);
                //Debug.Log(1);
                yield return new WaitForSeconds(1);
            }

            else if (i == 2)
            {
                sourcePigSty.PlayOneShot(pig3_28);
                //Debug.Log(2);
                yield return new WaitForSeconds(1);
            }
        }
    }
}

