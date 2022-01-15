using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public SystemData m_systemData;
    public int m_levelNum;
    public bool m_hasContinued = false;
    public int m_score = 0;

    [SerializeField]
    GameObject[] m_spawners;
    [SerializeField]
    GameObject[] m_shieldPrefabs;
    [SerializeField]
    GameObject[] m_slicePoints;
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
    [SerializeField]
    GameObject m_gameDone;
    [SerializeField]
    GameObject m_gameOver;
    [SerializeField]
    int m_levelNumber;

    GameObject m_shieldPrefab;
    int m_multiplier = 1;
    float[] m_samples = new float[1024];
    float m_samplesValue;
    float m_previousSamplesValue;
    float m_timer;
    bool m_beat;
    float m_totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        m_systemData = SaveSytem.LoadSystemData();
        for (int i = 0; i < m_systemData.m_shieldData.Length; i++)
        {
            if (m_systemData.m_shieldData[i].m_equipped)
            {
                m_shieldPrefab = m_shieldPrefabs[i];
            }
        }
        if (m_systemData.m_musicOn)
        {
            GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", 0.0f);
        }
        else
        {
            GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", -80.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_totalTime += Time.deltaTime;
        if (m_totalTime > GetComponent<AudioSource>().clip.length)
        {
            Time.timeScale = 0;
            m_gameDone.GetComponent<GameButtonsScript>().UpdateUI();
            m_gameDone.SetActive(true);
        }
        m_previousSamplesValue = m_samplesValue;
        GetComponent<AudioSource>().GetSpectrumData(m_samples, 0, m_fftWindow);

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
        spawnedShield.GetComponent<ShieldScript>().m_slicePoint = m_slicePoints[a_spawnerIndex];
        m_timer = 0;
    }

    public void Break(bool a_inInner)
    {
        m_score += 10 * m_multiplier;
        m_scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + m_score.ToString();
        if (m_multiplier < 8 && a_inInner)
        {
            m_multiplier++;
        }
        m_multiplierText.GetComponent<UnityEngine.UI.Text>().text = "x" + m_multiplier.ToString();
    }
    public void Miss()
    {
        //if (m_multiplier == 1)
        //{
        //    Time.timeScale = 0;
        //    GetComponent<AudioSource>().Pause();
        //    m_gameOver.GetComponent<GameButtonsScript>().UpdateUI();
        //    m_gameOver.SetActive(true);
        //}
        //else
        //{
        //    m_multiplier = 1;
        //    m_multiplierText.GetComponent<UnityEngine.UI.Text>().text = "x" + m_multiplier.ToString();
        //}
    }

    public void SaveSysytemData()
    {
        SaveSytem.SaveSystemData(m_systemData);
    }
}
