using System;
using UnityEngine;

public class PadersterSpawner : MonoBehaviour
{
    public GameObject pedestrainPrefab;
    public Transform spawnPoint;
    public int numOfPedestrains = 5;

    void Start()
    {
        SpawnPedestrians();
    }

    private void SpawnPedestrians()
    {
        for(int i =0;i< numOfPedestrains;i++)
        {
            GameObject pedestrain = Instantiate(pedestrainPrefab,spawnPoint.position, Quaternion.identity);
            npc_Controller controller = pedestrain.GetComponent<npc_Controller>();
        }
    }
}
