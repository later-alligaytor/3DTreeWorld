using UnityEngine;
using System.Collections;

public class Branch : MonoBehaviour
{
    [SerializeField]
    Transform Prefab;
    [SerializeField]
    float FallSpeed;
    Vector3 fall_Force;
    [SerializeField]
    float SlowSpeed;
    Vector3 slow_Force;

    Rigidbody rig;
    enum ModeSwitching { Start, Acceleration, Slow };
    ModeSwitching mode;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        mode = ModeSwitching.Start;

        fall_Force = new Vector3(0.0f, - FallSpeed, 0.0f);
        slow_Force = new Vector3(0.0f, - SlowSpeed, 0.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)){
            mode = ModeSwitching.Acceleration;
        }
    }

    void FixedUpdate()
    {
        //else {
        //    mode = ModeSwitching.Start;
        //}

        switch (mode)
        {
            //This is the starting mode which resets the GameObject
            case ModeSwitching.Start:
                //This resets the velocity of the Rigidbody
                // rig.linearVelocity = Vector3.zero;
                // rig.angularVelocity = Vector3.zero;
                rig.isKinematic = true;                
                break;

            //These are the modes ForceMode can force on a Rigidbody
            //This is Acceleration mode
            case ModeSwitching.Acceleration:
                //Use Acceleration as the force on the Rigidbody
                rig.isKinematic = false;
                rig.AddForce(fall_Force, ForceMode.Acceleration);
                break;

            case ModeSwitching.Slow:
                //Use Acceleration as the force on the Rigidbody
                rig.isKinematic = false;
                rig.linearVelocity = Vector3.zero;
                rig.AddForce(slow_Force, ForceMode.Acceleration);
                break;
        }
    }

    // void OnCollisionEnter(Collision collision) {
        //     if (collision.gameObject.tag == "Ground")
        // {
        //     //If the GameObject has the same tag as specified, output this message in the console
        //     mode = ModeSwitching.Start;            
        // }
        // if (collision.gameObject.tag == "Evil") {
	    //     Physics.IgnoreCollision(collision.collider, GetComponent<Collider>()); 
		// }
    void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Ground")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            mode = ModeSwitching.Slow;            
        }
        if (collision.tag == "Evil") {
	        Physics.IgnoreCollision(collision, GetComponent<Collider>()); //mb redundant if triggers
		}
        //Add collision with destroy zone
    }    

}
