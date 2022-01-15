using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreLevelsManagerScript : MonoBehaviour
{
    public enum BUYMETHOD
    {
        STARS,
        COINS,
        AD
    }
    [SerializeField]
    GameObject m_MenuButtonScript;
    [SerializeField]
    GameObject m_buyButton;
    [SerializeField]
    int m_leveldNum;
    [SerializeField]
    BUYMETHOD m_buyMethod;
    [SerializeField]
    int m_cost;

    MenuButtonsScript MBS;

    // Start is called before the first frame update
    void Start()
    {
        MBS = m_MenuButtonScript.GetComponent<MenuButtonsScript>();
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        SetValues();
    }

    public void Buy()
    {
        switch (m_buyMethod)
        {
            case (BUYMETHOD.STARS):
                if (MBS.m_systemData.m_stars >= m_cost)
                {
                    MBS.m_systemData.m_stars -= m_cost;
                    MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.COINS):
                if (MBS.m_systemData.m_coins >= m_cost)
                {
                    MBS.m_systemData.m_coins -= m_cost;
                    MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.AD):
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked = true;
                break;
            default:
                break;
        }
        MBS.SaveSysytemData();
        SetValues();
    }

    void SetValues()
    {
        if (MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked)
        {
            m_buyButton.GetComponent<Button>().enabled = false;
            m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Owned";
        }
        else
        {
            switch (m_buyMethod)
            {
                case (BUYMETHOD.STARS):
                    m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy Level " + (m_leveldNum + 1).ToString() + " (" + m_cost.ToString() + " Stars)";
                    break;
                case (BUYMETHOD.COINS):
                    m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy Level" + (m_leveldNum + 1).ToString() + " (" + m_cost.ToString() + " Coins)";
                    break;
                case (BUYMETHOD.AD):
                    m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy Level " + (m_leveldNum + 1).ToString() + " (AD)";
                    break;
                default:
                    break;
            }
        }
    }
}
