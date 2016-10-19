using UnityEngine;
using System.Collections;

public class DragonHouse_sounds : MonoBehaviour
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
                //Debug.Log(0);
                yield return new WaitForSeconds(10);
            }

            else if (i == 1)
            {
                sourceDragonH.PlayOneShot(Dragon2_4);
               //Debug.Log(1);
                yield return new WaitForSeconds(9);
            }

            else if (i == 2)
            {
                sourceDragonH.PlayOneShot(Dragon3_6);
               //Debug.Log(2);
                yield return new WaitForSeconds(11);
            }
          }
    }
}

