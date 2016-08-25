using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//JG to work on
public class GameManager : MonoBehaviour
{
    enum Gamestate
    {
        PAUSED,
        RUNNING,
        OCULUSMENU,
        INTERACT
   };
    //Gamestate currentState;
    private static GameManager manager = null;
    private bool kPaused = false;
    private string activeLevel;
    public static GameManager Manager
    {
        get { return manager; }
    }
    void Awake()
    {
        GetThisGameManager();
    }
    void GetThisGameManager()
    {
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            manager = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Debug.Log("Creating a new game state");
        SceneManager.LoadScene(0);
    }
        //gamestartup
        //fileload

    void MissionSelect()
    {

        //Main missions
        //puzzle select
        //filler missions (accessible anytim)
    }

    void SceneManagement()
    {
        //setup scene and load scenes
    }

    //void GameState()
    //{
    //    //enum - in mission, HQ, mission complete
    //}

    void SaveGame()
    {

    }

    void FixedUpdate()
    {
        //if(currentState == Gamestate.INTERACT)
        //{

        //}
        //Gameflow and isRunning
    }

    void PlayIntro()
    {
        //play intro
    }

    public string getLevel()
    {
        return activeLevel;
    }

    public void setLevel(string newLevel)
    {
        activeLevel = newLevel;
    }

    public bool IsPaused()
    {
        return kPaused;
    }
}
