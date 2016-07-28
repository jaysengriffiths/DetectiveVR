using UnityEngine;
using System.Collections;

public class house1_hidingScript : MonoBehaviour
{

	public AudioClip C4_IsHeThere_1;
    public AudioClip C5_BeingBewitched_2;
    public AudioClip C6_MagickedCow_4;

    public AudioSource sourceCBoy;
    public AudioSource sourceCGirl;

    void OnTriggerEnter()

    {
        StartCoroutine("ForgeHidingDialogue");
        Debug.Log("Forge Hiding dialogue begun");
    }

    IEnumerator ForgeHidingDialogue()
    {
        sourceCBoy.PlayOneShot(C4_IsHeThere_1);
        yield return new WaitForSeconds(1);

        sourceCGirl.PlayOneShot(C5_BeingBewitched_2);
        yield return new WaitForSeconds(2);

        sourceCBoy.PlayOneShot(C6_MagickedCow_4);
        yield return new WaitForSeconds(4);
    }
}
