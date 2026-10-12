using UnityEngine;

public class skyrotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private GameObject element;

    [SerializeField]
    private GameObject target;

    private Vector3 way;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        way.Set(0,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        //element.transform.rotate()
        //this.transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
        this.transform.RotateAround(target.transform.position, way, rotationSpeed * Time.deltaTime); //(rotates around, direction, speed)

    }
}
