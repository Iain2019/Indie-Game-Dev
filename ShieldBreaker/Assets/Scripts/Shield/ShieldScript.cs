using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_splitShieldPrefab;
    [SerializeField]
    float m_sliceAngle;

    public GameObject m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        float rotation = Random.Range(0.0f, 360.0f);
        Vector3 angle = new Vector3(transform.rotation.x, transform.rotation.y, rotation);
        transform.eulerAngles = angle;
    }

    // Update is called once per frame
    void Update()
    {
        
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
                //Debug.Log("Sliced Noise");

                Quaternion slicedSpawnRotation = Quaternion.LookRotation(transform.up);

                GameObject splitShield = Instantiate(m_splitShieldPrefab, transform.position, slicedSpawnRotation);
                Destroy(splitShield, 2.0f);
                m_gameManager.GetComponent<GameManagerScript>().Break();
                Destroy(gameObject);
            }
            else
            {
                //Debug.Log("Thump Noise");
            }
        }
    }
}
