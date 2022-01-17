using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public GameObject m_gameManager;
    public GameObject m_slicePoint;

    [SerializeField]
    GameObject m_splitShieldPrefab;
    [SerializeField]
    float m_sliceAngle;
    [SerializeField]
    AudioSource m_break;
    [SerializeField]
    AudioSource m_breakCrit;
    [SerializeField]
    AudioSource m_thump;

    private CircleCollider2D m_circleCollider2D;
    private GameObject m_slicePointOuter;
    private GameObject m_slicePointInner;

    // Start is called before the first frame update
    void Start()
    {
        if (!(SaveSytem.LoadSystemData().m_soundOn))
        {
            m_break.mute = true;
            m_breakCrit.mute = true;
            m_thump.mute = true;
        }
        m_circleCollider2D = GetComponent<CircleCollider2D>();
        m_slicePointOuter = m_slicePoint.transform.GetChild(0).gameObject;
        m_slicePointInner = m_slicePoint.transform.GetChild(1).gameObject;

        float rotation = Random.Range(0.0f, 360.0f);
        Vector3 angle = new Vector3(transform.rotation.x, transform.rotation.y, rotation);
        transform.eulerAngles = angle;
    }

    private void Update()
    {
        if (Vector2.Distance(m_slicePointOuter.transform.position, transform.position) < m_slicePointOuter.GetComponent<CircleScript>().m_radius)
        {
            m_circleCollider2D.enabled = true;
        }
        else
        {
            m_circleCollider2D.enabled = false;
        }

        if (m_slicePointOuter.transform.position.y > transform.position.y)
        {
            if (Vector2.Distance(m_slicePointOuter.transform.position, transform.position) > m_slicePointOuter.GetComponent<CircleScript>().m_radius)
            {
                m_gameManager.GetComponent<GameManagerScript>().Miss();
                this.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            Vector2 sliceDir = collision.GetComponent<BladeScript>().m_sliceDir;
            Vector3 v3SliceDir = new Vector3(sliceDir.x, sliceDir.y, 0.0f);
            float angle = Vector3.Angle(v3SliceDir, -transform.up);

            if (angle <= m_sliceAngle)
            {
                if (Vector2.Distance(m_slicePointOuter.transform.position, transform.position) < m_slicePointOuter.GetComponent<CircleScript>().m_radius)
                {
                    Quaternion slicedSpawnRotation = Quaternion.LookRotation(transform.up);
                    GameObject splitShield = Instantiate(m_splitShieldPrefab, transform.position, slicedSpawnRotation);
                    GameObject audioHolder = new GameObject();

                    if (Vector2.Distance(m_slicePointInner.transform.position, transform.position) < m_slicePointInner.GetComponent<CircleScript>().m_radius)
                    {
                        audioHolder.AddComponent<AudioSource>().clip = m_breakCrit.clip;
                        audioHolder.GetComponent<AudioSource>().playOnAwake = true;
                        audioHolder.GetComponent<AudioSource>().mute = m_breakCrit.mute;

                        m_gameManager.GetComponent<GameManagerScript>().Break(true);
                    }
                    else
                    {
                        audioHolder.AddComponent<AudioSource>().clip = m_break.clip;
                        audioHolder.GetComponent<AudioSource>().playOnAwake = true;
                        audioHolder.GetComponent<AudioSource>().mute = m_break.mute;

                        m_gameManager.GetComponent<GameManagerScript>().Break(false);
                    }
                    audioHolder.name = "AudioHolder";
                    Instantiate(audioHolder);
                    Destroy(audioHolder, 2.0f);
                    Destroy(splitShield, 2.0f);
                    Destroy(gameObject);
                }
            }
            else
            {
                m_thump.Play();

                m_gameManager.GetComponent<GameManagerScript>().Miss();
            }
        }
    }
}
