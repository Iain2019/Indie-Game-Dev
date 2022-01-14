using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuTimer : MonoBehaviour
{
    [SerializeField]
    GameObject m_text;
    [SerializeField]
    float m_time;

    float m_startingTime;

    private void Start()
    {
        m_startingTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        float time = m_time - (Time.realtimeSinceStartup - m_startingTime);

        if (time > 0)
        {
            m_text.GetComponent<Text>().text = "Menu(" + ((int)time).ToString() + ")";
        }
        else
        {
            m_text.GetComponent<Text>().text = "Menu";
            GetComponent<Button>().enabled = true;
            this.enabled = false;
        }

    }
}
