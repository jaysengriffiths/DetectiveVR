using UnityEngine;
using System.Collections;


public class Collision : MonoBehaviour
{
    //private GameObject player;
    // Use this for initialization
    void Start()
    {
        //player = gameObject.GetComponent<GameObject>();
    }


    void OnTriggerEnter (Collider other)
    {
        if  (other.tag == "NPC")
        {
            Debug.Log("NPC HIT ");

            //set state as talking
        }
        if (other.tag == "BUILDING")
        {
            Debug.Log("Hit Building ");
            //set state as talking
        }
        if (other.tag == "WALL")
        {
            Debug.Log("Hit walll");
            //set state as talking
        }
        if (other.tag == "CLUE")
        {
            Debug.Log("Hit clue ");
            //set state as talking
        }
    }

    void OnTriggerExit(Collider other)
    {
        //set state as moving 
        Debug.Log("Ending Collision");
    }
}





