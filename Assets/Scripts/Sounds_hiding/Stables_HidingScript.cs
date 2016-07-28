using UnityEngine;
using System.Collections;

public class Stables_HidingScript : MonoBehaviour
{
    public AudioClip T2_ActLikePost_4;
    public AudioClip T3_Breathing_2;

    public AudioSource sourceTrollFather;
    public AudioSource sourceTrollMother;

    void OnTriggerEnter()

    {
        StartCoroutine("Stables_HidingDialogue");
        Debug.Log("Stables Hiding dialogue begun");
    }

    IEnumerator Stables_HidingDialogue()
    {
        sourceTrollMother.PlayOneShot(T2_ActLikePost_4);
        yield return new WaitForSeconds(4);

        sourceTrollFather.PlayOneShot(T3_Breathing_2);
        yield return new WaitForSeconds(2);
    }
}
