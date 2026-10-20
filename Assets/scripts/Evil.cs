using System.Collections;
using UnityEngine;

public class Evil : MonoBehaviour
{
    //Needs on trigger effect
    [Header("Branch Settings")]
    public GameObject segmentPrefab;
    public float branchLength;
    public int currentDepth = 0;
    public int maxDepth = 6;
    public float turnAnglePerGrowth = 20f; //turn amount toward player. 

    [Header("Leader Settings")]
    //public bool Leader;
    public GameObject player;

    [Header("Branch Angles")]
    public float minAngle = 25f;
    public float maxAngle = 90f;
    // angle from parent branch
    public float rotationRange = 80f;

    GameObject leaderObject;
    MeshFilter meshFilter;

    private void Start()
    {
        //Calculates the starting branches Length
        branchLength = CalculateBranchLength(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentDepth == 0){
            Grow2(this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.G) && 0 < currentDepth && currentDepth < maxDepth){
            Grow2(leaderObject);
        }
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
        //either GetComponent().GetChild(0).transform;
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

                if (direction.sqrMagnitude > 0.001f){
                    targetRotation = Quaternion.FromToRotation(obj.transform.up, direction.normalized) * obj.transform.rotation;
                }
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