using UnityEngine;
using System.Collections;

public class chimneySound : MonoBehaviour
{
    public Vector3 chimneyDialogPlace;
    public AudioClip M118_PC_SheInChimney_2;
    private bool chimneyDiscoveryPlayed = false;

    void OnCollisionEnter()
    {
        Debug.Log("Enk went to chimney");
        StartCoroutine("PlayChimneyDiscovery");
    }

    IEnumerator PlayChimneyDiscovery()
    {
        if (chimneyDiscoveryPlayed == false)
        {
            chimneyDiscoveryPlayed = true;
            AudioSource.PlayClipAtPoint(M118_PC_SheInChimney_2, chimneyDialogPlace);
            yield return new WaitForSeconds(4);
            chimneyDiscoveryPlayed = false;
        }
    }
}
