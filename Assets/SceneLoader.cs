using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	/*void Awake()
    {
        int numSceneLoaders = FindObjectsOfType<SceneLoader>().Length;
        if (numSceneLoaders > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }*/

    public void GoToScene(int sceneNumber)
    {
        print("Loading new scene");
        SceneManager.LoadScene(sceneNumber);
    }
}
