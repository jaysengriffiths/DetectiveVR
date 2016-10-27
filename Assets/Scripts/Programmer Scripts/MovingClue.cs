using UnityEngine;
using System.Collections;

public class MovingClue : MonoBehaviour
{
    public float speed;
    private Player player;
    private SoundLookAt soundLook;
    bool movingToTarget = false;

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
        if (soundLook.isActivated && !movingToTarget)
        {
            gameObject.transform.position = player.enkHoldingCloth.transform.position;
            gameObject.transform.rotation = player.enkHoldingCloth.transform.rotation;
            player.clueObject = this.gameObject;
            gameObject.GetComponent<SoundLookAt>().enabled = false;
            //gameObject.SetActive(false);
        }

        if(player.selectedCharacter == null)
        {
            movingToTarget = false;
            player.clueComparisonPlayed = false;
        }
    }

    public void MoveTowards(Character character)
    {
        movingToTarget = true;
        float step = speed * Time.deltaTime;
        Transform clueTransform = character.transform;
        if (character.MovingClueLocation != null)
            clueTransform = character.MovingClueLocation;
        transform.position = Vector3.MoveTowards(transform.position, clueTransform.position, step);
    }

}
