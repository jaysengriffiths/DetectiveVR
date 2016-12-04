using UnityEngine;
using System.Collections;

public class savedData : MonoBehaviour
{

    void Start()
    {
        // read data back in

            // Mission[] missions = FindObjectsOfType<Mission>();      //commented out because the variable 'missions' is assigned but its value is never used
        
        // Mission string is saved to PlayerPrefs in Player script

        //get missons 
        foreach (Mission mi in MissionManager.missions)
        {
            mi.complete = PlayerPrefs.GetInt(mi.name + "Complete") != 0;
        }

        //get intros

        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character ch in characters)
        {
            ch.introPlayed = PlayerPrefs.GetInt(ch.name + "Intro") != 0;
        }

    }

    void Update()
    {

    }

    // this should get called in Main scene when the mission is complete           //it happens in DialogManager
  public  void UpdateSave(int reward = 0)
    {
        //the fact that the current mission is complete is saved in the MissionManager with  1 = trophy 2 = present
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }

    private void UpdateCharacterIntro (int played = 0)
    {
        //save the fact that this character has said their intro
        Character[] characters = FindObjectsOfType<Character>();
        foreach (Character ch in characters)
        {
            PlayerPrefs.SetInt(ch.name + "Intro", ch.introPlayed ? 1 : 0);  //in Character script, every character has introPlayed = false; in MissionManager, if character.introPlayed...
        }

    }
}
