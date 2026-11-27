using UnityEngine;

public class skyrotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float x, y, z;
    [SerializeField]
    private Vector3 disatnce;

    [SerializeField]
    private GameObject target;
    private Transform moon;
    private Transform sun;

    private Vector3 way;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // way.Set(0,1,1);
        way.Set(x,y,z);

        moon = transform.GetChild(0); // First child
        sun = transform.GetChild(1); // Second child

        Vector3 place = disatnce + target.transform.position;

        moon.position = -place;
        sun.position = place;
        moon.rotation = Quaternion.LookRotation(target.transform.position - moon.position);
        sun.rotation = Quaternion.LookRotation(target.transform.position - sun.position);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(target.transform.position, way, rotationSpeed * Time.deltaTime); //(rotates around, direction, speed)
        // Time.deltaTime is used so the angle rotation happens at same speed regardless of fps. Time.deltaTime = Seconds passed since previous frame

    }
}
