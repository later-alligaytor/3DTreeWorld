using System.Collections;
using UnityEngine;

public class EvilTree : MonoBehaviour
{
    [Header("Branch Settings")]
    public GameObject segmentPrefab;
    public float branchLength;
    public int currentDepth = 0;
    public int maxDepth = 6;
    public float turnAnglePerGrowth = 20f; //turn amount toward player. 

    [Header("Leader Settings")]
    public bool Leader;
    public GameObject player;

    [Header("Branch Angles")]
    public float minAngle = 25f;
    public float maxAngle = 90f;
    // angle from parent branch
    public float rotationRange = 80f;

    private void Start()
    {
        CalculateBranchLength();
    }

    private void Update()
    {
        // Only the original trunk steers toward the player.
        //if (isLeader && player != null)
        //{
        //    SteerTowardPlayer();
       // }

        // Only the leader handles the manual growth input.
        if (Leader && Input.GetKeyDown(KeyCode.G) && currentDepth < maxDepth){
            Grow();
            Leader = false;
        }
    }
    //Quaternion SteerTowardPlayer() {
        //if (player == null)
        //    return;

       // Vector3 direction = player.position - transform.position;

       // if (direction.sqrMagnitude < 0.001f)
       //     return;

        //Quaternion targetRotation =
        //    Quaternion.FromToRotation(transform.up, direction.normalized) * transform.rotation;

        // Only turn a small amount per growth event.
        //transform LRot = Quaternion.RotateTowards(transform.rotation, targetRotation, turnAnglePerGrowth);
        //return LRot;
    //}

    void CalculateBranchLength()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Half the mesh's local height, accounting for scale.
            branchLength = meshFilter.sharedMesh.bounds.extents.y * transform.lossyScale.y;
        }
        else
        {
            Debug.LogWarning(
                "No MeshFilter found on " + gameObject.name);
        }
    }

    void Grow(){
        if (segmentPrefab == null)
            return;

        int branchCount = Random.Range(2, 5);

        // Find the actual top of this segment in world space.
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3 topOfSegment = transform.position + transform.up * branchLength;
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Bounds bounds = meshFilter.sharedMesh.bounds;
            Vector3 localTop = bounds.center + Vector3.up * bounds.extents.y;
            topOfSegment = transform.TransformPoint(localTop);
        }

        for (int i = 0; i < branchCount; i++)
        {
            bool isLeader = (i == 0);
            Quaternion branchRotation;

            if (isLeader && player != null){
                Vector3 direction = player.transform.position - transform.position;
                Quaternion targetRotation = transform.rotation;

                if (direction.sqrMagnitude > 0.001f){
                    targetRotation = Quaternion.FromToRotation(transform.up, direction.normalized) * transform.rotation;
                }
                branchRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnAnglePerGrowth);
            }
            else {
                float skew = Random.Range(minAngle, maxAngle);
                float rotationAroundTrunk = (360f / branchCount) * i + Random.Range(-rotationRange, rotationRange);
                branchRotation = transform.rotation * Quaternion.Euler(0, rotationAroundTrunk, 0) * Quaternion.Euler(skew, 0, 0);
            }

            GameObject newBranch = Instantiate(segmentPrefab, topOfSegment, branchRotation);
            if (!isLeader){
                newBranch.transform.localScale = newBranch.transform.localScale - (newBranch.transform.localScale / Random.Range(2, 6));
            }
            newBranch.transform.SetParent(transform, true);

            EvilTree childScript = newBranch.GetComponent<EvilTree>();
            if (childScript != null){
                childScript.currentDepth = currentDepth + 1;
                childScript.maxDepth = maxDepth;
                childScript.segmentPrefab = segmentPrefab;
                childScript.player = player;
                childScript.Leader = isLeader;
            }
        }
    }
}