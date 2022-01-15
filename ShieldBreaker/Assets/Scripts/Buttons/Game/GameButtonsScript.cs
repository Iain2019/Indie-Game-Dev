using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonsScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_gameManager;
    [SerializeField]
    GameObject m_gamePanel;
    [SerializeField]
    GameObject[] m_stars;
    [SerializeField]
    GameObject m_newHighScore;
    [SerializeField]
    int m_3Star;
    [SerializeField]
    int m_2Star;
    [SerializeField]
    int m_1Star;

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            m_gameManager.GetComponent<AudioSource>().Play();
            m_gamePanel.SetActive(false);
        }
    }
    public void UpdateUI()
    {
        if (m_stars.Length != 0)
        {
            GameManagerScript GMS = m_gameManager.GetComponent<GameManagerScript>();

            int stars = 0;
            if (GMS.m_score > m_3Star)
            {
                stars = 3;
            }
            else if (GMS.m_score > m_2Star)
            {
                stars = 2;
            }
            else if (GMS.m_score > m_1Star)
            {
                stars = 1;
            }
            else
            {
                stars = 0;
            }

            for (int i = 0; i < stars; i++)
            {
                m_stars[i].GetComponent<Image>().color = Color.yellow;
            }

            if (GMS.m_systemData.m_levelsData[GMS.m_levelNum].m_stars < stars)
            {
                GMS.m_systemData.m_stars += stars - GMS.m_systemData.m_levelsData[GMS.m_levelNum].m_stars;
                GMS.m_systemData.m_levelsData[GMS.m_levelNum].m_stars = stars;
                m_newHighScore.SetActive(true);
            }
            else
            {
                m_newHighScore.SetActive(false);
            }

            GMS.SaveSysytemData();
        }
    }

    public void Continue()
    {
        if (!(m_gameManager.GetComponent<GameManagerScript>().m_hasContinued))
        {
            m_gameManager.GetComponent<GameManagerScript>().m_hasContinued = true;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(2); //Chnage to load menu when its added
    }
}
