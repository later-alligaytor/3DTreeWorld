using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private CharacterController controler; 

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jump;
    [SerializeField]
    private float gravity;

    private Vector3 velocity;
    private bool grounded;

    [SerializeField]
    private GameObject camera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controler = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;

        grounded = controler.isGrounded; 
        if (grounded) {
            velocity.y = 0f;
            //print("grounded");
        }

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), velocity.y ,Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(moveInput);

        controler.Move(move * Time.deltaTime);

        //get rotation of camera and remove z axis. 
        var camRo = camera.transform.forward;
        camRo.y = 0;  

        transform.forward = camRo;
        //Debug.Log("grounded" + grounded);
    }
}
