using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    //public variables
    public GameObject m_gameManager;
    public GameObject m_slicePoint;
    //serializable variables
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
    //private variables 
    CircleCollider2D m_circleCollider2D;
    GameObject m_slicePointOuter;
    GameObject m_slicePointInner;

    // Start is called before the first frame update
    void Start()
    {
        //check for sound
        if (!(SaveSytem.LoadSystemData().m_soundOn))
        {
            //mute sounds
            m_break.mute = true;
            m_breakCrit.mute = true;
            m_thump.mute = true;
        }
        //get components
        m_circleCollider2D = GetComponent<CircleCollider2D>();
        m_slicePointOuter = m_slicePoint.transform.GetChild(0).gameObject;
        m_slicePointInner = m_slicePoint.transform.GetChild(1).gameObject;

        //get rnd angle for shield create
        float rotation = Random.Range(0.0f, 360.0f);
        Vector3 angle = new Vector3(transform.rotation.x, transform.rotation.y, rotation);
        transform.eulerAngles = angle;
    }

    private void Update()
    {
        //if in circles created cutting region 
        if (Vector2.Distance(m_slicePointOuter.transform.position, transform.position) < m_slicePointOuter.GetComponent<CircleScript>().m_radius)
        {
            //enable collider for cutting
            m_circleCollider2D.enabled = true;
        }
        else
        {
            m_circleCollider2D.enabled = false;
        }
        //for checking if past bottom of outer cutting circle
        if (m_slicePointOuter.transform.position.y > transform.position.y)
        {
            if (Vector2.Distance(m_slicePointOuter.transform.position, transform.position) > m_slicePointOuter.GetComponent<CircleScript>().m_radius)
            {
                //tell gamemanger player missed
                m_gameManager.GetComponent<GameManagerScript>().Miss();
                this.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for hit by sword
        if (collision.tag == "Sword")
        {
            //get slice dir
            Vector2 sliceDir = collision.GetComponent<BladeScript>().m_sliceDir;
            Vector3 v3SliceDir = new Vector3(sliceDir.x, sliceDir.y, 0.0f);
            float angle = Vector3.Angle(v3SliceDir, -transform.up);
            //check if in angle allowed when cutting
            if (angle <= m_sliceAngle)
            {
                //check to see if in cutting radius
                if (Vector2.Distance(m_slicePointOuter.transform.position, transform.position) < m_slicePointOuter.GetComponent<CircleScript>().m_radius)
                {
                    //create cutting object
                    Quaternion slicedSpawnRotation = Quaternion.LookRotation(transform.up);
                    GameObject splitShield = Instantiate(m_splitShieldPrefab, transform.position, slicedSpawnRotation);
                    GameObject audioHolder = new GameObject();
                    //if in inner (crit) circle
                    if (Vector2.Distance(m_slicePointInner.transform.position, transform.position) < m_slicePointInner.GetComponent<CircleScript>().m_radius)
                    {
                        //set cutting audio info
                        audioHolder.AddComponent<AudioSource>().clip = m_breakCrit.clip;
                        audioHolder.GetComponent<AudioSource>().playOnAwake = true;
                        audioHolder.GetComponent<AudioSource>().mute = m_breakCrit.mute;
                        //critical break
                        m_gameManager.GetComponent<GameManagerScript>().Break(true);
                    }
                    //if in outer (normal) circle
                    else
                    {
                        //set cutting audio info
                        audioHolder.AddComponent<AudioSource>().clip = m_break.clip;
                        audioHolder.GetComponent<AudioSource>().playOnAwake = true;
                        audioHolder.GetComponent<AudioSource>().mute = m_break.mute;
                        //normal break
                        m_gameManager.GetComponent<GameManagerScript>().Break(false);
                    }
                    //spawn audio holder
                    audioHolder.name = "AudioHolder";
                    //add audio destroyer because unity can only do one destroy with time per script
                    audioHolder.AddComponent<AudioDestroyer>();
                    Instantiate(audioHolder);
                    //destroy timer
                    Destroy(splitShield, 2.0f);
                    //destroy this
                    Destroy(gameObject);
                }
            }
            else
            {
                //play wrong angle
                m_thump.Play();
                //missed
                m_gameManager.GetComponent<GameManagerScript>().Miss();
            }
        }
    }
}
