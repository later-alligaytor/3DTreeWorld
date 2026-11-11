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

    // [SerializeField]
    // private float shotDis;
    // [SerializeField]
    // private GameObject bulletPrefab;

    private Vector3 velocity;
    private bool grounded;

    [SerializeField]
    private GameObject camera;

    private Vector3 camRo;


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

        //get rotation of camera and remove y axis. 
        camRo = camera.transform.forward;
        camRo.y = 0;
        transform.forward = camRo;
        //Debug.Log("grounded" + grounded);
//_________________________________________________
        // Debug.DrawRay(transform.position+camera.transform.forward, camera.transform.forward*shotDis, Color.green); 
        // if (Input.GetKeyDown(KeyCode.Alpha4))
        // {
        //     Shoot();
        // }
        //__________________________________________
    }

    // void LateUpdate()
    // {
    //     Debug.DrawRay(transform.position+Vector3.forward, camera.transform.forward*shotDis, Color.green); 
    //     if (Input.GetKeyDown(KeyCode.Alpha4))
    //     {
    //         Shoot();
    //         Debug.Log("Shoot");
    //     }
    // }

    // private void Shoot()
    // {
    //     Debug.Log("Shoot");
    //     if(Physics.SphereCast(transform.position + camera.transform.forward, 0.1f, camera.transform.forward, out RaycastHit HitObj, shotDis, Physics.DefaultRaycastLayers,
    // QueryTriggerInteraction.Collide))
    //     {
    //         Debug.Log(HitObj.collider.gameObject.tag);
    //         if (HitObj.collider.gameObject.tag == "Evil")
    //         {
    //             //initialize "bullet" prefab (empty trigger collider with tag "bullet") on hitobj.point.
    //             GameObject newBullet = Instantiate(bulletPrefab, HitObj.point,bulletPrefab.transform.rotation);
    //         }
    //         // if (HitObj.collider.gameObject.tag == "Tagged")                 
    //         // {                     
    //         //     Debug.DrawRay(transform.position, transform.forward, Color.green);                     
    //         //     print("Hit");
    //         // }             
    //     }
    // }
}
