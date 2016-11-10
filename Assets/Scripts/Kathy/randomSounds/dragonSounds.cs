using UnityEngine;
using System.Collections;

public class dragonSounds : MonoBehaviour
{

    public AudioClip Dragon1_5;
    public AudioClip Dragon2_4;
    public AudioClip Dragon3_6;

    public AudioSource sourceDragonH;
    int i;

    void Start()
    {
        StartCoroutine("DragonH_sounds");
    }

    IEnumerator DragonH_sounds()
    {
        while (true)

        {
            i = Random.Range(0, 3);

            if (i == 0)
            {
                sourceDragonH.PlayOneShot(Dragon1_5);
                //Debug.Log(dragon0);
                yield return new WaitForSeconds(10);
            }

            else if (i == 1)
            {
                sourceDragonH.PlayOneShot(Dragon2_4);
                //Debug.Log(dragon1);
                yield return new WaitForSeconds(9);
            }

            else if (i == 2)
            {
                sourceDragonH.PlayOneShot(Dragon3_6);
                //Debug.Log(dragon2);
                yield return new WaitForSeconds(11);
            }
        }
    }
}
