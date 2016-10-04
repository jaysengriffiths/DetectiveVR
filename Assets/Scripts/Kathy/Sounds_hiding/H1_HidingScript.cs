using UnityEngine;
using System.Collections;

public class H1_HidingScript : MonoBehaviour
{
    public AudioClip L_02_Detective_2s;
    public AudioClip C1_QuickHide_2;
    public AudioClip C1A_Hurry_1;
    public AudioClip C1B_GetChooksIn_1;
    public AudioClip T1_RunAway_2;

    public AudioClip henChased_2;
    public AudioClip roosterChased_3;

    public AudioSource sourceForge;
    public AudioSource sourceHenHouse;
    public AudioSource sourceStables;

    void Start ()
    {
        StartCoroutine("H1_HidingSounds");
    }

    IEnumerator H1_HidingSounds()
    {
        sourceStables.PlayOneShot(L_02_Detective_2s);
        yield return new WaitForSeconds(2);

        sourceStables.PlayOneShot(T1_RunAway_2);
        yield return new WaitForSeconds(2);

        sourceForge.PlayOneShot(C1_QuickHide_2);
        yield return new WaitForSeconds(2);

        sourceHenHouse.PlayOneShot(C1A_Hurry_1);
        yield return new WaitForSeconds(1);

        sourceHenHouse.PlayOneShot(C1B_GetChooksIn_1);
        yield return new WaitForSeconds(1);

        sourceHenHouse.PlayOneShot(henChased_2);
        yield return new WaitForSeconds(1);

        sourceHenHouse.PlayOneShot(roosterChased_3);
        yield return new WaitForSeconds(3);
    }
}
