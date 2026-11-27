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

    public bool canMove;


    // Update is called once per frame
    void LateUpdate()
    {
        if (canMove){
            transform.position = target.transform.position + offset;

            mouseRotation.x += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseRotation.y += Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseRotation.y = Mathf.Clamp(mouseRotation.y, -70.0F, 70.0F); //limites vertical camera rotation
        
            transform.localRotation = Quaternion.Euler(-mouseRotation.y, mouseRotation.x, mouseRotation.z);
        }
    }
}
