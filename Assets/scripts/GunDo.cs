using UnityEngine;
using System.Collections;

public class GunDo : MonoBehaviour
{
    // public bool gunReady;
    Animator animator;

    [SerializeField]
    float CooldownDuration = 5.0f; //in seconds.
    [SerializeField]
    private float shotDis;
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    GameObject particles;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject camera;

    private bool canShoot;
    private bool guideLine;
    
    void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("gunReady", true);

        guideLine = false;
        // gunReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot)
        {
            Shoot();
        }

        if (guideLine)
        {
            Debug.DrawRay(player.transform.position + camera.transform.forward/2, camera.transform.forward/2*shotDis, Color.green); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!guideLine)
            {
                guideLine = true;
            }
            else
            {
                guideLine = false;
            }
        }
        
    }
	
    private void Shoot()
    {
        if (!animator.GetBool("gunReady"))
        {
            return;
        }
        animator.SetBool("gunReady", false);
        canShoot = false;
        StartCoroutine(StartCooldown());
    }

	public void Shoot2() // Is called from animation event on "Shoot" animation.
	{
        Debug.Log("Shoot");
        if(Physics.SphereCast(player.transform.position + camera.transform.forward/2, 0.05f, camera.transform.forward/2*shotDis, out RaycastHit HitObj, shotDis, Physics.DefaultRaycastLayers,
    QueryTriggerInteraction.Collide))
        {
            Debug.Log(HitObj.collider.gameObject.tag);
            if (HitObj.collider.gameObject.tag == "Evil")
            {
                //initialize "bullet" prefab (empty trigger collider with tag "bullet") on hitobj.point.
                GameObject newBullet = Instantiate(bulletPrefab, HitObj.point,bulletPrefab.transform.rotation);
            }
        }
	}


	public IEnumerator StartCooldown()
	{
		yield return new WaitForSeconds(CooldownDuration);
        particles.SetActive(false);
        animator.SetBool("gunReady", true);
	}
    // bool isHit = Physics.SphereCast(Vector3 {startPosition}, float radius, Vector3 {vectorDirection}, out RaycastHit {RaycastHitObj}, float {maxDistance}, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
    
    public void ActiveParticles()
	{
        particles.SetActive(true);
	}
    
    public void ActiveCanShoot()
	{
        canShoot = true;
	}
}
