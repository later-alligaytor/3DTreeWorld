using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
//fra Bastian git + edit
{
    [SerializeField]
    float rotationSpeed = 3f;

    //[SerializeField]
    //float rotationLimit = 45;

    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 offset;

    Vector3 mouseRotation = Vector3.zero;


    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 desiredPosition = target.transform.position + offset;
        //Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, Time.deltaTime);
        //transform.position = smoothedPosition;
        transform.position = target.transform.position + offset;

        mouseRotation.x += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseRotation.y += Input.GetAxis("Mouse Y") * rotationSpeed;

        //mouseRotation.x = Mathf.Clamp(mouseRotation.x, -rotationLimit, rotationLimit);
        //mouseRotation.y = Mathf.Clamp(mouseRotation.y, -rotationLimit, rotationLimit);

        //Debug.Log(mouseRotation);
        
        transform.localRotation = Quaternion.Euler(-mouseRotation.y, mouseRotation.x, mouseRotation.z);
    }
}
