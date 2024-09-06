using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    public float maxSlidingTime;
    public float slideForce;
    private float slideTimer;

    [Header("Slide Crouching")]
    public float crouchYScale;
    private float startYScale;

    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;

    bool isSliding;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = playerObj.localScale.y;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //start slide
        if (Input.GetKeyDown(KeyCode.LeftControl) && (horizontalInput != 0 || verticalInput != 0) && pm.grounded)
        {
            StartSliding();
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl) && isSliding)
        {
            StopSliding();
        }
    }

    void FixedUpdate()
    {
        if (isSliding)
        {
            SlideMovement();
        }
    }

    void StartSliding()
    {
        isSliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, crouchYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);

        slideTimer = maxSlidingTime;
    }

    void SlideMovement()
    {
        Vector3 slideDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(slideDirection.normalized * slideForce * 10f, ForceMode.Force);

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
        {
            StopSliding();
        }
    }

    void StopSliding()
    {
        isSliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
