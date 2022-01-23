using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuTimer : MonoBehaviour
{
    //serializeable variables
    [SerializeField]
    GameObject m_text;
    [SerializeField]
    float m_time;
    //private variables
    float m_startingTime;

    private void Start()
    {
        //time since startups - due to time.timescale being 0, other method required for testing time is required
        m_startingTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //get current time since script started
        float time = m_time - (Time.realtimeSinceStartup - m_startingTime);
        //timer for menu button
        if (time > 0)
        {
            //show time
            m_text.GetComponent<Text>().text = "Menu(" + ((int)time).ToString() + ")";
        }
        else
        {
            //enable the button
            m_text.GetComponent<Text>().text = "Menu";
            GetComponent<Button>().enabled = true;
            this.enabled = false;
        }

    }
}
