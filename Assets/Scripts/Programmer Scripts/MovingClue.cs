using UnityEngine;
using System.Collections;

public class MovingClue : MonoBehaviour
{
    //public GameObject correctClue;
    private Mission mission;
    public float speed;
    //public Character character;
    // Use this for initialization
    void Awake()
    {
   
    }
    void Start ()
    {
        gameObject.GetComponent<SoundLookAt>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position, step);


    }

}
