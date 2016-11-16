using UnityEngine;
using System.Collections;

public class M6_BonfireMission : MonoBehaviour
{
    public Material nightSkybox;

    public Animator witchAnimator;
    public GameObject witch;
    public GameObject planeSupportingWitch;
    private bool logsSettled = false;
    public Transform log;
    public Transform log1;
    public Transform log2;
    public Transform log3;
    public Transform log4;
    public Transform log5;
    //private Vector3 logLastPos;
    //private Vector3 log1LastPos;
    //private Vector3 log2LastPos;
    //private Vector3 log3LastPos;
    //private Vector3 log4LastPos;
    //private Vector3 log5LastPos;
    //private float threshold = 0.1f; //minimum displacement before movement recognised
    public GameObject fireParticleEmitter;

    public Animator ladyOfManorAnimator;
    public GameObject torch;
    public BoxCollider torchBoxCollider1;
    public BoxCollider torchBoxCollider2;
    public Vector3 torchInHandPos;  //need to do this without relying on arbitrary figures.  Need angle z = -30
    public Vector3 torchLightFirePos;  //need to do this without relying on arbitrary figures.  Need angle z = -125
    public float speed = 1.0f;

    public AudioSource log_source;
    public AudioSource log1_source;
    public AudioSource log2_source;
    public AudioSource log3_source;
    public AudioSource log4_source;
    public AudioSource log5_source;

    public AudioSource beekeeper_source;
    public AudioSource bellringer_source;
    public AudioSource blacksmith_source;
    public AudioSource blacksmithwife_source;
    public AudioSource carpenter_source;
    public AudioSource cobbler_source;
    public AudioSource dairytroll_source;
    public AudioSource dragonkeeper_source;
    public AudioSource enk_source;
    public AudioSource farmer_source;
    public AudioSource farmwife_source;
    public AudioSource giantboy_source;
    public AudioSource giantgirl_source;
    public AudioSource goatherd_source;
    public AudioSource goblinboy_source;
    public AudioSource goblinchief_source;
    public AudioSource goblingirl_source;
    public AudioSource henkeeper_source;
    public AudioSource ladyOfManor_source;
    public AudioSource oldmiller_source;
    public AudioSource oldwoman_source;
    public AudioSource potter_source;
    public AudioSource trinketseller_source;
    public AudioSource trollbaby_source;
    public AudioSource witch_source;

