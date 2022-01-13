using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdButtonsScript : MonoBehaviour
{
    public void Close()
    {
        if (SceneManager.sceneCount > 1)
        {
            int lastSceneIndex = SceneManager.sceneCount - 1;
            Scene lastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(lastLoadedScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
}
