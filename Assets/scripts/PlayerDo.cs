using UnityEngine;

public class PlayerDo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Collider = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        string collidedObjName = collision.gameObject.name;
        Debug.Log(collidedObjName);
    }
}