    //setting
    public AudioClip PC_Night_1;
    //burn the witch
    public AudioClip M608_vBS_Burn_2;
    public AudioClip M609_vBW_Burn_2;
    public AudioClip M610_vOM_Burn_2;
    public AudioClip M611_vP_Burn_3;
    public AudioClip M612_vC_Burn_2;
    public AudioClip M613_vDK_Burn_3;
    public AudioClip M614_vHK_Burn_2;
    public AudioClip M615_vBR_Burn_2;
    public AudioClip M616_vB_Burn_2;
    public AudioClip M617_vG_Burn_2;
    public AudioClip M619_gCOB_Burn_2;
    public AudioClip M620_gTS_Burn_1;
    public AudioClip M621_gB_Burn_2;
    public AudioClip M622_gG_Burn_2;
    public AudioClip M623_tF_Burn_2;
    public AudioClip M624_tGH_Burn_2;
    public AudioClip M625_tBEE_Burn_2;
    public AudioClip M626_tDT_Burn_2;
    public AudioClip M627_tBAB_Burn_2;
    //instructions
    public AudioClip M602_Lady_BurnDontSave_7;
    public AudioClip M629_Lady_TorchToWood_6;
    public AudioClip M630_Lady_DontGiveBack_3;
    public AudioClip M631_Lady_DontPickUpLogs_5;
    public AudioClip M632_Lady_BonfireTogether_4;
    //decisions
    public AudioClip M633_PC_DecideSave_6;
    public AudioClip M634_Lady_SaveResponse_9;
    public AudioClip M635_W_ResponseToSave_10;
    public AudioClip M636_PC_BehaveNow_2;
    public AudioClip M637_W_GoToMountains_12;
    public AudioClip M638_Lady_Rubbish_14;
    public AudioClip M639_PC_AnyProof_1;
    public AudioClip M640_W_WoodFrom_2;
    public AudioClip M641_tFW_FromMountain_2;
    public AudioClip M644_gC_BegPCSave_11;
    public AudioClip M645_vBS_BegPCSave_4;
    public AudioClip M646_tFW_BegPCSave_3;
    public AudioClip M647_Lady_LookAtMe_3;
    public AudioClip M648_W_LookAtMe_2;
    public AudioClip M649_PC_DecideLeaveAfterSave_7;
    public AudioClip M650_W_ResponseToLeaveAfterSave_2;
    public AudioClip M651_Lady_ResponseToLeaveAfterSave_12;
    public AudioClip M652_PC_DecideStayAfterSave_9;
    public AudioClip M653_PC_DecideBurn_10;
    public AudioClip M654_W_ResponseToBurn_12;
    public AudioClip M655_PC_ReturnToHQAfterBurn_2;
    public AudioClip M656_Lady_ResponseToBurn_9;
    public AudioClip M657_Lady_ResponseToBurn_14;
    public AudioClip M658_Lady_LookAtTrophy_7;
    public AudioClip M659_Lady_DontLookAtMap_9;
    public AudioClip M660_PC_DecideStayAfterBurn_7;
    public AudioClip M661_PC_DecideLeaveAfterBurn_7;
    public AudioClip M662_Lady_ResponseToStayAfterBurn_14;
    public AudioClip M663_Lady_ResponseToLeaveAfterBurn_18;
    //end game decisions
    public AudioClip M642_W_FrostGiantWood_8;
    public AudioClip M643_W_OverarchingMystery_8;
    //hoorays
    public AudioClip M664_vBS_ResponseToStayAfterSave_3;
    public AudioClip M665_vOW_Cheer_2;
    public AudioClip M666_vBW_Cheer_1;
    public AudioClip M667_vOM_Cheer_2;
    public AudioClip M668_vP_Cheer_2;
    public AudioClip M669_vC_Cheer_2;
    public AudioClip M670_vDK_Cheer_2;
    public AudioClip M671_vHK_Cheer_2;
    public AudioClip M672_vBR_Cheer_2;
    public AudioClip M673_vB_Cheer_2;
    public AudioClip M674_vG_Cheer_2;
    public AudioClip M675_gC_Cheer_2;
    public AudioClip M676_gTS_Cheer_2;
    public AudioClip M677_gB_Cheer_2;
    public AudioClip M677_gCOB_Cheer_2;
    public AudioClip M678_gG_Cheer_3;
    public AudioClip M679_tF_Cheer_2;
    public AudioClip M680_tFW_Cheer_1;
    public AudioClip M681_tGH_Cheer_2;
    public AudioClip M682_tBEE_Cheer_2;
    public AudioClip M683_tDT_Cheer_2;
    public AudioClip M684_tBAB_Cheer_2;
    
    void Start()
    {
        RenderSettings.skybox = nightSkybox;
        ladyOfManorAnimator.SetTrigger("torch");
        witchAnimator.SetTrigger("isListening");
        //logLastPos = log.position;
        //log1LastPos = log1.position;
        //log2LastPos = log2.position;
        //log3LastPos = log3.position;
       // log4LastPos = log4.position;
       // log5LastPos = log5.position;
        StartCoroutine("Instructions");
    }

    private IEnumerator Instructions()

    {
        yield return new WaitForSeconds(2);
        enk_source.PlayOneShot(PC_Night_1);
        yield return new WaitForSeconds(2);

        //choice
        ladyOfManor_source.PlayOneShot(M602_Lady_BurnDontSave_7);
        yield return new WaitForSeconds(9);

        //burn the witch
        //next voice overlaps until pile of voices calling out
        //need to randomise order and wait time
        blacksmith_source.PlayOneShot (M608_vBS_Burn_2);
        yield return new WaitForSeconds(0.5f);
        blacksmithwife_source.PlayOneShot (M609_vBW_Burn_2);
        yield return new WaitForSeconds(0.5f);
        oldmiller_source.PlayOneShot (M610_vOM_Burn_2);
        yield return new WaitForSeconds(0.5f);
        potter_source.PlayOneShot (M611_vP_Burn_3);
        yield return new WaitForSeconds(0.5f);
        carpenter_source.PlayOneShot (M612_vC_Burn_2);
        yield return new WaitForSeconds(0.5f);
        dragonkeeper_source.PlayOneShot (M613_vDK_Burn_3);
        yield return new WaitForSeconds(0.5f);
        henkeeper_source.PlayOneShot (M614_vHK_Burn_2);
        yield return new WaitForSeconds(0.5f);
        bellringer_source.PlayOneShot (M615_vBR_Burn_2);
        yield return new WaitForSeconds(0.5f);
        giantboy_source.PlayOneShot (M616_vB_Burn_2);
        yield return new WaitForSeconds(0.5f);
        giantgirl_source.PlayOneShot (M617_vG_Burn_2);
        yield return new WaitForSeconds(0.5f);
        cobbler_source.PlayOneShot (M619_gCOB_Burn_2);
        yield return new WaitForSeconds(0.5f);
        trinketseller_source.PlayOneShot (M620_gTS_Burn_1);
        yield return new WaitForSeconds(0.5f);
        goblinboy_source.PlayOneShot (M621_gB_Burn_2);
        yield return new WaitForSeconds(0.5f);
        goblingirl_source.PlayOneShot (M622_gG_Burn_2);
        yield return new WaitForSeconds(0.5f);
        farmer_source.PlayOneShot (M623_tF_Burn_2);
        yield return new WaitForSeconds(0.5f);
        goatherd_source.PlayOneShot (M624_tGH_Burn_2);
        yield return new WaitForSeconds(0.5f);
        beekeeper_source.PlayOneShot (M625_tBEE_Burn_2);
        yield return new WaitForSeconds(0.5f);
        dairytroll_source.PlayOneShot (M626_tDT_Burn_2);
        yield return new WaitForSeconds(0.5f);
        trollbaby_source.PlayOneShot (M627_tBAB_Burn_2);
        yield return new WaitForSeconds(4);

        //instructions
        ladyOfManor_source.PlayOneShot(M629_Lady_TorchToWood_6);
        yield return new WaitForSeconds(7);
        ladyOfManor_source.PlayOneShot(M631_Lady_DontPickUpLogs_5);
        yield return new WaitForSeconds(6);
    }

