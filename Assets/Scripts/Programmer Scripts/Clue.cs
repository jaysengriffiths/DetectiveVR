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
    public Transform player;
    public int activateDistance = 4;
    // Use this for initialization
    void Start ()
    {
        cluePos = gameObject.GetComponent<Transform>();      

	}

	// Update is called once per frame
	void Update ()
    {
        if(Vector3.Distance(player.transform.position, this.transform.position) < activateDistance)
        {
            isActivated = true;
        }
        if (Vector3.Distance(player.transform.position, this.transform.position) > activateDistance)
        {
            isActivated = false;
        }
        //play idle sound

        if (isActivated && mClueType == ClueType.STATIC)
        {
            //cluePos.transform.position += new Vector3(0, 1, 0);
            cluePos.transform.Rotate(Vector3.down * Time.deltaTime * speed);
            //cluePos.transform.position = new Vector3(0, 2, 0);
            //play awake sound
        }   


        
	}

}
