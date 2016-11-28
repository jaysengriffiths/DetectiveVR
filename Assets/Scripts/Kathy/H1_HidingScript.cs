using UnityEngine;
using System.Collections;

public class H1_HidingScript : MonoBehaviour
{
    public AudioClip H01_vBW_Hide_1;
    public AudioClip H02_vDK_Hurry_3;
    public AudioClip H03_vHK_ChooksIn_2;
    public AudioClip H04_tF_Run_2;
    public AudioClip H05_vBR_3;

    public AudioClip HenHide3_2;
    public AudioClip HenHide4_3;

    public AudioSource sourceForge;
    public AudioSource sourceHenHouse;
    public AudioSource sourceStables;

    void Start ()
    {
        StartCoroutine("H1_HidingSounds");
    }

    IEnumerator H1_HidingSounds()
    {
        yield return new WaitForSeconds(2);

        sourceStables.PlayOneShot(H05_vBR_3);
        yield return new WaitForSeconds(2);

        sourceStables.PlayOneShot(H01_vBW_Hide_1);
        yield return new WaitForSeconds(1);

        sourceHenHouse.PlayOneShot(H04_tF_Run_2);
        yield return new WaitForSeconds(1.5f);

        sourceForge.PlayOneShot(H02_vDK_Hurry_3);
        yield return new WaitForSeconds(3);

        sourceHenHouse.PlayOneShot(H03_vHK_ChooksIn_2);
        yield return new WaitForSeconds(2);

        sourceHenHouse.PlayOneShot(HenHide3_2);
        yield return new WaitForSeconds(1);

        sourceHenHouse.PlayOneShot(HenHide4_3);
        yield return new WaitForSeconds(3);
    }
}
