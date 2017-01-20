using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SetPrefs : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetInt("Greetings", 0);
        //StartCoroutine(LoadNewScene());
        SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }


/*
    IEnumerator LoadNewScene()
    {
        // loadScene = false;  // Kathy

        AsyncOperation async = SceneManager.LoadSceneAsync("Main_Scene");

        while (!async.isDone)
        {
            yield return null;
        }

        loadScene = false;  // Kathy
        // yield return new WaitForSeconds(12);
    }
    */
}
