using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreItemManagerScript : MonoBehaviour
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
    GameObject m_equipButton;
    [SerializeField]
    int m_shieldNum;
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
                    MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.COINS):
                if (MBS.m_systemData.m_coins >= m_cost)
                {
                    MBS.m_systemData.m_coins -= m_cost;
                    MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.AD):
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked = true;
                break;
            default:
                break;
        }
        MBS.SaveSysytemData();
        SetValues();
    }
    public void Equip()
    {
        for (int i = 0; i < MBS.m_systemData.m_shieldData.Length; i++)
        {
            MBS.m_systemData.m_shieldData[i].m_equipped = false;
        }
        MBS.m_systemData.m_shieldData[m_shieldNum].m_equipped = true;
        MBS.SaveSysytemData();
        SetValues();
    }

    void SetValues()
    {
        if (MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked)
        {
            m_buyButton.SetActive(false);
            m_equipButton.SetActive(true);
            if (MBS.m_systemData.m_shieldData[m_shieldNum].m_equipped)
            {
                m_equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equiped";
                m_equipButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                m_equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equip";
                m_equipButton.GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            m_buyButton.SetActive(true);
            m_equipButton.SetActive(false);
            switch (m_buyMethod)
            {
                case (BUYMETHOD.STARS):
                    m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy (" + m_cost.ToString() + " Stars)";
                    break;
                case (BUYMETHOD.COINS):
                    m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy (" + m_cost.ToString() + " Coins)";
                    break;
                case (BUYMETHOD.AD):
                    m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy (" + "AD)";
                    break;
                default:
                    break;
            }
        }
    }
}
