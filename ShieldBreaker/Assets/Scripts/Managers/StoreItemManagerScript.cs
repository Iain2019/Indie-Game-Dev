using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreItemManagerScript : MonoBehaviour
{
    //methond of purchase
    public enum BUYMETHOD
    {
        STARS,
        COINS,
        AD
    }
    //serializable variables
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
        //change on buy mathod
        switch (m_buyMethod)
        {
            case (BUYMETHOD.STARS):
                //if can afford
                if (MBS.m_systemData.m_stars >= m_cost)
                {
                    //subtract cost
                    MBS.m_systemData.m_stars -= m_cost;
                    //set unlocked
                    MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.COINS):
                //if can afford
                if (MBS.m_systemData.m_coins >= m_cost)
                {
                    //subtract cost
                    MBS.m_systemData.m_coins -= m_cost;
                    //set unlocked
                    MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked = true;
                }
                break;
            case (BUYMETHOD.AD):
                //load ad
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                //set unlocked
                MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked = true;
                break;
            default:
                break;
        }
        //save and set UI values
        MBS.SaveSysytemData();
        MBS.systemDataCheck();
        SetValues();
    }
    public void Equip()
    {
        //unequip all
        for (int i = 0; i < MBS.m_systemData.m_shieldData.Length; i++)
        {
            MBS.m_systemData.m_shieldData[i].m_equipped = false;
        }
        //equip this, save and set UI values
        MBS.m_systemData.m_shieldData[m_shieldNum].m_equipped = true;
        MBS.SaveSysytemData();
        SetValues();
    }

    void SetValues()
    {
        //check unlocked
        if (MBS.m_systemData.m_shieldData[m_shieldNum].m_unlocked)
        {
            //turn off buy button, activate equip button
            m_buyButton.SetActive(false);
            m_equipButton.SetActive(true);
            //if is equiped 
            if (MBS.m_systemData.m_shieldData[m_shieldNum].m_equipped)
            {
                //diable button and show equiped
                m_equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equiped";
                m_equipButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                //activate button and show equipable
                m_equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equip";
                m_equipButton.GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            //activate buy button
            m_buyButton.SetActive(true);
            m_equipButton.SetActive(false);
            switch (m_buyMethod)
            {
                //show buy costs
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
