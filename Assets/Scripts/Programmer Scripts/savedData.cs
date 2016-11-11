using UnityEngine;
using System.Collections;

public class savedData : MonoBehaviour
{

    void Start()
    {
        // read data back in
        Mission[] missions = FindObjectsOfType<Mission>();
        Character[] characters = FindObjectsOfType<Character>();
        //get missons 
        foreach (Mission mi in MissionManager.missions)
        {
            mi.complete = PlayerPrefs.GetInt(mi.name + "Complete") != 0;
        }
        //get intros
        foreach (Character ch in characters)
        {
            ch.introPlayed = PlayerPrefs.GetInt(ch.name + "Intro") != 0;
        }

    }

    void Update()
    {

    }

    // this should get called in Main scene when the mission is complete
  public  void UpdateSave(int reward = 0)
    {
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character ch in characters)
        {
            PlayerPrefs.SetInt(ch.name + "Intro", ch.introPlayed ? 1 : 0); 
        }
        //PlayerPrefs.SetInt("rank",  );

        // save the fact that the current mission has been done
        MissionManager mm = FindObjectOfType<MissionManager>();
        PlayerPrefs.SetInt(mm.currentMission.name, reward); // could save out 1 = trophy 2 = present
        PlayerPrefs.Save();
    }

    public void ReSetSave()
    {
        PlayerPrefs.DeleteAll();
    }


}
