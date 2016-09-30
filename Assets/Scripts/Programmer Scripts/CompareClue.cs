using UnityEngine;
using System.Collections;

public class CompareClue : MonoBehaviour
{
    public GameObject correctClue;
    private Mission mission;
    bool move = true;
    // Use this for initialization
    void Awake()
    {
        mission = gameObject.GetComponent<Mission>();
        
    }
    void Start ()
    {
        correctClue = mission.clueSpawned;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ClueMove();
	}

    void ClueMove()
    {
        if (move == true)
        {
            Vector3.Lerp(correctClue.transform.position, mission.GetGuiltySuspect().character.transform.position, 5);
        }
        
    }
}
