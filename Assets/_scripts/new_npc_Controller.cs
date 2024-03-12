using System;
using UnityEngine;
using UnityEngine.AI;


public class new_npc_Controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform[] destinationPoints;
    public float minDistance;
    int currentDestinationIndex = 0;
   void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Roam();
    }
   void Roam()
    {
        if (agent.remainingDistance < minDistance)
        {
            
        currentDestinationIndex = (currentDestinationIndex + 1) % destinationPoints.Length;
        Vector3 newDestination = destinationPoints[currentDestinationIndex].position;
        agent.SetDestination(newDestination);
        }

      
        animator.SetFloat("vertical", agent.velocity.magnitude > 0.1f ? 1 : 0);
    }
    private int FindNearestDestination()
    {
        int nearestIndex = 0;
        float shortestDis = Mathf.Infinity;
        for(int i =0;i<destinationPoints.Length;i++)
        {
            if(i == currentDestinationIndex)
                continue;
            
            float distance = Vector3.Distance(transform.position,destinationPoints[i].position);
            if(distance < shortestDis)
            {
                shortestDis = distance;
                nearestIndex = i;
            }
        }
        return nearestIndex;
    }

    // Method to set the pool of destination points
    public void SetDestinationPoints(Transform[] points)
    {
        destinationPoints = points;
    }
    
}
