using UnityEngine;
using System.Collections;

public class savedData : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {
        // read data back in
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character ch in characters)
        {
            ch.introPlayed = PlayerPrefs.GetInt(ch.name + "Intro") != 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // this should get called in Main scene when the mission is complete
  public  void UpdateSave(int reward)
    {
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character ch in characters)
        {
            PlayerPrefs.SetInt(ch.name + "Intro", ch.introPlayed ? 1 : 0);
        }

        // save the fact that the current mission has been done
        MissionManager mm = FindObjectOfType<MissionManager>();
        PlayerPrefs.SetInt(mm.currentMission.name + "Done", reward); // could save out 1 = trophy 2 = present
    }
    public void UpdateSave()
    {
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character ch in characters)
        {
            PlayerPrefs.SetInt(ch.name + "Intro", ch.introPlayed ? 1 : 0);
        }

        // save the fact that the current mission has been done
       // MissionManager mm = FindObjectOfType<MissionManager>();
       // PlayerPrefs.SetInt(mm.currentMission.name + "Done", 1); // could save out 1 = trophy 2 = present
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }


}
