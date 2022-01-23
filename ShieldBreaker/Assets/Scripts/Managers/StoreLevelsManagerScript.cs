using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreLevelsManagerScript : MonoBehaviour
{
    //method of purchase
    public enum BUYMETHOD
    {
        STARS,
        COINS,
        AD
    }
    //serilizable variables
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
    //private variables 
    MenuButtonsScript MBS;

    // Start is called before the first frame update
    void Start()
    {
        //get the menu button script
        MBS = m_MenuButtonScript.GetComponent<MenuButtonsScript>();
        //set UI values
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        //ensure UI vlaues are set
        SetValues();
    }

    public void Buy()
    {
        //chnage on buy mathod
        switch (m_buyMethod)
        {
            case (BUYMETHOD.STARS):
                //if can afford
                if (MBS.m_systemData.m_stars >= m_cost)
                {
                    //subtract cost
                    MBS.m_systemData.m_stars -= m_cost;
                    //set unlocked
                    MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.COINS):
                //if can afford
                if (MBS.m_systemData.m_coins >= m_cost)
                {
                    //subtract cost
                    MBS.m_systemData.m_coins -= m_cost;
                    //set unlocked
                    MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.AD):
                //load ad scene
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                //set unlocked
                MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked = true;
                break;
            default:
                break;
        }
        //save chnages to save data
        MBS.SaveSysytemData();
        MBS.systemDataCheck();
        //update UI values to make sure its always updated
        SetValues();
    }

    void SetValues()
    {
        //in unlocked
        if (MBS.m_systemData.m_levelsData[m_leveldNum].m_unlocked)
        {
            //disable buy button and show owned
            m_buyButton.GetComponent<Button>().enabled = false;
            m_buyButton.transform.GetChild(0).GetComponent<Text>().text = "Owned";
        }
        else
        {
            switch (m_buyMethod)
            {
                //show costs for purchase 
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
