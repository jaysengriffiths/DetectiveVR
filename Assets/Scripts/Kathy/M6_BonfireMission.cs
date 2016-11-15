using UnityEngine;
using System.Collections;

public class M6_BonfireMission : MonoBehaviour
{

    public GameObject witch;
    //public Transform witchAtStakePos;
    //public Transform witchFreePos;
    public GameObject planeSupportingWitch;
    private bool logsSettled = false;

    public Transform log;
    public Transform log1;
    public Transform log2;
    public Transform log3;
    public Transform log4;
    public Transform log5;
    private Vector3 logLastPos;
    private Vector3 log1LastPos;
    private Vector3 log2LastPos;
    private Vector3 log3LastPos;
    private Vector3 log4LastPos;
    private Vector3 log5LastPos;
    private float threshold = 0.0f; //minimum displacement before movement recognised

    public AudioSource log_source;
    public AudioSource log1_source;
    public AudioSource log2_source;
    public AudioSource log3_source;
    public AudioSource log4_source;
    public AudioSource log5_source;


    void Start()
    {
        logLastPos = log.position;
        log1LastPos = log1.position;
        log2LastPos = log2.position;
        log3LastPos = log3.position;
        log4LastPos = log4.position;
        log5LastPos = log5.position;
}

    void Update()
    {
        Vector3 offset = log.position - logLastPos;
        Vector3 offset1 = log1.position - log1LastPos;
        Vector3 offset2 = log2.position - log2LastPos;
        Vector3 offset3 = log3.position - log3LastPos;
        Vector3 offset4 = log4.position - log4LastPos;
        Vector3 offset5 = log5.position - log5LastPos;
        if ((offset.y > threshold) || (offset1.y > threshold) || (offset2.y > threshold) || (offset3.y > threshold) || (offset4.y > threshold) || (offset5.y > threshold))
        {
            StartCoroutine("WitchDescends");
        }
    }
    
    void BonfireLogHit()

        {
            

        }
    
    /*
     * 

                if (witch.transform == witchAtStakePos)
                {
                    StartCoroutine("WitchDescends");
                    //StartCoroutine(WitchDescends (gameObject witch, Vector3 witchFreePos, float speed));
                    //StartCoroutine(WitchDescends (witch, new Vector3(6.06f, 1.05f, 7.41f), 5f));
                    */
    private IEnumerator WitchDescends() //two seconds after first log drops, witch descends
    {
        if (logsSettled == false)
        {
            yield return new WaitForSeconds(1);
            logsSettled = true;
        }

        else
        {
            yield return new WaitForSeconds(2);

            planeSupportingWitch.SetActive(false);
            //stop checking

            log_source.mute = true;  //temporarily mute audio source until figure out how to disable it
            log1_source.mute = true;
            log2_source.mute = true;
            log3_source.mute = true;
            log4_source.mute = true;
            log5_source.mute = true;
        }




        //temporarily transporting Witch to new arbitrary position until figure out how to send her slowly to WitchFreePos without using Update function
        //Vector3 newPos = new Vector3(6.06f, 1.05f, 7.41f);
        //witch.transform.position = newPos;

        /* possible way of moving witch without using Update function
            float secondX;
            float secondy;
            float secondz;
            Vector3 firstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 secondPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            while(transform.position.x < secondX)
             {
               transform.position = new Vecotr3(transform.position.x + 0.001f, transform.position.y, transform.position.z);
             }
        */

        /*another possible way of moving witch without using Update function
            private IEnumerator WitchDescends(witch, new Vector3(6.06f, 1.05f, 7.41f), 5f))
          {
              //while (witch.transform.position != witchFreePos)
              while (witch.transform.position != new Vector3(6.06f, 1.05f, 7.41f)
              {
                  float step = speed * Time.deltaTime;
                  witch.transform.position = Vector3.MoveTowards(witch.transform.position, Vector3(6.06f, 1.05f, 7.41f), speed * Time.deltaTime);
                  yield return new WaitForEndOfFrame();  //this enables script to work outside Update function
              }
          */
    }


}
