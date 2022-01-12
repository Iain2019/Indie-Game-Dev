using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScalingScript : MonoBehaviour
{
    public float m_minXYScale;
    public float m_maxXYScale;

    [SerializeField]
    float m_startScale;
    [SerializeField]
    float m_deltaScale;

    public GameObject m_gameManager;

    CircleCollider2D m_circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= m_startScale;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.0f);
        m_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale *= m_deltaScale;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (1.0f * Time.deltaTime));


        if (transform.localScale.x >= m_maxXYScale || transform.localScale.y >= m_maxXYScale)
        {
            m_gameManager.GetComponent<GameManagerScript>().Miss();

            //Debug.Log("Missed Noise");
            Destroy(gameObject);
        }
        else if (transform.localScale.x >= m_minXYScale || transform.localScale.y >= m_minXYScale)
        {
            m_circleCollider2D.enabled = true;
        }
    }
}
