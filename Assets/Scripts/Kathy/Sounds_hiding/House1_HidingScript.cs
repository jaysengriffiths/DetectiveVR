using UnityEngine;
using System.Collections;

public class House1_HidingScript : MonoBehaviour
{
    public AudioClip C2_Sh_1;
    public AudioClip C3_HesNear_2;

    public AudioSource sourceCFather;
    public AudioSource sourceCMother;

    void OnTriggerEnter()

    {
        StartCoroutine("House1HidingDialogue");
        Debug.Log("House1 Hiding dialogue begun");
    }

    IEnumerator House1HidingDialogue()
    {
        sourceCFather.PlayOneShot(C2_Sh_1);
        yield return new WaitForSeconds(1);

        sourceCMother.PlayOneShot(C3_HesNear_2);
        yield return new WaitForSeconds(2);
    }
}
