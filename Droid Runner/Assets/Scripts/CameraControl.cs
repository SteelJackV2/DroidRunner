using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 0.0f;

    public Transform lookAt;
    public Transform camTransform;
    public Transform head;
    public float distance = 2.0f;
    public bool mouseOn;
    public FloatingJoystick lookAround;

    private float currentX = 100.0f;
    private float currentY = -45.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;

    //float moveVertical = Input.GetAxis("Vertical");

    int start = 1000;

    private void Start()
    {
        camTransform = transform;
    }

    private void Update()
    {
        start--;

        if (start > 0)
        {
            distance = start/100.0f +2.0f;
            currentX += .5f;
        }

        if (start == 0)
        {
            distance = 3.0f;
            currentX = 180.0f;
            currentY = -25.0f;
        }


        if (mouseOn && (start<=0))
        {
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
            //distance -= moveVertical / 2.0f;
        }
        currentX += (lookAround.Horizontal * 3);
        currentY += (lookAround.Vertical * 3);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }
}
