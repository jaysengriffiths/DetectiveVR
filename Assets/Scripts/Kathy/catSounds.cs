using UnityEngine;
using System.Collections;

public class catSounds : MonoBehaviour
{
    public AudioClip Cat_Chimney0_2;
    public AudioClip Cat_Chimney1_5;
    public AudioClip Cat_Chimney2_1;
    public AudioClip Cat_Chimney3_4;
    public AudioClip Cat_Chimney4_2;
    public AudioClip M118_PC_SheInChimney_2;

    public AudioSource sourceEnk;
    public AudioSource sourceCat;
    int i;

    void Start()
    {
        StartCoroutine("Cat_sounds");
    }

    IEnumerator Cat_sounds()
    {
        while (true)

        {
            i = Random.Range(0, 5);

            if (i == 0)
            {
                sourceCat.PlayOneShot(Cat_Chimney0_2);
                //Debug.Log(0);
                yield return new WaitForSeconds(4);
            }

            else if (i == 1)
            {
                sourceCat.PlayOneShot(Cat_Chimney1_5);
                //Debug.Log(1);
                yield return new WaitForSeconds(7);
            }

            else if (i == 2)
            {
                sourceCat.PlayOneShot(Cat_Chimney2_1);
                //Debug.Log(2);
                yield return new WaitForSeconds(1);
            }

            else if (i == 3)
            {
                sourceCat.PlayOneShot(Cat_Chimney3_4);
                //Debug.Log(1);
                yield return new WaitForSeconds(4);
            }

            else if (i == 4)
            {
                sourceCat.PlayOneShot(Cat_Chimney4_2);
                //Debug.Log(2);
                yield return new WaitForSeconds(2);
            }
        }
    }
}
