using UnityEngine;
using System.Collections;

public class PlayerDo : MonoBehaviour
{
    [SerializeField]
    private int HPMax;
    public int HP;

    [SerializeField]
    private float Delay;
    private bool takingDamage;

    [SerializeField]
    private GameObject gunCamera;
    private Animator camAnimator;
    [SerializeField]
    private GameObject camera;
    private CameraControls camControl;

    [SerializeField]
    private GameObject uiManager;
    [SerializeField]
    private GameObject playerModel;
    private ChangeMat playerMat;

    private int damagingObjects;

    void Awake()
    {
        camAnimator = gunCamera.GetComponent<Animator>();
        camControl = camera.GetComponent<CameraControls>();
    }
    void Start()
    {
        HP = HPMax;
        takingDamage = false;
        playerMat = playerModel.GetComponent<ChangeMat>();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P)) {
        //     playerMat.ChangeColor(HP);
        // }
    }
 
    private void OnControllerColliderHit(ControllerColliderHit collision){ // <- because character controller instead of normal collider.
        // string collidedObjName = collision.gameObject.name;
        // Debug.Log(collidedObjName);
        if (collision.gameObject.CompareTag("Delete"))
        {
            HP = 0;
            uiManager.GetComponent<Menu>().GameOver();        
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Evil") 
        {
            Evil tree = collision.GetComponentInParent<Evil>();
            if (tree.isEvil){
                damagingObjects++;
                if (damagingObjects == 1)
                {
                    takingDamage = true;
                    StartCoroutine(DamageTimer());
                }
            }
        }
    }
    void OnTriggerExit(Collider collision) {
        if (collision.gameObject.tag == "Evil") 
        {
            Evil tree = collision.GetComponentInParent<Evil>();
            if (tree.isEvil){
                damagingObjects--;
                if (damagingObjects <= 0)
                {
                    damagingObjects = 0;
                    takingDamage = false;
                    // damageCoroutine = null;
                }
            }
        }
    }

    private IEnumerator DamageTimer()
	{
        while(takingDamage){
            HP -= 1;
            camAnimator.SetTrigger("shake");
		    yield return new WaitForSeconds(Delay);
            playerMat.ChangeColor(HP);

            if (HP < 1){
                uiManager.GetComponent<Menu>().GameOver();
            }
        }
        if (damagingObjects <= 0){
            StopCoroutine(DamageTimer());
        }
	}

    public void LockMouse(){
        // Locks the cursor to the center of the screen and hides it automatically
        Cursor.lockState = CursorLockMode.Locked;
        camControl.canMove = true;
    }
    public void UnlockMouse(){
        Cursor.lockState = CursorLockMode.Confined;
        camControl.canMove = false;
    }

    public Animator GetCam(){
        return camAnimator;
    }
}
