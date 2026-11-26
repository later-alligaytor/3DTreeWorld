using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject treePrefab;
    public GrowTimer Timer;

    [SerializeField]
    float radius;
    [SerializeField]
    int density;

    private int layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        layerMask = ~LayerMask.GetMask("IgnoreRaycast");
        for (int i = 0; i <= density; i++){
            Vector2 circle = Random.insideUnitCircle * radius;
            Vector3 randomPosition = transform.position + new Vector3(circle.x,0,circle.y);

            RaycastHit hit;
            if (Physics.Raycast(randomPosition,Vector3.down, out hit, 80f, layerMask))
            {
                Vector3 finalSpawnPosition = hit.point - new Vector3(0, 0.05f, 0);
                GameObject newTree = Instantiate(treePrefab,finalSpawnPosition,Quaternion.identity);

                newTree.transform.SetParent(transform, true);

                Evil treeScript = newTree.GetComponent<Evil>();
                Timer.OnGrow.AddListener(() => treeScript.Grow2(newTree));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.K)){
        //     Vector2 circle = Random.insideUnitCircle * radius;
        //     Vector3 randomPosition = transform.position + new Vector3(circle.x,0,circle.y);

        //     RaycastHit hit;
        //     if (Physics.Raycast(randomPosition,Vector3.down, out hit, 80f, layerMask))
        //     {
        //         Vector3 finalSpawnPosition = hit.point - new Vector3(0, 0.05f, 0);
        //         Instantiate(treePrefab,finalSpawnPosition,Quaternion.identity);
        //     }
        // }
    }
    
    // private void OnDrawGizmosSelected() // Only draws when object is selected
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
