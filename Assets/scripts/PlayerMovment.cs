using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private CharacterController controler; 
    private PlayerDo pDo;
    private Animator camAni;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jump;
    [SerializeField]
    private float gravity;

    // [SerializeField]
    // private float shotDis;
    // [SerializeField]
    // private GameObject bulletPrefab;
    private LayerMask layerGround;

    private Vector3 velocity;
    private bool grounded;
    private bool isMoving;

    [SerializeField]
    private GameObject camera;
    private Vector3 camRo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controler = GetComponent<CharacterController>();
        pDo = GetComponent<PlayerDo>();
        camAni = pDo.GetCam();
        layerGround = LayerMask.GetMask("GroundEf");
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

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if player is moving
        isMoving = horizontal != 0 || vertical != 0;
        camAni.SetBool("walking", isMoving);

        Vector3 moveInput = new Vector3(horizontal, velocity.y, vertical);
        Vector3 move = transform.TransformDirection(moveInput);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitObj, 1.5f, layerGround))
        {
            float friction = 0f;
            PhysicsMaterial material = hitObj.collider.sharedMaterial;
            if (material != null)
            {
                friction = 1f - material.dynamicFriction;
            }
            // Vector3 horizontalVertical = new Vector3(horizontal, 0f, vertical); //y = 0f is used, so the friction calculation dossent effect gravity.
            // horizontalVertical = transform.TransformDirection(horizontalVertical);
            // horizontalVertical *= Mathf.Pow(1f - friction, Time.deltaTime);
            // move = horizontalVertical + Vector3.up * velocity.y; //gravety is added back here via. vector(0,1,0)*y. 
            move.x *= friction;
            move.z *= friction;
        }

        controler.Move(move * Time.deltaTime);

        //get rotation of camera and remove y axis. 
        camRo = camera.transform.forward;
        camRo.y = 0;
        transform.forward = camRo;
    }
}
