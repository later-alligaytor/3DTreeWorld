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
            takingDamage = true;
            StartCoroutine(DamageTimer());
        }
    }
    void OnTriggerExit(Collider collision) {
        if (collision.tag == "Evil") 
        {
            takingDamage = false;
        }
    }

    private IEnumerator DamageTimer()
	{
        while(takingDamage){
            HP -= 1;
            camAnimator.SetTrigger("shake");
		    yield return new WaitForSeconds(Delay);
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
