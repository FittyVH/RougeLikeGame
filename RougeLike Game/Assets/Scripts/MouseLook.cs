using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensi = 100f;

    public Transform orientation;

    float xRotation = 0f;
    float yRotation = 0f;
    //x is the axis in which we want to rotate in order to look up, this is also the angle

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseLookX = Input.GetAxis("Mouse X") * mouseSensi * Time.deltaTime;
        float mouseLookY = Input.GetAxis("Mouse Y") * mouseSensi * Time.deltaTime;

        yRotation += mouseLookX;
        xRotation -= mouseLookY;
        //we want to increment it with mouseLookY, += will invert the vertical controls
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //clamping it to 180 degrees

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //quaternion used for rotation, euler takes angles of x,y,z axis
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
