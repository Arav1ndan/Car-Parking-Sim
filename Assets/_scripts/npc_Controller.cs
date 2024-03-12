using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class npc_Controller : MonoBehaviour
{
     public NavMeshAgent agent;
    public Animator animator;
    public GameObject path;

    public Transform[] pathPoints;

    public int index = 0;

    public float minDistance;

    // New variables for starting and ending points
    public Transform startingPoint;
    public Transform endingPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        pathPoints = new Transform[path.transform.childCount];

        for (int i = 0; i < pathPoints.Length; i++)
        {
            pathPoints[i] = path.transform.GetChild(i);
        }
    }

    void Update()
    {
        Roam();
    }

    void Roam()
    {
        if (Vector3.Distance(transform.position, pathPoints[index].position) < minDistance)
        {
            if (index >= 0 && index < pathPoints.Length)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
        }
        agent.SetDestination(pathPoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }

  
    public void SetWaypoints(Transform start, Transform end)
    {
        startingPoint = start;
        endingPoint = end;
    }

}
