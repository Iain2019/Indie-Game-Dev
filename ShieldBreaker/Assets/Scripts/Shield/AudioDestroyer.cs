using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //start a destroy time, needed becuase unity cannot hangle two on one script
        Destroy(this.gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
