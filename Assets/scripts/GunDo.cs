using UnityEngine;
using System.Collections;

public class GunDo : MonoBehaviour
{
    // public bool gunReady;
    Animator animator;

    [SerializeField]
    float CooldownDuration = 5.0f; //in seconds.
    
    void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("gunReady", true);
        // gunReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Shoot();
        }
        
    }
	
    private void Shoot()
    {
        if (!animator.GetBool("gunReady"))
        {
            return;
        }
        animator.SetBool("gunReady", false);
        // gunReady = false;
        StartCoroutine(StartCooldown());
    }

	public IEnumerator StartCooldown()
	{
		yield return new WaitForSeconds(CooldownDuration);
        animator.SetBool("gunReady", true);
        // gunReady = true;
	}
}
