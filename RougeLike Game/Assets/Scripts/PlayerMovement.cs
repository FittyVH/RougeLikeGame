using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed;
    public float groundDrag;
    public float airDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Crounching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    public float slopeDownwardForce;
    private RaycastHit slopeHit;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    void Update()
    {
        MyInput();
        SpeedControl();
    }

    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(orientation.position, 0.2f, whatIsGround);
        if (grounded)
        {
            HorizontalDrag();
            readyToJump = true;
        }
        else
        {
            HorizontalDrag();
            readyToJump = false;
        }
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
            readyToJump = false;
        }

        //start crouching
        if (Input.GetKeyDown(KeyCode.LeftControl) && grounded)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    void MovePlayer()
    {
        //calc move direction of player
        movementDirection = orientation.transform.forward * verticalInput + orientation.transform.right * horizontalInput;

        //ground check
        if (grounded)
        {
            rb.AddForce(movementDirection * moveSpeed * 10f, ForceMode.Force);
            //ForceMode.Force determines the how force is applied, you dont need to understand that part
        }
        else if (!grounded)
        {
            rb.AddForce(movementDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //slope movement
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 10f, ForceMode.Force);
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * slopeDownwardForce * 10f, ForceMode.Force);
            }
        }

        //turn off gravity while on slope;
        rb.useGravity = !OnSlope();
    }

    void SpeedControl()
    {
        //limit speed on slope
        if (OnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        //speed limit on ground/air
        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitVelocity = flatVelocity.normalized * moveSpeed;
                                        //gets only direction * the speed we want
                rb.velocity = new Vector3(limitVelocity.x, 0f, limitVelocity.z);
                //apply this velocity to actual velocity
            }
        }
    }


    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    void HorizontalDrag()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 dragForce = -horizontalVelocity * groundDrag;

        rb.AddForce(dragForce, ForceMode.Force);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    }
}
