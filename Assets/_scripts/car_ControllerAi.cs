using UnityEngine;
using UnityEngine.AI;
public class car_ControllerAi : MonoBehaviour
{
     public NavMeshAgent agent;
    public Transform[] pathPoints;
    public int index = 0;
    public float minDistance = 1f;
    public float maxSteeringAngle = 30f;
    public Transform frontLeftWheel;
    public Transform frontRightWheel;

    private Rigidbody carRigidbody;
    public Transform playerCar;
    public float stopDistance = 5f;


    public Transform startingPoint;
    public Transform endingPoint;

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
            if (index >= 0 && index < pathPoints.Length - 1)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
        }
        agent.SetDestination(pathPoints[index].position);

        
        Vector3 desiredVelocity = agent.desiredVelocity;

       
        carRigidbody.velocity = new Vector3(desiredVelocity.x, carRigidbody.velocity.y, desiredVelocity.z);

        
        Vector3 relativePosition = transform.InverseTransformPoint(pathPoints[index].position);
        float targetAngle = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;

        float steeringAngle = Mathf.Clamp(targetAngle, -maxSteeringAngle, maxSteeringAngle);
        
     
        frontLeftWheel.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);
        frontRightWheel.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);

         if (playerCar != null)
        {
            float distanceToPlayerCar = Vector3.Distance(transform.position, playerCar.position);
            if (distanceToPlayerCar < stopDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }
     public void SetWaypoints(Transform start, Transform end)
    {
        startingPoint = start;
        endingPoint = end;
    }
}
