using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_main;
    [SerializeField]
    GameObject m_levels;
    [SerializeField]
    GameObject m_store;
    [SerializeField]
    GameObject m_options;
    [SerializeField]
    GameObject m_storeMain;
    [SerializeField]
    GameObject m_storeShields;
    [SerializeField]
    GameObject m_storeLevels;
    [SerializeField]
    GameObject m_starsText;
    [SerializeField]
    GameObject m_coinsText;
    [SerializeField]
    GameObject m_soundButton;
    [SerializeField]
    GameObject m_musicButton;
    [SerializeField]
    int m_levelCount;

    SystemData m_systemData;

    private void Start()
    {
        systemDataCheck();
    }

    public void Levels()
    {
        m_main.SetActive(false);
        m_levels.SetActive(true);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(3);
    }
    public void BackFromLevels()
    {
        m_main.SetActive(true);
        m_levels.SetActive(false);
    }
    public void Store()
    {
        m_main.SetActive(false);
        m_store.SetActive(true);
    }
    public void StoreShields()
    {
        m_storeMain.SetActive(false);
        m_storeShields.SetActive(true);
    }
    public void BackFromStoreShields()
    {
        m_storeMain.SetActive(true);
        m_storeShields.SetActive(false);
    }
    public void StoreLevel()
    {
        m_storeMain.SetActive(false);
        m_storeLevels.SetActive(true);
    }
    public void BackFromStoreLevels()
    {
        m_storeMain.SetActive(true);
        m_storeLevels.SetActive(false);
    }
    public void BackFromStore()
    {
        m_main.SetActive(true);
        m_store.SetActive(false);
    }
    public void Options()
    {
        m_main.SetActive(false);
        m_options.SetActive(true);
    }
    public void ToggleSound()
    {
        m_systemData.m_soundOn = !m_systemData.m_soundOn;
        if (m_systemData.m_soundOn)
        {
            m_soundButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            m_soundButton.GetComponent<Image>().color = Color.red;
        }
        SaveSytem.SaveSystemData(m_systemData);
    }
    public void ToggleMusic()
    {
        m_systemData.m_musicOn = !m_systemData.m_musicOn;
        SaveSytem.SaveSystemData(m_systemData);
        if (m_systemData.m_musicOn)
        {
            m_musicButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            m_musicButton.GetComponent<Image>().color = Color.red;
        }
    }
    public void GiveStars()
    {
        m_systemData.m_stars += 1;
        SaveSytem.SaveSystemData(m_systemData);
        m_starsText.GetComponent<Text>().text = "Stars: " + m_systemData.m_stars.ToString();
    }
    public void GiveCoins()
    {
        m_systemData.m_coins += 10;
        SaveSytem.SaveSystemData(m_systemData);
        m_coinsText.GetComponent<Text>().text = "Coins: " + m_systemData.m_coins.ToString();
    }
    public void ResetSaveData()
    {
        SaveSytem.DeleteSystemData();
        systemDataCheck();
    }
    public void DeleteSaveData()
    {
        SaveSytem.DeleteSystemData();
    }
    public void BackFromOptions()
    {
        m_main.SetActive(true);
        m_options.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }

    void systemDataCheck()
    {
        m_systemData = SaveSytem.LoadSystemData();
        if (m_systemData == null)
        {
            m_systemData = new SystemData(null);
            m_systemData.m_stars = 0;
            m_systemData.m_coins = 0;
            m_systemData.m_soundOn = true;
            m_systemData.m_musicOn = true;
            m_systemData.m_levelsData = new LevelData[m_levelCount];
            for (int i = 0; i < m_systemData.m_levelsData.Length; i++)
            {
                if (i == 0)
                {
                    m_systemData.m_levelsData[i].m_unlocked = true;
                }
                else
                {
                    m_systemData.m_levelsData[i].m_unlocked = false;
                }
                m_systemData.m_levelsData[i].m_stars = 0;
            }

            SaveSytem.SaveSystemData(m_systemData);
        }
        m_starsText.GetComponent<Text>().text = "Stars: " + m_systemData.m_stars.ToString();
        m_coinsText.GetComponent<Text>().text = "Coins: " + m_systemData.m_coins.ToString();
        if (m_systemData.m_soundOn)
        {
            m_soundButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            m_soundButton.GetComponent<Image>().color = Color.red;
        }
        if (m_systemData.m_musicOn)
        {
            m_musicButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            m_musicButton.GetComponent<Image>().color = Color.red;
        }
    }
}
