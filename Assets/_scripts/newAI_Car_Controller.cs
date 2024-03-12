using UnityEngine;
using UnityEngine.AI;

public class newAI_Car_Controller : MonoBehaviour
{public NavMeshAgent agent;
    public Transform[] pathPoints;
    public int index = 0;
    public float minDistance = 1f;
    public float maxSteeringAngle = 30f;
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform playerCar;
    public float stopDistance = 5f;
    public float obstacleBuffer = 1f; 
    private Rigidbody carRigidbody;
    public float brakingForce = 50;

    public float radarDistance = 8f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Drive();
    }

    void Drive()
    {
      
        if (Vector3.Distance(transform.position, pathPoints[index].position) < minDistance)
        {
            SetNextDestination();
        }

        
        agent.SetDestination(pathPoints[index].position);


        carRigidbody.velocity = agent.desiredVelocity;

        
        Vector3 relativePosition = transform.InverseTransformPoint(pathPoints[index].position);
        float targetAngle = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;
        float steeringAngle = Mathf.Clamp(targetAngle, -maxSteeringAngle, maxSteeringAngle);
        
      
        frontLeftWheel.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);
        frontRightWheel.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);

        //int playerCarLayerMask = LayerMask.NameToLayer("PlayerCar");
        
        RaycastHit hit;
       if (Physics.Raycast(transform.position, transform.forward, out hit, radarDistance))
        {
           Debug.DrawLine(transform.position, hit.point, Color.red); 
           Debug.Log("Distance to obstacle: " + hit.distance);
           
           if(hit.collider.gameObject.CompareTag("Player"))
           {
               Debug.Log("Player car detected");
               float adjustedStopDistance = Mathf.Max(hit.distance - obstacleBuffer, 0f);
               Debug.Log("Adjusted stop distance: " + adjustedStopDistance);
               if (hit.distance < adjustedStopDistance)
               {
                   agent.isStopped = true;
                   carRigidbody.AddForce(-carRigidbody.velocity * brakingForce);
                   Debug.Log("Obstacle detected. Stopping car.");
               }
               else
               {
                   agent.isStopped = false;
                   carRigidbody.velocity = agent.desiredVelocity;
                   Debug.Log("No obstacle detected. Continuing driving.");
               }
           }          
        }
        else
        {
            agent.isStopped = false;
            carRigidbody.velocity = agent.desiredVelocity;
        }
    }

    void SetNextDestination()
    {
        if (index >= 0 && index < pathPoints.Length - 1)
        {
            index += 1;
        }
        else
        {
            index = 0;
        }
    }
}
