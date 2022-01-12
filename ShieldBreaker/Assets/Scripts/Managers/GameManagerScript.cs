using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_spawners;
    [SerializeField]
    GameObject m_shieldPrefab;
    //[SerializeField]
    //float m_maxTimer;
    [SerializeField]
    float m_beatTime;
    [SerializeField]
    float m_spawnBiasUpper;
    [SerializeField]
    float m_spawnBiasLower;
    [SerializeField]
    FFTWindow m_fftWindow;
    [SerializeField]
    GameObject m_scoreText;
    [SerializeField]
    GameObject m_multiplierText;

    int m_score = 0;
    int m_multiplier = 1;
    float[] m_samples = new float[1024];
    float m_samplesValue;
    float m_previousSamplesValue;
    
    //float m_timer;
    float m_timer;
    bool m_beat;
    float m_thetaScale;

    // Start is called before the first frame update
    void Start()
    {
        //Old Constant Spawing
        //m_timer = m_maxTimer;
        m_thetaScale = 0.01f;        

        foreach(GameObject spawner in m_spawners)
        {
            for(int i = 0; i < 2; i++)
            {
                Transform circleDrawer = spawner.transform.GetChild(i);

                LineRenderer lineRenderer = circleDrawer.GetComponent<LineRenderer>();
                float theta = 0.0f;
                int size = (int)((1.0f / m_thetaScale) + 1.0f);
                lineRenderer.positionCount = size;
                for (int j = 0; j < size; j++)
                {
                    float radius = 0.0f;
                    if (i == 0)
                    {
                        radius = m_shieldPrefab.GetComponent<ShieldScalingScript>().m_minXYScale - 0.75f;
                    }
                    else if (i == 1)
                    {
                        radius = m_shieldPrefab.GetComponent<ShieldScalingScript>().m_maxXYScale - 1.0f;
                    }
                    theta += (2.0f * Mathf.PI * m_thetaScale);
                    float x = circleDrawer.transform.position.x + radius * Mathf.Cos(theta);
                    float y = circleDrawer.transform.position.y + radius * Mathf.Sin(theta);
                    lineRenderer.SetPosition(j, new Vector3(x, y, 0));
                }
            }

            //Old Constant Spawing
            //Instantiate(m_shieldPrefab, spawner.transform.position, spawner.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Old Constant Spawing
        //if (m_timer > 0.0f)
        //{
        //    m_timer -= Time.deltaTime;
        //}
        //else
        //{
        //    m_timer = m_maxTimer;
        //    foreach (GameObject spawner in m_spawners)
        //    {
        //        Instantiate(m_shieldPrefab, spawner.transform.position, spawner.transform.rotation);
        //    }
        //}

        m_previousSamplesValue = m_samplesValue;
        AudioListener.GetSpectrumData(m_samples, 0, m_fftWindow);

        if (m_samples != null && m_samples.Length > 0)
        {
            m_samplesValue = m_samples[0] * 100; //denormalize
        }

        if ((m_previousSamplesValue > m_spawnBiasUpper && m_samplesValue <= m_spawnBiasUpper) 
            || (m_previousSamplesValue <= m_spawnBiasUpper && m_samplesValue > m_spawnBiasUpper))
        {
            if (m_timer > m_beatTime)
            {
                OnBeat(0);
            }
        }

        if ((m_previousSamplesValue > m_spawnBiasLower && m_samplesValue <= m_spawnBiasLower)
            || (m_previousSamplesValue <= m_spawnBiasLower && m_samplesValue > m_spawnBiasLower))
        {
            if (m_timer > m_beatTime)
            {
                OnBeat(1);
            }
        }

        m_timer += Time.deltaTime;
    }

    void OnBeat(int a_spawnerIndex)
    {
        //Debug.Log("Beat");
        GameObject spawnedShield = Instantiate(m_shieldPrefab, m_spawners[a_spawnerIndex].transform.position, m_spawners[a_spawnerIndex].transform.rotation);
        spawnedShield.name = "SpawnedShield" + a_spawnerIndex.ToString();
        spawnedShield.GetComponent<ShieldScript>().m_gameManager = gameObject;
        spawnedShield.GetComponent<ShieldScalingScript>().m_gameManager = gameObject;
        m_timer = 0;
    }

    public void Break()
    {
        m_score += 10 * m_multiplier;
        m_scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + m_score.ToString();
        if (m_multiplier < 8)
        {
            m_multiplier++;
        }
        m_multiplierText.GetComponent<UnityEngine.UI.Text>().text = "x" + m_multiplier.ToString();
    }
    public void Miss()
    {
        if (m_multiplier == 1)
        {
            Debug.Log("GameOver!");
        }
        else
        {
            m_multiplier = 1;
            m_multiplierText.GetComponent<UnityEngine.UI.Text>().text = "x" + m_multiplier.ToString();
        }
    }
}
