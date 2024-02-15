using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPosScript : MonoBehaviour

    
{
    public Transform CameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraPos.position;
    }
}
