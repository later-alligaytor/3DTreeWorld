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
    private GameObject camera;
    private Animator camAnimator;

    [SerializeField]
    private GameObject uiManager;

    private int damagingObjects;

    void Awake()
    {
        camAnimator = camera.GetComponent<Animator>();
    }
    void Start()
    {
        HP = HPMax;
        takingDamage = false;
    }

    void Update()
    {
    }
 
    // private void OnControllerColliderHit(ControllerColliderHit collision){ // <- because character controller instead of normal collider.
    //     string collidedObjName = collision.gameObject.name;
    //     Debug.Log(collidedObjName);
    // }

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
        if (collision.gameObject.tag == "Delete") 
        {
            HP = 0;
            uiManager.GetComponent<Menu>().GameOver();
        }
    }

    private IEnumerator DamageTimer()
	{
        while(takingDamage){
            HP -= 1;
            camAnimator.SetTrigger("shake");
		    yield return new WaitForSeconds(Delay);

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
    }
    public void UnlockMouse(){
        Cursor.lockState = CursorLockMode.Confined;
    }

    public Animator GetCam(){
        return camAnimator;
    }
}
