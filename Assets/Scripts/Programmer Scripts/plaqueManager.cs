using UnityEngine;
using System.Collections;

public class  plaqueManager : MonoBehaviour {

    int ranking = 0;        //The private field `plaqueManager.ranking' is assigned but its value is never used
    bool missionComplete = false;       //The private field `plaqueManager.missionComplete' is assigned but its value is never used
    public AudioClip myPlaqueSays;
    public Sprite[] plaqueSigns;
    public AudioClip[] plaqueSounds;

    public Sprite currentSign;
    public AudioClip currentSound;

    ArrayList missions = new ArrayList();
    int missionsCompleted;

    // Use this for initialization
    void Start () {

      //  currentSign = plaqueSigns[ranking];
      //  currentSound = plaqueSounds[ranking];

        missionsCompleted = 0;
        GameObject root = GameObject.Find("MissionList");
        SoundLookAt[] items = root.GetComponentsInChildren<SoundLookAt>();

       for (int i = 0; i < items.Length; i++)
       {
            if (items[i].missionName != "")
            {
               missions.Add(items[i].missionName);
               if (PlayerPrefs.GetInt(items[i].missionName) != 0)
                {
                    missionsCompleted++;
                    
                    // adjust trophies TODO

                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	   
        currentSign = plaqueSigns[missionsCompleted];
        currentSound = plaqueSounds[missionsCompleted];
    }

}
