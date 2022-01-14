using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonsScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_gameManager;
    [SerializeField]
    GameObject m_gameOverPanel;

    public void Continue()
    {
        if (!(m_gameManager.GetComponent<GameManagerScript>().m_hasContinued))
        {
            m_gameManager.GetComponent<GameManagerScript>().m_hasContinued = true;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            m_gameOverPanel.SetActive(false);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(2); //Chnage to load menu when its added
    }
}
