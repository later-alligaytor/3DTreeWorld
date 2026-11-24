using System.Collections;
using UnityEngine;

public class Evil : MonoBehaviour
{
    [Header("Branch Settings")]
    public GameObject segmentPrefab;
    [SerializeField]
    private bool isEvil;
    public float branchLength;
    public int currentDepth = 0;
    public int maxDepth = 6;
    public float turnAnglePerGrowth = 20f; //turn amount toward player. 

    [Header("Leader Settings")]
    //public bool Leader;
    public GameObject player;
    [SerializeField]
    float evilRange;
    [SerializeField]
    private float variation;

    [Header("Branch Angles")]
    public float minAngle = 25f;
    public float maxAngle = 90f;
    // angle from parent branch
    public float rotationRange = 80f;

    GameObject leaderObject;
    MeshFilter meshFilter;
    LayerMask layerPlayer;

    private void Start()
    {
        //Calculates the starting branches Length
        branchLength = CalculateBranchLength(this.gameObject);
        layerPlayer = LayerMask.GetMask("Player");

        if (currentDepth == 0){
            startGrow(this.gameObject);
            startGrow(leaderObject);
            startGrow(leaderObject);
        }
        else if (0 < currentDepth && currentDepth < maxDepth){
            startGrow(leaderObject);
            startGrow(leaderObject);
            startGrow(leaderObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentDepth == 0){
            Grow2(this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.G) && 0 < currentDepth && currentDepth < maxDepth){
            Grow2(leaderObject);
        }
        // if (leaderObject != null){
        //     Vector3 playerDie = player.transform.position - leaderObject.transform.position;
        //     if (playerDie.magnitude> evilRange){
        //         Debug.DrawRay(leaderObject.transform.position, playerDie, Color.blue); 
        //     }
        //     if(playerDie.magnitude <= evilRange){
        //         Debug.DrawRay(leaderObject.transform.position, playerDie, Color.red); 
        //     }
        // }
    }

    float CalculateBranchLength(GameObject obj)
    {
        meshFilter = obj.GetComponent<MeshFilter>();
        float length;

        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Half the mesh's local height, accounting for scale.
            length = meshFilter.sharedMesh.bounds.extents.y * transform.lossyScale.y;
        }
        else
        {
            Debug.LogWarning("No MeshFilter found on " + gameObject.name);
            length = 1;
        }
        return length;
    }

    void Grow2(GameObject obj){
        if (segmentPrefab == null)
            return;

        int branchCount = Random.Range(2, 5);

        branchLength = CalculateBranchLength(obj);
        Vector3 topOfSegment = obj.transform.position + obj.transform.up * branchLength;
        meshFilter = obj.GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Bounds bounds = meshFilter.sharedMesh.bounds;
            Vector3 localTop = bounds.center + Vector3.up * bounds.extents.y;
            topOfSegment = obj.transform.TransformPoint(localTop);
            //gets a point to the top of the branch or something.
        } //Dont quite get this.

        for (int i = 0; i < branchCount; i++)
        {
            bool isLeader = (i == 0);
            Quaternion branchRotation;

            if (isLeader && player != null){
                Vector3 direction = player.transform.position - obj.transform.position;
                Quaternion targetRotation = obj.transform.rotation;

                if(isEvil && Physics.Raycast(obj.transform.position, direction, out RaycastHit hitObj, evilRange, layerPlayer)){
                    if (direction.sqrMagnitude > 0.001f){
                        // Vector3 randomOffset = Random.insideUnitSphere * 0.2f;
                        Vector3 variedDirection = (direction.normalized + Random.insideUnitSphere * variation).normalized; //Random variation in player direction.
                        // targetRotation = Quaternion.FromToRotation(obj.transform.up, direction.normalized) * obj.transform.rotation;
                        targetRotation = Quaternion.FromToRotation(obj.transform.up, variedDirection) * obj.transform.rotation;
                    }
                    branchRotation = Quaternion.RotateTowards(obj.transform.rotation, targetRotation, turnAnglePerGrowth);
                }
                else {
                    Vector3 randomDirection = (Random.insideUnitSphere * variation).normalized; //Random direction.
                    targetRotation = Quaternion.FromToRotation(obj.transform.up, randomDirection) * obj.transform.rotation;
                    branchRotation = Quaternion.RotateTowards(obj.transform.rotation, targetRotation, turnAnglePerGrowth);
                }

            }
            else {
                float skew = Random.Range(minAngle, maxAngle);
                float rotationAroundTrunk = (360f / branchCount) * i + Random.Range(-rotationRange, rotationRange);
                branchRotation = obj.transform.rotation * Quaternion.Euler(0, rotationAroundTrunk, 0) * Quaternion.Euler(skew, 0, 0);
            }

            GameObject newBranch = Instantiate(segmentPrefab, topOfSegment, branchRotation);
            newBranch.transform.localScale = newBranch.transform.localScale - (newBranch.transform.localScale / Random.Range(2, 6));

            newBranch.transform.SetParent(transform, true);

            if (isLeader)
            {
                leaderObject = newBranch;
            }
        }
        currentDepth += 1;
    }

    void startGrow(GameObject obj){
        if (segmentPrefab == null)
            return;

        int branchCount = Random.Range(2, 5);

        branchLength = CalculateBranchLength(obj);
        Vector3 topOfSegment = obj.transform.position + obj.transform.up * branchLength;
        meshFilter = obj.GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Bounds bounds = meshFilter.sharedMesh.bounds;
            Vector3 localTop = bounds.center + Vector3.up * bounds.extents.y;
            topOfSegment = obj.transform.TransformPoint(localTop);
            //gets a point to the top of the branch or something.
        } //Dont quite get this.

        for (int i = 0; i < branchCount; i++)
        {
            bool isLeader = (i == 0);
            Quaternion branchRotation;

            if (isLeader){
                Quaternion targetRotation = obj.transform.rotation;
                
                Vector3 randomDirection = (Random.insideUnitSphere * variation).normalized; //Random direction.
                targetRotation = Quaternion.FromToRotation(obj.transform.up, randomDirection) * obj.transform.rotation;
                branchRotation = Quaternion.RotateTowards(obj.transform.rotation, targetRotation, turnAnglePerGrowth);
            }
            else {
                float skew = Random.Range(minAngle, maxAngle);
                float rotationAroundTrunk = (360f / branchCount) * i + Random.Range(-rotationRange, rotationRange);
                branchRotation = obj.transform.rotation * Quaternion.Euler(0, rotationAroundTrunk, 0) * Quaternion.Euler(skew, 0, 0);
            }

            GameObject newBranch = Instantiate(segmentPrefab, topOfSegment, branchRotation);
            newBranch.transform.localScale = newBranch.transform.localScale - (newBranch.transform.localScale / Random.Range(2, 6));

            newBranch.transform.SetParent(transform, true);

            if (isLeader)
            {
                leaderObject = newBranch;
            }
        }
        currentDepth += 1;
    }
}