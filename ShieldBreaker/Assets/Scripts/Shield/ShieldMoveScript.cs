using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMoveScript : MonoBehaviour
{
    public float m_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (m_moveSpeed * Time.deltaTime), transform.position.z);
    }
}
