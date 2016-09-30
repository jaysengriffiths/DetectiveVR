using UnityEngine;
using System.Collections;

public class Kathy_witchControls : MonoBehaviour
{
    static Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isWaking");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("isStartingToTalk");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("isPuttingOutHands");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("isBringingBackHands");
        }
            
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("isListening");
        }
     }
}
