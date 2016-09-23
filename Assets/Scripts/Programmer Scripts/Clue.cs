using UnityEngine;
using System.Collections;

public class Clue : MonoBehaviour
{
    private Transform cluePos;
    public enum ClueType
    {
        STATIC,
        ATTACHED
    };
    public ClueType mClueType;

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
        cluePos = gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < activateDistance && mClueType == ClueType.STATIC)
        {
            isActivated = true;
            cluePos.transform.Rotate(Vector3.down * Time.deltaTime * speed);
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
