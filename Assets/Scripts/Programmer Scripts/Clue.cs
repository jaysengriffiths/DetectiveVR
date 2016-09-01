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
    public GameObject player;
    public int activateDistance = 4;
    public float timer = 0.0f;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cluePos = gameObject.GetComponent<Transform>();      
	}

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("awake");
        RaycastHit hit;
        

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10) && mClueType == ClueType.STATIC)
        {
            //cluePos.transform.position += new Vector3(0, 1, 0);
            cluePos.transform.Rotate(Vector3.down * Time.deltaTime * speed);
            //cluePos.transform.position = new Vector3(0, 2, 0);
            Debug.Log("awake");
        }
        if (Vector3.Distance(player.transform.position, this.transform.position) < activateDistance && mClueType == ClueType.STATIC)
        {
         
        }
        

        //test 


        
	}

}
