using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongAdTimerScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_XButton;

    float m_time = 30.0f;
    float m_startingTime;

    private void Start()
    {
        //m_startingTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        float time = m_time - Time.timeSinceLevelLoad;

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
