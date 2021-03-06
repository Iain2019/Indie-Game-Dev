using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    //public variables 
    public SystemData m_systemData;
    //serilisable variables
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
    GameObject m_storePurchase;
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
    [SerializeField]
    int m_shieldCount;

    private void Start()
    {
        //check game data
        systemDataCheck();
    }

    public void Levels()
    {
        //swich active panel
        m_main.SetActive(false);
        m_levels.SetActive(true);
    }
    public void LoadLevel1()
    {
        //load level if unlocked
        if (m_systemData.m_levelsData[0].m_unlocked)
        {
            SceneManager.LoadScene(3);
        }
    }
    public void LoadLevel2()
    {
        //load level if unlocked
        if (m_systemData.m_levelsData[1].m_unlocked)
        {
            SceneManager.LoadScene(4);
        }
    }
    public void LoadLevel3()
    {
        //load level if unlocked
        if (m_systemData.m_levelsData[2].m_unlocked)
        {
            SceneManager.LoadScene(5);
        }
    }
    public void LoadLevel4()
    {
        //load level if unlocked
        if (m_systemData.m_levelsData[3].m_unlocked)
        {
            SceneManager.LoadScene(6);
        }
    }
    public void LoadLevel5()
    {
        //load level if unlocked
        if (m_systemData.m_levelsData[4].m_unlocked)
        {
            SceneManager.LoadScene(7);
        }
    }
    public void LoadLevel6()
    {
        //load level if unlocked
        if (m_systemData.m_levelsData[5].m_unlocked)
        {
            SceneManager.LoadScene(8);
        }
    }
    public void BackFromLevels()
    {
        //swich active panel
        m_main.SetActive(true);
        m_levels.SetActive(false);
    }
    public void Store()
    {
        //swich active panel
        m_main.SetActive(false);
        m_store.SetActive(true);
    }
    public void StoreShields()
    {
        //swich active panel
        m_storeMain.SetActive(false);
        m_storeShields.SetActive(true);
    }
    public void BackFromStoreShields()
    {
        //swich active panel
        m_storeMain.SetActive(true);
        m_storeShields.SetActive(false);
    }
    public void StorePurchase()
    {
        //swich active panel
        m_storeMain.SetActive(false);
        m_storePurchase.SetActive(true);
    }
    public void BackFromStorePurchase()
    {
        //swich active panel
        m_storeMain.SetActive(true);
        m_storePurchase.SetActive(false);
    }
    public void StoreLevel()
    {
        //swich active panel
        m_storeMain.SetActive(false);
        m_storeLevels.SetActive(true);
    }
    public void BackFromStoreLevels()
    {
        //swich active panel
        m_storeMain.SetActive(true);
        m_storeLevels.SetActive(false);
    }
    public void BackFromStore()
    {
        //swich active panel
        m_main.SetActive(true);
        m_store.SetActive(false);
    }
    public void Options()
    {
        //swich active panel
        m_main.SetActive(false);
        m_options.SetActive(true);
    }
    public void ToggleSound()
    {
        //switch sound on off
        m_systemData.m_soundOn = !m_systemData.m_soundOn;
        //set button UI
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
        //switch music on off
        m_systemData.m_musicOn = !m_systemData.m_musicOn;
        SaveSytem.SaveSystemData(m_systemData);
        //set button UI
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
        //delete game data, and reset
        SaveSytem.DeleteSystemData();
        systemDataCheck();
    }
    public void DeleteSaveData()
    {
        //just delete
        SaveSytem.DeleteSystemData();
    }
    public void BackFromOptions()
    {
        //swich active panel
        m_main.SetActive(true);
        m_options.SetActive(false);
    }
    public void Quit()
    {
        //close
        Application.Quit();
    }

    public void systemDataCheck()
    {
        //load level data
        m_systemData = SaveSytem.LoadSystemData();
        if (m_systemData == null)
        {
            //if null assign base data
            m_systemData = new SystemData(null);
            m_systemData.m_stars = 0;
            m_systemData.m_coins = 0;
            m_systemData.m_soundOn = true;
            m_systemData.m_musicOn = true;
            m_systemData.m_levelsData = new LevelData[m_levelCount];
            m_systemData.m_shieldData = new ShieldData[m_shieldCount];

            for (int i = 0; i < m_systemData.m_levelsData.Length; i++)
            {
                LevelData levelData = new LevelData(null);
                if (i == 0)
                {
                    levelData.m_unlocked = true;
                }
                else
                {
                    levelData.m_unlocked = false;
                }
                levelData.m_stars = 0;

                m_systemData.m_levelsData[i] = levelData;
            }

            for (int i = 0; i < m_systemData.m_shieldData.Length; i++)
            {
                ShieldData shieldData = new ShieldData(null);
                if (i == 0)
                {
                    shieldData.m_unlocked = true;
                    shieldData.m_equipped = true;
                }
                else
                {
                    shieldData.m_unlocked = false;
                    shieldData.m_equipped = false;
                }

                m_systemData.m_shieldData[i] = shieldData;
            }
            //save
            SaveSytem.SaveSystemData(m_systemData);
        }
        //set UI
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
    public void SaveSysytemData()
    {
        //save data
        SaveSytem.SaveSystemData(m_systemData);
    }

}
