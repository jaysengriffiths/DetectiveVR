using UnityEngine;
using System.Collections;

public class CatWailing : MonoBehaviour
{
    public AudioClip catAngry1_1s;
    public AudioClip catAngry2_1s;
    public AudioClip catComplain1_3s;
    public AudioClip catComplain2_2s;
    public AudioClip catComplain3_4s;
    public AudioClip catComplain4_4s;
    public AudioClip catDemand1_1s;
    public AudioClip catDemand2_1s;
    public AudioClip catMoan_1s;
    public AudioClip catQuiet1_1s;
    public AudioClip catQuiet2_1s;
    public AudioClip catQuiet3_1s;
    public AudioClip catYowl1_3s;

    public AudioSource sourceCat;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Caterwauling");
        Debug.Log("Coroutine started");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Caterwauling()
    {
        sourceCat.PlayOneShot(catAngry1_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catAngry2_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catComplain1_3s);
        yield return new WaitForSeconds(3);

        sourceCat.PlayOneShot(catComplain2_2s);
        yield return new WaitForSeconds(2);

        sourceCat.PlayOneShot(catComplain3_4s);
        yield return new WaitForSeconds(4);

        sourceCat.PlayOneShot(catComplain4_4s);
        yield return new WaitForSeconds(4);

        sourceCat.PlayOneShot(catDemand1_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catDemand2_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catMoan_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catQuiet1_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catQuiet2_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catQuiet3_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catYowl1_3s);
        yield return new WaitForSeconds(3);
    }
}
