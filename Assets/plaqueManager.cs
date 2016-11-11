using UnityEngine;
using System.Collections;

public class  plaqueManager : MonoBehaviour {

    int ranking = 0;
    bool missionComplete = false;
    public Sprite[] plaqueSigns;
    public AudioClip[] plaqueSounds;

    public Sprite currentSign;
    public AudioClip currentSound;

    ArrayList missions;
    int missionsCompleted;

    // Use this for initialization
    void Start () {

        currentSign = plaqueSigns[ranking];
        currentSound = plaqueSounds[ranking];

        missionsCompleted = 0;

        GameObject root = GameObject.Find("Missions");
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
	    if(missionComplete)
        {
            ranking++;
            missionComplete = false;
        }
        currentSign = plaqueSigns[ranking];
        currentSound = plaqueSounds[ranking];
    }



}
