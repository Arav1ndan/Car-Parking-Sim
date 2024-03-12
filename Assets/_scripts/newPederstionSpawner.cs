using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class newPederstionSpawner : MonoBehaviour
{public NavMeshAgent agent;
public Animator animator;
public Transform[] destinationPoints;
public float minDistance;

[SerializeField] private GameObject[] pedestrianPrefabs;
[SerializeField] private int spawnCount = 10;
[SerializeField] private Transform[] PathPoints;

public float spawnInterval = 2f;

void Start()
{
    if (pedestrianPrefabs.Length == 0)
    {
        Debug.LogError("No prefabs assigned to the pedestrianPrefabs array! ");
        return;
    }

    for (int i = 0; i < spawnCount; i++)
    {
        int randomPrefabIndex = Random.Range(0, pedestrianPrefabs.Length);
        int randomPathPointIndex = Random.Range(0, PathPoints.Length);

        GameObject spawnedPrefab = Instantiate(
            pedestrianPrefabs[randomPrefabIndex],
            PathPoints[randomPathPointIndex].position,
            PathPoints[randomPathPointIndex].rotation
        );
    }
}

void Update()
{
    if (!agent.hasPath)
        return;

    float remainingDistance = agent.remainingDistance; // Avoid duplicate calls

    if (remainingDistance <= minDistance)
    {
        int randomIndex = Random.Range(0, destinationPoints.Length);
        Vector3 newDestination = destinationPoints[randomIndex].position;
        agent.SetDestination(newDestination);
    }

    animator.SetFloat("vertical", agent.velocity.magnitude > 0.1f ? 1 : 0);
}
}
