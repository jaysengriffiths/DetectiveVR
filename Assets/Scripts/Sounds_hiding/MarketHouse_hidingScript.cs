using UnityEngine;
using System.Collections;

public class MarketHouse_hidingScript : MonoBehaviour
{
    public AudioClip G3_DoYouThink_2;
    public AudioClip G4_ArrestMe_3;
    public AudioClip G5_ShutYourEyes_3;

    public AudioSource sourceGoblinBoy;
    public AudioSource sourceGoblinGirl;

    void OnTriggerEnter()

    {
        StartCoroutine("marketHHidingDialogue");
        Debug.Log("marketHHiding dialogue begun");
    }

    IEnumerator marketHHidingDialogue()
    {
        sourceGoblinBoy.PlayOneShot(G3_DoYouThink_2);
        yield return new WaitForSeconds(2);

        sourceGoblinGirl.PlayOneShot(G4_ArrestMe_3);
        yield return new WaitForSeconds(3);

        sourceGoblinBoy.PlayOneShot(G5_ShutYourEyes_3);
        yield return new WaitForSeconds(3);
    }
}
