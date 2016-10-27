using UnityEngine;
using System.Collections;

public class MovingClue : MonoBehaviour
{
    public float speed;
    private Player player;
    private SoundLookAt soundLook;
    void Awake()
    {
   
    }
    void Start ()
    {
        player = FindObjectOfType<Player>();
        soundLook = GetComponent<SoundLookAt>();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundLook.isActivated)
        {

            player.clueObject = this.gameObject;
            //gameObject.SetActive(false);
        }
    }

    public void MoveTowards(Character character)
    {
        float step = speed * Time.deltaTime;
        Transform clueTransform = character.transform;
        if (character.MovingClueLocation != null)
            clueTransform = character.MovingClueLocation;
        transform.position = Vector3.MoveTowards(transform.position, clueTransform.position, step);
    }

}
