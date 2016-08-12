using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class M1_GreetingScript : MonoBehaviour
{
	public static bool mapMovement = false;
    public static bool doorMovement = false;

    public GameObject Lady;
    public GameObject redFace;
    public GameObject map;
    public GameObject door;
    public GameObject mapStar;
    public GameObject fadeScreen;

    public Transform target;    //make the PC the target
    private float mapDistance = 0.75f;

    public float speed;

    public AudioClip L_01_GreetDetective_6s;
    public AudioClip L_02_LookToTalk_6s;
    public AudioClip PC_03_WhatHappened_2s;
    public AudioClip L_04_DontNeedDetective_4s;
    public AudioClip L_05_DontArrest_6s;
    public AudioClip L_06_MapTakeToCrimeScene_6s;
    public AudioClip L_07_KeepOutOfWay_4s;
    public AudioClip doorSlam;

    public AudioSource sourceLady;
    public AudioSource sourcePC;
    public AudioSource sourceDoor;


    void Start ()
    {
        redFace.SetActive(false);
        mapStar.SetActive(false);
        StartCoroutine("L01_GreetingDialogue");
    }

        IEnumerator L01_GreetingDialogue()
    {
        sourceLady.PlayOneShot(L_01_GreetDetective_6s);
        yield return new WaitForSeconds(6);
		
        sourceLady.PlayOneShot(L_02_LookToTalk_6s);
        yield return new WaitForSeconds(5);

        redFace.SetActive(true);

        yield return new WaitForSeconds(2);

        sourcePC.PlayOneShot(PC_03_WhatHappened_2s);
        yield return new WaitForSeconds(2);

        sourceLady.PlayOneShot(L_04_DontNeedDetective_4s);
        yield return new WaitForSeconds(4);

        sourceLady.PlayOneShot(L_05_DontArrest_6s);
        yield return new WaitForSeconds(6);

        mapMovement = true;

        sourceLady.PlayOneShot(L_06_MapTakeToCrimeScene_6s);
        yield return new WaitForSeconds(6);

        mapMovement = false;

        sourceLady.PlayOneShot(L_07_KeepOutOfWay_4s);
        yield return new WaitForSeconds(4);

		doorMovement = true;

        redFace.SetActive(false);

        yield return new WaitForSeconds(2);

        sourceDoor.PlayOneShot(doorSlam);

        yield return new WaitForSeconds(2);

        mapStar.SetActive(true);
        //should now be able to teleport by viewing mapStar
        //but since can't view mapStar, just teleport now

        //screen black for 1.5 seconds
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("LostCat");



    }

	void Update ()
    {
        //Lady always faces PC
       Lady.transform.LookAt(target);

        if (mapMovement == true && (Vector3.Distance(Lady.transform.position, map.transform.position) > mapDistance))
        {
            Lady.transform.position = Vector3.MoveTowards(Lady.transform.position, new Vector3(map.transform.position.x, 0.9f, map.transform.position.z), speed * Time.deltaTime);
        }

        if (doorMovement == true)
        {
			Lady.transform.position = Vector3.MoveTowards(Lady.transform.position, new Vector3 (door.transform.position.x, 0.9f, door.transform.position.z), speed * Time.deltaTime);
        }
 }

}
