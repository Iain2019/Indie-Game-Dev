using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongAdTimerScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_XButton;
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
            GetComponent<UnityEngine.UI.Text>().text = ((int)time).ToString();
        }
        else
        {
            m_XButton.SetActive(true);
            this.enabled = false;
        }

    }
}
