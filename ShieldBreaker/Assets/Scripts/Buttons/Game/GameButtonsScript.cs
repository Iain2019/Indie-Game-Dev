using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonsScript : MonoBehaviour
{
    //serlizable variabless
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
    //private variables 
    GameManagerScript GMS;

    private void Start()
    {
        //get game manager
        GMS = m_gameManager.GetComponent<GameManagerScript>();
    }
    private void Update()
    {
        //if time scale is 1 being play again
        if (Time.timeScale == 1)
        {
            GMS.m_music.Play();
            GMS.m_playersMusic.Play();
            m_gamePanel.SetActive(false);
        }
    }
    public void UpdateUI()
    {
        //change UI based on level success
        GMS = m_gameManager.GetComponent<GameManagerScript>();
        if (m_stars.Length != 0)
        {
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
            //save data
            GMS.SaveSysytemData();
        }
    }

    public void Continue()
    {
        //allow continue for ad
        if (!(GMS.m_hasContinued))
        {
            GMS.m_hasContinued = true;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }

    public void Menu()
    {
        //load short ad
        SceneManager.LoadScene(2); //Chnage to load menu when its added
    }
}
