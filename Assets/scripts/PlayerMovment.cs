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
            float curSpeed = 0f;
            PhysicsMaterial material = hitObj.collider.sharedMaterial;
            if (material != null)
            {
                friction = material.dynamicFriction;
                curSpeed = (1f - friction) * 2f; // speeds up if friction is low, slows down if friction is high.
            }
            move.x *= curSpeed;
            move.z *= curSpeed;
        }

        controler.Move(move * Time.deltaTime);

        //get rotation of camera and remove y axis. 
        camRo = camera.transform.forward;
        camRo.y = 0;
        transform.forward = camRo;
    }
}
