using UnityEngine;
using System.Collections;

public class House2_hidingScript : MonoBehaviour
{

    public AudioClip G1_BeArrested_3;
    public AudioClip G2_Worse_3;

    public AudioSource sourceGoblinFather;
    public AudioSource sourceGoblinMother;

    void OnTriggerEnter()

    {
        StartCoroutine("House2HidingDialogue");
        Debug.Log("House2Hiding dialogue begun");
    }

    IEnumerator House2HidingDialogue()
    {
        sourceGoblinFather.PlayOneShot(G1_BeArrested_3);
        yield return new WaitForSeconds(3);

        sourceGoblinMother.PlayOneShot(G2_Worse_3);
        yield return new WaitForSeconds(3);
    }
}
