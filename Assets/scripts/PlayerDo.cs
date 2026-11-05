using UnityEngine;

public class PlayerDo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        string collidedObjName = collision.gameObject.name;
        Debug.Log(collidedObjName);
    }
    public void LockMouse()
    {
        // Locks the cursor to the center of the screen and hides it automatically
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
