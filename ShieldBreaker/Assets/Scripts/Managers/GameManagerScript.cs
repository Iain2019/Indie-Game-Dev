using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //public variables
    public SystemData m_systemData;
    public int m_levelNum;
    public bool m_hasContinued = false;
    public int m_score = 0;
    public AudioSource m_music;
    public AudioSource m_playersMusic;
    //serilizable variables
    [SerializeField]
    GameObject[] m_spawners;
    [SerializeField]
    GameObject[] m_shieldPrefabs;
    [SerializeField]
    GameObject m_bladePrefab;
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
    [SerializeField]
    AudioSource m_scratch;
    //private variables
    GameObject m_shieldPrefab;
    int m_multiplier = 1;
    float[] m_samples = new float[1024];
    float m_samplesValue;
    float m_previousSamplesValue;
    float m_timer;
    bool m_beat;
    float m_totalTime = 0;
    bool m_startPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        //load save data
        //save data always exists when level opens, no risk of missing data due to check in menu
        m_systemData = SaveSytem.LoadSystemData();

        //mute sound if needed
        if (!(m_systemData.m_soundOn))
        {
            m_scratch.mute = true;
        }
        //set time scale to real time
        Time.timeScale = 1;
        for (int i = 0; i < m_systemData.m_shieldData.Length; i++)
        {
            //equip current shield
            if (m_systemData.m_shieldData[i].m_equipped)
            {
                m_shieldPrefab = m_shieldPrefabs[i];
            }
        }
        //check if music is muted
        if (m_systemData.m_musicOn)
        {
            //m_music.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", 0.0f);
            m_playersMusic.mute = false;
        }
        else
        {
            //m_music.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", -80.0f);
            m_playersMusic.mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check for mouse down (or finger down)
        if (Input.GetMouseButtonDown(0))
        {
            //get point in game space
            Instantiate(m_bladePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
        //count time
        m_totalTime += Time.deltaTime;
        //play music after delay so in line with swiping
        if (m_totalTime > 8 / (m_shieldPrefab.GetComponent<ShieldMoveScript>().m_moveSpeed) && m_startPlay)
        {
            m_playersMusic.Play();
            m_startPlay = false;
        }
        if (m_totalTime > m_music.clip.length + 3.0f)
        {

            //3 secs after music stop level ends
            Time.timeScale = 0;
            m_gameDone.GetComponent<GameButtonsScript>().UpdateUI();
            m_gameDone.SetActive(true);
        }
        //set previous mjusic sample data
        m_previousSamplesValue = m_samplesValue;
        //get spectrum data from audio
        m_music.GetSpectrumData(m_samples, 0, m_fftWindow);
        //if sample data
        if (m_samples != null && m_samples.Length > 0)
        {
            //deserialise samples data
            m_samplesValue = m_samples[0] * 100; //denormalize
        }
        //check for upper bias 
        if ((m_previousSamplesValue > m_spawnBiasUpper && m_samplesValue <= m_spawnBiasUpper) 
            || (m_previousSamplesValue <= m_spawnBiasUpper && m_samplesValue > m_spawnBiasUpper))
        {
            if (m_timer > m_beatTime)
            {
                OnBeat(0);
            }
        }
        //ceck for lower bias
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
        //spawn shield
        GameObject spawnedShield = Instantiate(m_shieldPrefab, m_spawners[a_spawnerIndex].transform.position, m_spawners[a_spawnerIndex].transform.rotation);
        spawnedShield.name = "SpawnedShield" + a_spawnerIndex.ToString();
        //set variables on create
        spawnedShield.GetComponent<ShieldScript>().m_gameManager = gameObject;
        spawnedShield.GetComponent<ShieldScript>().m_slicePoint = m_slicePoints[a_spawnerIndex];
        //destory after 5 for if missed
        Destroy(spawnedShield, 5.0f);
        m_timer = 0;
    }

    public void Break(bool a_inInner)
    {
        //break if swiped
        m_score += 10 * m_multiplier;
        //up score
        m_scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + m_score.ToString();
        if (m_multiplier < 8 && a_inInner)
        {
            //up multiplier if in crit region
            m_multiplier++;
        }
        m_multiplierText.GetComponent<UnityEngine.UI.Text>().text = "x" + m_multiplier.ToString();
    }
    public void Miss()
    {
        //if lose state
        if (m_multiplier == 1)
        {
            Time.timeScale = 0;
            m_music.Pause();
            m_playersMusic.Pause();
            m_scratch.Play();
            m_gameOver.GetComponent<GameButtonsScript>().UpdateUI();
            m_gameOver.SetActive(true);
        }
        else
        {
            //set multiplier to 1
            m_multiplier = 1;
            m_multiplierText.GetComponent<UnityEngine.UI.Text>().text = "x" + m_multiplier.ToString();
        }
    }

    public void SaveSysytemData()
    {
        //save data
        SaveSytem.SaveSystemData(m_systemData);
    }
}
