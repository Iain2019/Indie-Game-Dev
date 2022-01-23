using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    //public variables
    public Vector2 m_sliceDir;
    //serializable variables
    [SerializeField]
    GameObject m_trailPrefab;
    [SerializeField]
    float m_sliceVelocity;

    //private variables
    Camera m_camera;
    Rigidbody2D m_rigidbody2D;
    CircleCollider2D m_circleCollider2D;
    bool m_isCutting = false;
    GameObject m_currentTrail;
    Vector2 m_previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        //get gameobjects
        m_camera = Camera.main;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();
        //set previous balde pos
        m_previousPosition = transform.position;
        m_isCutting = true;
        //create trail
        m_currentTrail = Instantiate(m_trailPrefab, transform);
        //get start pos from screen
        m_previousPosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        m_circleCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        m_rigidbody2D.position = currentPosition;
        //on up end cutting
        if (Input.GetMouseButtonUp(0))
        {
            EndSlash();
        }
        //update whilst cutting
        if (m_isCutting)
        {
            UpdateSword(currentPosition);
        }
    }
    void UpdateSword(Vector2 a_currentVelocity)
    {
        //get dir of cut
        m_sliceDir = (a_currentVelocity - m_previousPosition).normalized;
        //get velocityt
        float velocity = ((a_currentVelocity - m_previousPosition).magnitude) / Time.deltaTime;
        //if swipe is quick enough turn on collider to allow for cut
        if (velocity > m_sliceVelocity)
        {
            m_circleCollider2D.enabled = true;
        }
        else
        {
            m_circleCollider2D.enabled = false;
        }

        m_previousPosition = a_currentVelocity;
    }
    void EndSlash()
    {
        //deparent the trail
        m_isCutting = false;
        m_currentTrail.transform.SetParent(null);
        //destroy the trail after 2 secs
        Destroy(m_currentTrail, 2.0f);
        m_circleCollider2D.enabled = false;
        //destroy this
        Destroy(this.gameObject);
    }
}
