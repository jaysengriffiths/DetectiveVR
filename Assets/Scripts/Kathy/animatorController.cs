using UnityEngine;
using System.Collections;

public class animatorController : MonoBehaviour
{
    static Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("isIdle");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("isListening");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("isTalking");
        }

        if (Input.GetKeyDown(KeyCode.H))  // no handcuff-to-talk transition, so stay in handcuff pose until unHandcuffed
        {
            anim.SetTrigger("isHandcuffed");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("isUnHandcuffed");
        }
    }
}