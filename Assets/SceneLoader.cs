using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
