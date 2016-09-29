using UnityEngine;
using System.Collections;

public class Clue : MonoBehaviour
{
    public bool isActivated;
    public AudioClip IdleSound;
    public AudioClip AwakeSound;
    private int speed = 50;
    private GameObject player;
    public int activateDistance = 4;
    public float timer = 0.0f;
    // Use this for initialization
    void Start ()
    {

	}

	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < activateDistance)
        {
            //transform.Rotate(Vector3.down * Time.deltaTime * speed);
            timer++;
            if (timer > 600)
            {
                Debug.Log("Awake Sound");
                timer = 0;
            }
            
        }
        if(timer > 500 && !isActivated)
        {
            //Debug.Log("Idle Sound");
            timer = 0;
        }
        timer++;

        //test 


        
	}

}
