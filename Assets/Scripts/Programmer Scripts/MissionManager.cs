using UnityEngine;
using System.Collections;

//JG to work on

public class MissionManager : MonoBehaviour
{
    //private int guiltyIndex;

    public Mission mission;
    //Array of suspects 0 = complaiant
    void Start()
    {
        //guiltyIndex = Random.Range(0, 5);
        mission = gameObject.GetComponent<Mission>();
        
           
    }

    void FixedUpdate()
    {

    }

    void Arrest()
    {

     


    }

    void Warn()
    {

    }

    void UpdateSkybox()
    {
        //endOfMission
    }


    void AwardObject()
    {
        //if mission = num
        //if arrest correct
        //trophy.size * correctGuesses
        //if warn correct
        //gift.size * correctGuesses
    }  
}