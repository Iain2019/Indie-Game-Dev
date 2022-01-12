using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{

    public Vector2 m_sliceDir;

    [SerializeField]
    GameObject m_trailPrefab;
    [SerializeField]
    float m_sliceVelocity;

    Camera m_camera;
    Rigidbody2D m_rigidbody2D;
    CircleCollider2D m_circleCollider2D;

    bool m_isCutting = false;
    GameObject m_currentTrail;
    Vector2 m_previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();

        m_previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        m_rigidbody2D.position = currentPosition;

        if (Input.GetMouseButtonDown(0))
        {
            StartSlash();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSlash();
        }

        if (m_isCutting)
        {
            UpdateSword(currentPosition);
        }
    }
    void UpdateSword(Vector2 a_currentVelocity)
    {
        m_sliceDir = (a_currentVelocity - m_previousPosition).normalized;

        float velocity = ((a_currentVelocity - m_previousPosition).magnitude) / Time.deltaTime;

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

    void StartSlash()
    {
        //Debug.Log("Swoosh Noise");

        m_isCutting = true;
        m_currentTrail = Instantiate(m_trailPrefab, transform);
        m_previousPosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        m_circleCollider2D.enabled = false;
    }
    void EndSlash()
    {
        m_isCutting = false;
        m_currentTrail.transform.SetParent(null);
        Destroy(m_currentTrail, 2.0f);
        m_circleCollider2D.enabled = false;
    }
}
