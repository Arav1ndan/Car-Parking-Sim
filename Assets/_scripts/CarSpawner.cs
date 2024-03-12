using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] pathPoints;
    public Transform startingPoint;
    public Transform endingPoint;
    public float spawnInterval = 5f;
    private float nextSpawnTime;

     void Update()
    {
       
        if (Time.time >= nextSpawnTime)
        {
            SpawnCar();
            nextSpawnTime = Time.time + spawnInterval; 
        }
    }
      void SpawnCar()
    {
        // Instantiate a new car at the starting point
        GameObject newCar = Instantiate(carPrefab, startingPoint.position, startingPoint.rotation);

        // Get the car controller component
        car_ControllerAi carController = newCar.GetComponent<car_ControllerAi>();

        // Assign the path points to the car
        carController.pathPoints = pathPoints;

        // Set the starting and ending points for the car's path
        carController.startingPoint = startingPoint;
        carController.endingPoint = endingPoint;
    }
}
