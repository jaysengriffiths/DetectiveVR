using UnityEngine;
using System.Collections;

public class MovingClue : MonoBehaviour
{
    //public GameObject correctClue;
    private Mission mission;
    public float speed;
    //public Character character;
    // Use this for initialization
    public Player player;
    void Awake()
    {
   
    }
    void Start ()
    {
        player = FindObjectOfType<Player>();
        gameObject.GetComponent<SoundLookAt>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoveTowards()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, step);
    }

}
