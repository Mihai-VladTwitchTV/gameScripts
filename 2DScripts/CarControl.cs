using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{


    public Rigidbody2D carMain;
    public Rigidbody2D frontTire;
    public Rigidbody2D backTire;
    private float movement;
    public float carSpin = 20;
    public float speed = 20;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        backTire.AddTorque(movement * -speed * Time.fixedDeltaTime);
        

        frontTire.AddTorque(movement * -speed * Time.fixedDeltaTime);
       
        carMain.AddTorque(movement * -speed * Time.fixedDeltaTime);
    }
}
