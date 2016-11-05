using UnityEngine;
using System.Collections;

public class animatorController : MonoBehaviour
{
    Animator anim;


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

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("isStartingToListen");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("isListening");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("isStartingToTalk");
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("sitDown");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("torch");
        }
    }
}