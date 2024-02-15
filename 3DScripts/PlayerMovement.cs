using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    //run logic
    public float baseSpeed;
    float sprintSpeed;
    float moveSpeed;
    public float staminaCap;
    public float staminaQuant;
    public Transform orientation;
    float horiInput;
    float vertInput;
    public float friction;
    public float playerHeight;
    public LayerMask ground;
    bool isOnGround;
    public float runStamDecrease;
    public KeyCode sprintKey = KeyCode.LeftShift;
    //public float pushbackStrength;
    Vector3 moveDirection;

    Rigidbody body;

    //jump logic
    public float jumpStamina;
    public float stamRecovery;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool jumpAvailable = true;
    public KeyCode jumpKey = KeyCode.Space;

    void Start()
    {
        moveSpeed = baseSpeed;
        sprintSpeed = 2f * baseSpeed;
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        staminaQuant = staminaCap;
    }

    void Sprint()
    {
        if (staminaQuant >= 1f && Input.GetKey(sprintKey))
        {
            moveSpeed = sprintSpeed;
            staminaQuant = staminaQuant - runStamDecrease;
        }
        else if (staminaQuant < staminaCap)
        {
            staminaQuant = staminaQuant + stamRecovery;
            moveSpeed = baseSpeed;        
        }
    }

    private void inputRead()
    {
        horiInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && jumpAvailable && isOnGround)
        {
            jumpAvailable = false;
            Jump();
            Invoke(nameof(makeJumpAvailable), jumpCooldown);

        }
        Sprint();
    }

    // Update is called once per frame

    private void SpeedCap()
    {
        Vector3 flatVel = new Vector3(body.velocity.x, 10f, body.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 cappedSpeed = flatVel.normalized * moveSpeed;
            body.velocity = new Vector3(cappedSpeed.x, body.velocity.y, cappedSpeed.z);
        }
    }
    void Update()
    {
        //grounded check
        isOnGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        inputRead();
        SpeedCap();
        //handle friction
        if (isOnGround)
            body.drag = friction;
        else
            body.drag = 0;
    }
 
    

    private void FixedUpdate()
    {
        Move();
        ///OnCollisionStay();
    }

    private void Move()
    {
        //calculate movement dir
        moveDirection = orientation.forward * vertInput + orientation.right * horiInput;
        if (isOnGround)
            body.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            body.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void Jump()
    {
        //set initial y to 0
        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        staminaQuant = staminaQuant - jumpStamina;
    }
    private void makeJumpAvailable()
    {
        jumpAvailable = true;
    }
}
