using UnityEngine;

public class Lookat : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target.transform);
        //transform.Rotate( 90, 0, 0 );
        transform.RotateAround(transform.position, transform.right, 90);
    }
}
