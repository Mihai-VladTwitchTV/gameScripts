using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float startx = -82, starty = -6;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>=86)
       transform.position = new Vector2(startx, starty);

    }
}
