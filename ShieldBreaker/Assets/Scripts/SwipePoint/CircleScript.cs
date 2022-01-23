using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    //serializable variables 
    [SerializeField]
    bool m_isInner;
    public float m_radius;
    //private variables 
    float m_thetaScale = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        //create circles for cutting area
        for (int i = 0; i < 2; i++)
        {
            //get linerender to draw circles with
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            float theta = 0.0f;
            //get circle points
            int size = (int)((1.0f / m_thetaScale) + 1.0f);
            lineRenderer.positionCount = size;
            for (int j = 0; j < size; j++)
            {
                //set points of line renderer 
                theta += (2.0f * Mathf.PI * m_thetaScale);
                float x = transform.position.x + m_radius * Mathf.Cos(theta);
                float y = transform.position.y + m_radius * Mathf.Sin(theta);
                lineRenderer.SetPosition(j, new Vector3(x, y, 0));
            }
        }
    }
}