    private void Update()
    {
        //detect log movement so player has saved witch
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 0.01f))

        /*detected movement but Enk does not hit hard enough in gameplay
        Vector3 offset = log.position - logLastPos;
        Vector3 offset1 = log1.position - log1LastPos;
        Vector3 offset2 = log2.position - log2LastPos;
        Vector3 offset3 = log3.position - log3LastPos;
        Vector3 offset4 = log4.position - log4LastPos;
        Vector3 offset5 = log5.position - log5LastPos;
        if ((offset.y > threshold) || (offset1.y > threshold) || (offset2.y > threshold) || (offset3.y > threshold) || (offset4.y > threshold) || (offset5.y > threshold))
        */

        {
            if (hit.transform.gameObject.tag == "log")
            {
                //stop torch working
                torchBoxCollider1.enabled = false;
                torchBoxCollider2.enabled = false;  //not sure how to distinguish 2 box colliders on same game object

                StopCoroutine("Instructions");
                StartCoroutine("WitchDescends");
                StartCoroutine("WitchSaved");
            }
        }

        //detect torch touch so player burns witch
        RaycastHit hit1;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit1, 0.01f))
        {
            if (hit1.transform.gameObject.tag == "torch")
            {
                //stop logs falling
                log.GetComponent<Rigidbody>().isKinematic = false;
                log1.GetComponent<Rigidbody>().isKinematic = false;
                log2.GetComponent<Rigidbody>().isKinematic = false;
                log3.GetComponent<Rigidbody>().isKinematic = false;
                log4.GetComponent<Rigidbody>().isKinematic = false;
                log5.GetComponent<Rigidbody>().isKinematic = false;

            StopCoroutine("Instructions");
            StartCoroutine("BurnWitch");
            }
        }
     }

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
    }

    //above temporary coroutine drops Witch until figure out how to send her slowly to WitchFreePos without using Update function
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

        }
        */

    private IEnumerator WitchSaved()
        {
            //witch saved dialogue
            yield return new WaitForSeconds(4);
            enk_source.PlayOneShot(M633_PC_DecideSave_6);
            yield return new WaitForSeconds(7);
            ladyOfManor_source.PlayOneShot(M634_Lady_SaveResponse_9);
            yield return new WaitForSeconds(10);
            witch_source.PlayOneShot(M635_W_ResponseToSave_10);
            witchAnimator.SetTrigger("isTalking");
            yield return new WaitForSeconds(11);
            witchAnimator.SetTrigger("isIdle");
            enk_source.PlayOneShot(M636_PC_BehaveNow_2);
            yield return new WaitForSeconds(4);
            witch_source.PlayOneShot(M637_W_GoToMountains_12);
            yield return new WaitForSeconds(13);
            ladyOfManor_source.PlayOneShot(M638_Lady_Rubbish_14);
            yield return new WaitForSeconds(15);
            enk_source.PlayOneShot(M639_PC_AnyProof_1);
            yield return new WaitForSeconds(3);
            witch_source.PlayOneShot(M640_W_WoodFrom_2);
            yield return new WaitForSeconds(3);
            farmwife_source.PlayOneShot(M641_tFW_FromMountain_2);
            yield return new WaitForSeconds(3);
            //choice to go or stay
            witch_source.PlayOneShot(M642_W_FrostGiantWood_8);
            yield return new WaitForSeconds(9);
            witchAnimator.SetTrigger("isTalking");
            witch_source.PlayOneShot(M643_W_OverarchingMystery_8);
            yield return new WaitForSeconds(9);
            witchAnimator.SetTrigger("isIdle");
            goblinchief_source.PlayOneShot(M644_gC_BegPCSave_11);
            yield return new WaitForSeconds(12);
            blacksmith_source.PlayOneShot(M645_vBS_BegPCSave_4);
            yield return new WaitForSeconds(5);
            farmwife_source.PlayOneShot(M646_tFW_BegPCSave_3);
            yield return new WaitForSeconds(4);
            witch_source.PlayOneShot(M648_W_LookAtMe_2);
            witchAnimator.SetTrigger("isTalking");
            yield return new WaitForSeconds(3);
            witchAnimator.SetTrigger("isIdle");
            ladyOfManor_source.PlayOneShot(M647_Lady_LookAtMe_3);
            yield return new WaitForSeconds(4);

            //in future will look at warning book to stay or handcuffs to go but temporarily no choice but to stay
            enk_source.PlayOneShot(M652_PC_DecideStayAfterSave_9);
            yield return new WaitForSeconds(11);
            //cheering
            //order and spacing needs to be randomised
            yield return new WaitForSeconds(2);
            blacksmith_source.PlayOneShot(M664_vBS_ResponseToStayAfterSave_3);
            yield return new WaitForSeconds(0.5f);
            blacksmithwife_source.PlayOneShot(M666_vBW_Cheer_1);
            yield return new WaitForSeconds(0.5f);
            oldmiller_source.PlayOneShot(M667_vOM_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            oldwoman_source.PlayOneShot(M665_vOW_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            potter_source.PlayOneShot(M668_vP_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            carpenter_source.PlayOneShot(M669_vC_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            dragonkeeper_source.PlayOneShot(M670_vDK_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            henkeeper_source.PlayOneShot(M671_vHK_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            bellringer_source.PlayOneShot(M672_vBR_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            giantboy_source.PlayOneShot(M673_vB_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            giantgirl_source.PlayOneShot(M674_vG_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            goblinchief_source.PlayOneShot(M675_gC_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            cobbler_source.PlayOneShot(M677_gCOB_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            trinketseller_source.PlayOneShot(M676_gTS_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            goblinboy_source.PlayOneShot(M677_gB_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            goblingirl_source.PlayOneShot(M678_gG_Cheer_3);
            yield return new WaitForSeconds(0.5f);
            farmer_source.PlayOneShot(M679_tF_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            farmwife_source.PlayOneShot(M680_tFW_Cheer_1);
            yield return new WaitForSeconds(0.5f);
            goatherd_source.PlayOneShot(M681_tGH_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            beekeeper_source.PlayOneShot(M682_tBEE_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            dairytroll_source.PlayOneShot(M683_tDT_Cheer_2);
            yield return new WaitForSeconds(0.5f);
            trollbaby_source.PlayOneShot(M684_tBAB_Cheer_2);
            yield return new WaitForSeconds(0.5f);
        }


        private IEnumerator BurnWitch()
        {
            //torch travels to fire and sets it alight
            yield return new WaitForSeconds(2);
            //torch.transform.position = Vector3.Lerp(torch.transform.position, torchLightFirePos, Time.deltaTime * speed); - can't use as not in Update
            torch.transform.position = torchLightFirePos;
            yield return new WaitForSeconds(2);
            ParticleSystem particleSystem = fireParticleEmitter.GetComponent<ParticleSystem>();
            var em = particleSystem.emission;
            em.enabled = true;

            enk_source.PlayOneShot(M653_PC_DecideBurn_10);
            yield return new WaitForSeconds(12);
            witch_source.PlayOneShot(M654_W_ResponseToBurn_12);
            witchAnimator.SetTrigger("isTalking");
            yield return new WaitForSeconds(13);
            witchAnimator.SetTrigger("isIdle");

            //enk is supposed to go back to HQ and get biggest trophy and be given choice to go or stay, not coded yet
            enk_source.PlayOneShot(M661_PC_DecideLeaveAfterBurn_7);
            yield return new WaitForSeconds(9);
            ladyOfManor_source.PlayOneShot(M662_Lady_ResponseToStayAfterBurn_14);
            yield return new WaitForSeconds(15);
        }
}
